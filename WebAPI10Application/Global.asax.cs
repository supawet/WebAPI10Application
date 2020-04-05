using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Data.SqlClient;
using System.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.SqlServer.Management.AlwaysEncrypted.AzureKeyVaultProvider;
using System.Threading.Tasks;

using WebAPI10Application.MessageHandlers;

namespace WebAPI10Application
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static ClientCredential _clientCredential;

        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyMessageHandler());

            InitializeAzureKeyVaultProvider();
        }

        static void InitializeAzureKeyVaultProvider()
        {
            string clientId = ConfigurationManager.AppSettings["AuthClientId"];
            string clientSecret = ConfigurationManager.AppSettings["AuthClientSecret"];
            _clientCredential = new ClientCredential(clientId, clientSecret);

            SqlColumnEncryptionAzureKeyVaultProvider azureKeyVaultProvider =
              new SqlColumnEncryptionAzureKeyVaultProvider(GetToken);

            Dictionary<string, SqlColumnEncryptionKeyStoreProvider> providers =
              new Dictionary<string, SqlColumnEncryptionKeyStoreProvider>();

            providers.Add(SqlColumnEncryptionAzureKeyVaultProvider.ProviderName, azureKeyVaultProvider);
            SqlConnection.RegisterColumnEncryptionKeyStoreProviders(providers);
        }

        public async static Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, _clientCredential);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the access token");

            return result.AccessToken;
        }
    }
}