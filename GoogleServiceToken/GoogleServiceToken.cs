using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace GoogleServiceToken
{
    public class GetAccessToken : CodeActivity
    {
        [Category("Input")]
        [DisplayName("Credential File")]
        [Description("Provide the filepath to the Service Account JSON key credential file exported from Google Cloud Platform")]
        [RequiredArgument]
        public InArgument<string> CredentialFile { get; set; }

        [DisplayName("Scopes")]
        [Description("Enter a space-separated list of Google OAuth2 scopes to authenticate the account against")]
        [Category("Input")]
        public InArgument<string> Scopes { get; set; }

        [Category("Output")]
        public OutArgument<string> AccessToken { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Inputs
            var credentialfile = CredentialFile.Get(context);
            var scopes = Scopes.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            JObject svcCreds = (JObject)JsonConvert.DeserializeObject(System.IO.File.ReadAllText(credentialfile));
            String svcEmail = (String)svcCreds["client_email"];

            String accessToken = "";
            ServiceAccountCredential credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(svcEmail)
                {
                    Scopes = scopes.Split()
                }.FromPrivateKey((String)svcCreds["private_key"]));

            if (credential.RequestAccessTokenAsync(CancellationToken.None).Result)
            {
                accessToken = credential.Token.AccessToken;
            }
            ///////////////////////////

            // Outputs
            AccessToken.Set(context, accessToken);
        }
    }
}