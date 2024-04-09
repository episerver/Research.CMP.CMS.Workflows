using Flurl;
using Flurl.Http;
using Research.CMP.CMS.Workflows.REST.Authorization;

namespace Research.CMP.CMS.Workflows;

public class CmpApiClient
{
    private string ClientId { get; }
    private string ClientSecret { get; }
    private AuthorizationResponse _token = null;
    private DateTime _tokenExpiry = DateTime.Now;
    
    public CmpApiClient(string clientId, string clientSecret)
    {
        //FlurlHttp.Clients.UseNewtonsoft();
        ClientId = clientId;
        ClientSecret = clientSecret;
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
            Console.WriteLine(e);
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
    #endregion
    
}