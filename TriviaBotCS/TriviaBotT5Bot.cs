// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
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

        public const string WelcomeText = @"Please answer the questions as it appears in your chat window:";


        static List<Choice> choices = new List<Choice>();
       
        /// <summary>
        /// The <see cref="DialogSet"/> that contains all the Dialogs that can be used at runtime.
        /// </summary>
        static DialogSet _dialogs;

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

            choices.Add(new Choice
            {
                Value = "Plain Pizza",
                Synonyms = new List<string> { "plain" }
            });
            choices.Add(new Choice { Value = "Pizza with Pepperoni", Synonyms = new List<string> { "4 Day", "workshop", "full" } });
            choices.Add(new Choice { Value = "Pizza with Mushrooms", Synonyms = new List<string> { "mushroom", "mushrooms", "shrooms" } });
            choices.Add(new Choice { Value = "Pizza with Peppers, Mushrooms and Brocolli", Synonyms = new List<string> { "vegtable", "veggie" } });
            choices.Add(new Choice { Value = "Pizza with Anchovies" });

            _dialogs = new DialogSet(accessors.ConversationDialogState);
            _dialogs.Add(new ChoicePrompt("choices"));
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
            /* if (turnContext.Activity.Type == ActivityTypes.Message)
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
             }*/
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            /* if (turnContext.Activity.Type == ActivityTypes.Message)
             {
                 // Extract the text from the message activity the user sent.
                 // Make this lowercase not accounting for culture in this case
                 // so that there are fewer string variations which you will
                 // have to account for in your bot.
                 var text = turnContext.Activity.Text.ToLowerInvariant();

                 // Take the input from the user and create the appropriate response.
                 var responseText = ProcessInput(text);

                 // Respond to the user.
                 await turnContext.SendActivityAsync(responseText, cancellationToken: cancellationToken);

                 await SendSuggestedActionsAsync(turnContext, cancellationToken);
             } */
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // Run the DialogSet - let the framework identify the current state of the dialog from
                // the dialog stack and figure out what (if any) is the active dialog.
                var dialogContext = await _dialogs.CreateContextAsync(turnContext, cancellationToken);
                var results = await dialogContext.ContinueDialogAsync(cancellationToken);

                // If the DialogTurnStatus is Empty we should start a new dialog.
                if (results.Status == DialogTurnStatus.Empty)
                {
                    // A prompt dialog can be started directly on the DialogContext. The prompt text is given in the PromptOptions.
                    await dialogContext.PromptAsync(
                        "choices",
                        new PromptOptions { Prompt = MessageFactory.Text("Please answer the trivia question."), Choices = choices },
                        cancellationToken);
                }

                // We had a dialog run (it was the prompt). Now it is Complete.
                else if (results.Status == DialogTurnStatus.Complete)
                {
                    // Check for a result.
                    if (results.Result != null)
                    {
                        // Finish by sending a message to the user. Next time ContinueAsync is called it will return DialogTurnStatus.Empty.
                        await turnContext.SendActivityAsync(MessageFactory.Text($"Thank you, I have your answer as '{results.Result}'."));
                    }
                }
            }
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (turnContext.Activity.MembersAdded != null)
                {
                    // Send a welcome message to the user and tell them what actions they may perform to use this bot
                    await SendWelcomeMessageAsync(turnContext, cancellationToken);
                }
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

            // Save the new turn count into the conversation state.
            await _accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        }


        /// <summary>
        /// On a conversation update activity sent to the bot, the bot will
        /// send a message to the any new user(s) that were added.
        /// </summary>
        /// <param name="turnContext">Provides the <see cref="ITurnContext"/> for the turn of the bot.</param>
        /// <param name="cancellationToken" >(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(
                        $"Welcome to Trivia Bot(T5) Assistant, {member.Name}.                                                    {WelcomeText}",
                        cancellationToken: cancellationToken);

                    var dialogContext = await _dialogs.CreateContextAsync(turnContext, cancellationToken);
                    await dialogContext.PromptAsync(
                       "choices",
                       new PromptOptions { Prompt = MessageFactory.Text("Please answer the trivia question."), Choices = choices },
                       cancellationToken);
                    //await SendSuggestedActionsAsync(turnContext, cancellationToken);
                }
            }
        }

        /// <summary>
        /// Creates and sends an activity with suggested actions to the user. When the user
        /// clicks one of the buttons the text value from the <see cref="CardAction"/> will be
        /// displayed in the channel just as if the user entered the text. There are multiple
        /// <see cref="ActionTypes"/> that may be used for different situations.
        /// </summary>
        /// <param name="turnContext">Provides the <see cref="ITurnContext"/> for the turn of the bot.</param>
        /// <param name="cancellationToken" >(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        private static async Task SendSuggestedActionsAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            
            var reply = turnContext.Activity.CreateReply("What is your favorite color?");
            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "Red", Type = ActionTypes.ImBack, Value = "Red" },
                    new CardAction() { Title = "Yellow", Type = ActionTypes.ImBack, Value = "Yellow" },
                    new CardAction() { Title = "Blue", Type = ActionTypes.ImBack, Value = "Blue" },
                },
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task GetNextQuestionAsync(ITurnContext context, CancellationToken cancellationToken)
        {
            // Fetch the members in the current conversation
            var connector = new ConnectorClient(new Uri(context.Activity.ServiceUrl));
            var members = await connector.Conversations.GetConversationMembersAsync(context.Activity.Conversation.Id);

            var Id = context.Activity.Recipient.Id;

            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append("  \"Id\": \"\"" + Id + "\"");
            sb.Append("}");

            var client = new HttpClient();
            await client.PostAsync("https://msopenhack.azurewebsites.net/api/trivia/register",
                new StringContent(sb.ToString()));


        }


        /// <summary>
        /// Given the text from the message activity the user sent, create the text for the response.
        /// </summary>
        /// <param name="text">The text that was input by the user.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        private static string ProcessInput(string text)
        {
            const string colorText = "is the best color, I agree.";
            switch (text)
            {
                case "red":
                    {
                        return $"Red {colorText}";
                    }

                case "yellow":
                    {
                        return $"Yellow {colorText}";
                    }

                case "blue":
                    {
                        return $"Blue {colorText}";
                    }

                default:
                    {
                        return "Please select a color from the suggested action choices";
                    }
            }
        }
    }
}
