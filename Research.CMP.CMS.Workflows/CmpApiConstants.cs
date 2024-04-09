namespace Research.CMP.CMS.Workflows
{
    public static class CmpApiConstants
    {
        public const string TokenCacheKey = "welcome-access-token";

        public const string AccountsBaseUrl = "https://accounts.cmp.optimizely.com";
        
        public const string AccountsGetToken = "/o/oauth2/v1/token";

        public const string APIBaseUrl = "https://api.cmp.optimizely.com";

        public const string UploadUrl = "v3/upload-url";
        
        public const string AssetListPath = "v3/assets";

        public const string GetImagesPath = "v3/images/{0}";

        public const string GetRawItemPath = "v3/raw-files/{0}";

        public const string GetVideoPath = "v3/videos/{0}";

        public const string GetArticlePath = "v3/articles/{0}";

        public const string FolderListPath = "v3/folders";

        public const string GetFolderPath = "v3/folders/{0}";

        public const string FolderIdentifier = "folder";

        public const string ArticleIdentifier = "article";

        public const string ImageIdentifier = "image";

        public const string RawIdentifier = "raw_file";

        public const string VideoIdentifier = "video";
    }
}