using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Research.CMP.CMS.Workflows.Models;
using Research.CMP.CMS.Workflows.REST.Authorization;

namespace Research.CMP.CMS.Workflows;

public class CmpApiClient
{
    private readonly ILogger<CmpApiClient> _log;
    private string ClientId { get; }
    private string ClientSecret { get; }
    private AuthorizationResponse _token = null;
    private DateTime _tokenExpiry = DateTime.Now;
    
    public CmpApiClient(IOptions<CmpCmsWorkflowOptions> options, ILogger<CmpApiClient> logger)
    {
        _log = logger;
        //FlurlHttp.Clients.UseNewtonsoft();
        ClientId = options.Value.ClientId;
        ClientSecret = options.Value.ClientSecret;
    }

    #region Authorization

    public async Task<AuthorizationResponse> GetToken(string clientId = "", string clientSecret = "")
    {
        if (DateTime.Now <= _tokenExpiry && _token is not null) return this._token;

        var authorizationRequest = new AuthorizationRequest
        {
            ClientId = string.IsNullOrEmpty(clientId) ? this.ClientId : clientId,
            ClientSecret = string.IsNullOrEmpty(clientSecret) ? this.ClientSecret : clientSecret,
        };

        try
        {

            _token = await CmpApiConstants.AccountsBaseUrl
                .AppendPathSegment(CmpApiConstants.AccountsGetToken)
                .PostJsonAsync(authorizationRequest)
                .ReceiveJson<AuthorizationResponse>();
        }
        catch (Exception e)
        {
            _log.LogError(e.Message);
            throw;
        }

        
        if (_token is null || string.IsNullOrEmpty(_token.AccessToken))
        {
            throw new Exception("Could not get token, please check the clientId and clientSecret");
        }
        
        _tokenExpiry = DateTime.Now.AddSeconds(_token.ExpiresIn - 120); // keep 2 minutes as buffer
        
        return _token;
    }

    #endregion 
    
    #region Tasks
    public async Task<T> GetData<T>(string url)
    {
        var token = await GetToken(this.ClientId, this.ClientSecret);
        return url.WithOAuthBearerToken(token.AccessToken).GetJsonAsync<T>().Result;
    }
    
    public async Task<int> PatchTask(string url, object data)
    {
        var token = await GetToken(this.ClientId, this.ClientSecret);
        return url.WithOAuthBearerToken(token.AccessToken).PatchJsonAsync(data).Result.StatusCode;
    }
    #endregion
    
}