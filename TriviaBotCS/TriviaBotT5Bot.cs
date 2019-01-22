// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using Microsoft.Extensions.Logging;

namespace TriviaBotT5
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class TriviaBotT5Bot : IBot
    {
        private readonly TriviaBotT5Accessors _accessors;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public TriviaBotT5Bot(TriviaBotT5Accessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<TriviaBotT5Bot>();
            _logger.LogTrace("Turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
        }

        /// <summary>
        /// Every conversation turn for our Echo Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // Get the conversation state from the turn context.
                var state = await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());

                // Bump the turn count for this conversation.
                state.TurnCount++;

                // Set the property using the accessor.
                await _accessors.CounterState.SetAsync(turnContext, state);

                // Save the new turn count into the conversation state.
                await _accessors.ConversationState.SaveChangesAsync(turnContext);

                // Echo back to the user whatever they typed.
                var responseMessage = $"Turn {state.TurnCount}: You sent '{turnContext.Activity.Text}'\n";
                await turnContext.SendActivityAsync(responseMessage);
            }
            else if( turnContext.Activity.Type == ActivityTypes.InstallationUpdate)
            {
                var responseMessage = $"Installation Update '{turnContext.Activity.Text}'\n";
                await turnContext.SendActivityAsync(responseMessage);

                var context = turnContext;
                // Fetch the members in the current conversation
                var connector = new ConnectorClient(new Uri(context.Activity.ServiceUrl));
                var members = await connector.Conversations.GetConversationMembersAsync(context.Activity.Conversation.Id);     

                TeamsChannelData channelData = turnContext.Activity.GetChannelData<TeamsChannelData>();

                if (channelData != null)
                {
                    var teamId = channelData.Team.Id;
                    var sb = new StringBuilder();
                    sb.Append("{");
                    sb.Append("  \"teamId\": \"\"" + teamId + "\"");
                    sb.Append("  \"members\": [");
                    // Concatenate information about all members into a string
                    foreach (var member in members)
                    {
                        sb.Append($"{{\"id\": \"{member.Id}\", \"name\" = \"{member.Name}\"}}");
                        sb.AppendLine();
                    }
                    sb.Append("]}");

                    var client = new HttpClient();
                    await client.PostAsync("https://msopenhack.azurewebsites.net/api/trivia/register",
                        new StringContent(sb.ToString()));

                    await turnContext.SendActivityAsync($"Team Roster Updated");
                }


            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }
    }
}
