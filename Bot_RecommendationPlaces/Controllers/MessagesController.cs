using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bot_RecommendationPlaces.Model.Bots;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace Bot_RecommendationPlaces
{
    [BotAuthentication]
    public class MessagesController : ApiController {
        /// <summary>
        /// Conecta a classe RecommendationPlace ao bot. Esta função irá retornar uma interface IDialog. 
        /// Essa interface que será responsável por gerenciar o fluxo de conversão entre nosso bot e os usuários.
        /// </summary>
        /// <returns></returns>
        internal static IDialog<RecommendationPlace> MakeRootDialog() {
            return Chain.From(() => FormDialog.FromForm(RecommendationPlace.BuildForm));
        }

        /// <summary>
        /// POST: api/Messages
        /// Recebe uma mensagem do usuário e a responde
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity) {
            if (activity.Type == ActivityTypes.Message) {
                await Conversation.SendAsync(activity, MakeRootDialog);
            } else {
                HandleSystemMessage(activity);
            } // end else
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        /// <summary>
        /// Método utilizado para capturar eventos relacionados a conversação
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Activity HandleSystemMessage(Activity message) {
            if (message.Type == ActivityTypes.DeleteUserData) {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            } else if (message.Type == ActivityTypes.ConversationUpdate) {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            } else if (message.Type == ActivityTypes.ContactRelationUpdate) {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            } else if (message.Type == ActivityTypes.Typing) {
                // Handle knowing tha the user is typing
            } else if (message.Type == ActivityTypes.Ping) {

            } // end else

            return null;
        }
    }
}