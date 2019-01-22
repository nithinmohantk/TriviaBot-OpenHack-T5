using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TriviaBotT5
{
    public class WelcomeUserBot : IBot
    {
        // Generic message to be sent to user
        private const string _genericMessage = @"This is a simple Welcome Bot sample. You can say 'intro' to 
                                         see the introduction card. If you are running this bot in the Bot 
                                         Framework Emulator, press the 'Start Over' button to simulate user joining a bot or a channel";

        // The bot state accessor object. Use this to access specific state properties.
        private readonly WelcomeUserStateAccessors _welcomeUserStateAccessors;

        // Initializes a new instance of the <see cref="WelcomeUserBot"/> class.
        public WelcomeUserBot(WelcomeUserStateAccessors statePropertyAccessor)
        {
            _welcomeUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");
        }

        // Every conversation turn for our WelcomeUser Bot will call this method, including
        // any type of activities such as ConversationUpdate or ContactRelationUpdate which
        // are sent when a user joins a conversation.
        // This bot doesn't use any dialogs; it's "single turn" processing, meaning a single
        // request and response.
        // This bot uses UserState to keep track of first message a user sends.
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
        {
            // Use state accessor to extract the didBotWelcomeUser flag
            var didBotWelcomeUser = await _welcomeUserStateAccessors.DidBotWelcomeUser.GetAsync(turnContext, () => false);

            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // Your bot should proactively send a welcome message to a personal chat the first time
                // (and only the first time) a user initiates a personal chat with your bot.
                if (didBotWelcomeUser == false)
                {
                    // Update user state flag to reflect bot handled first user interaction.
                    await _welcomeUserStateAccessors.DidBotWelcomeUser.SetAsync(turnContext, true);
                    await _welcomeUserStateAccessors.UserState.SaveChangesAsync(turnContext);

                    // the channel should sends the user name in the 'From' object
                    var userName = turnContext.Activity.From.Name;

                    await turnContext.SendActivityAsync($"You are seeing this message because this was your first message ever to this bot.", cancellationToken: cancellationToken);
                    await turnContext.SendActivityAsync($"It is a good practice to welcome the user and provide a personal greeting. For example, welcome {userName}.", cancellationToken: cancellationToken);
                }
            }
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate) // Greet when users are added to the conversation.
            {
                if (turnContext.Activity.MembersAdded.Any())
                {
                    // Iterate over all new members added to the conversation
                    foreach (var member in turnContext.Activity.MembersAdded)
                    {
                        // Greet anyone that was not the target (recipient) of this message
                        // the 'bot' is the recipient for events from the channel,
                        // turnContext.Activity.MembersAdded == turnContext.Activity.Recipient.Id indicates the
                        // bot was added to the conversation.
                        if (member.Id != turnContext.Activity.Recipient.Id)
                        {
                            await turnContext.SendActivityAsync($"Hi there - {member.Name}. Welcome to the 'Welcome User' Bot. This bot will introduce you to welcoming and greeting users.", cancellationToken: cancellationToken);
                            await turnContext.SendActivityAsync($"You are seeing this message because the bot recieved at least one 'ConversationUpdate' event,indicating you (and possibly others) joined the conversation. If you are using the emulator, pressing the 'Start Over' button to trigger this event again. The specifics of the 'ConversationUpdate' event depends on the channel. You can read more information at https://aka.ms/about-botframewor-welcome-user", cancellationToken: cancellationToken);
                            await turnContext.SendActivityAsync($"It is a good pattern to use this event to send general greeting to user, explaning what your bot can do. In this example, the bot handles 'hello', 'hi', 'help' and 'intro. Try it now, type 'hi'", cancellationToken: cancellationToken);
                        }
                    }
                }
            }
            // save any state changes made to your state objects.
            await _welcomeUserStateAccessors.UserState.SaveChangesAsync(turnContext);
        }

    }
}
