using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.Graph.Beta;
using Microsoft.Graph.Beta.Models;

namespace Microsoft.GraphApi.BetaTest
{
    public class CreateTeam
    {
        public async Task Create()
        {
            var graphClient = CreateGraphClient();
            var me = await graphClient.Users.GetAsync(q => q.QueryParameters.Filter = $"mail = sebastian.asd@sadsa.de");

            await graphClient.Teams.PostAsync(new Team()
            {
                DisplayName = "TestTeams",
                Visibility = TeamVisibilityType.Private,
                Members = new List<ConversationMember>()
                {
                    new ConversationMember()
                    {
                        DisplayName = me.Value.First().DisplayName,
                        Id = me.Value.First().Id,
                        Roles = new List<string>() { "owner" }
                    }
                },
                Channels = new List<Channel>()
                {
                    new Channel()
                    {
                        DisplayName = "Allgemein",
                        IsFavoriteByDefault = true,
                        Description = "Dies ist der generelle Channel"
                    }
                }
            });
        }

        private static GraphServiceClient CreateGraphClient()
        {
            var scopes = new[]
            {
                "https://graph.microsoft.com/Channel.Create",
                "https://graph.microsoft.com/Channel.ReadBasic.All",
                "https://graph.microsoft.com/ChannelSettings.ReadWrite.All",
                "https://graph.microsoft.com/Directory.ReadWrite.All",
                "https://graph.microsoft.com/User.Read"
            };

            var tenantId = "808f7945-2acf-4ce3-a2c1-bc495b741382";
            var clientId = "9b3234ac-897b-4821-aa6f-edf4b65e7917";

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            var secretCredentials = new ClientSecretCredential(tenantId, clientId, "asdasdasda");
            var graphClient = new GraphServiceClient(secretCredentials, scopes);
            return graphClient;
        }
    }
}
