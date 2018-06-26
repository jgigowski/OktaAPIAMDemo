using System;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using Newtonsoft.Json;
using OktaAPIShared.Models;

namespace OktaAPI.Helpers
{
    public class APIHelper
    {
        private static string _apiUrlBase;
        private static string _oktaOAuthHeaderAuth;
        private static string _oktaOAuthIssuerId;
        private static string _oktaOAuthClientId;
        private static string _oktaOAuthRedirectUri;
        
        private static HttpClient _client = new HttpClient();

        static APIHelper()
        {
            _apiUrlBase = WebConfigurationManager.AppSettings["okta:BaseUrl"];
            _oktaOAuthIssuerId = WebConfigurationManager.AppSettings["okta:OAuthIssuerId"];
            _oktaOAuthClientId = WebConfigurationManager.AppSettings["okta:OAuthClientId"];
            _oktaOAuthRedirectUri = WebConfigurationManager.AppSettings["okta:OAuthRedirectUri"];

            var oktaOAuthSecret = WebConfigurationManager.AppSettings["okta:OauthClientSecret"];
            _oktaOAuthHeaderAuth = Base64Encode($"{_oktaOAuthClientId}:{oktaOAuthSecret}");
        }
        
        public static OIDCTokenResponse GetToken()
        {
            var sJsonResponse = JsonHelper.Post($"https://{_apiUrlBase}/oauth2/{_oktaOAuthIssuerId}/v1/token?grant_type=client_credentials&redirect_uri={_oktaOAuthRedirectUri}&scope=crud", _oktaOAuthHeaderAuth);
            return JsonConvert.DeserializeObject<OIDCTokenResponse>(sJsonResponse);
        }

        public static TokenIntrospectionResponse IntrospectToken(string token)
        {
            var sJsonResponse = JsonHelper.Post($"https://{_apiUrlBase}/oauth2/{_oktaOAuthIssuerId}/v1/introspect?token={token}&token_type_hint=access_token", _oktaOAuthHeaderAuth);
            return JsonConvert.DeserializeObject<TokenIntrospectionResponse>(sJsonResponse);
        }

        public static void RevokeToken(string token)
        {
            JsonHelper.Post($"https://{_apiUrlBase}/oauth2/{_oktaOAuthIssuerId}/v1/revoke?token={token}&token_type_hint=access_token", _oktaOAuthHeaderAuth);
        }

        private static string Base64Encode(string plainText)
        {
            var bytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(bytes);
        }
    }
}