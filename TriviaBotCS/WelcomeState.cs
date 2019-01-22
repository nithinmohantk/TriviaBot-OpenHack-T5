using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaBotT5
{
    /// The state object is used to keep track of various state related to a user in a conversation.
    /// In this example, we are tracking if the bot has replied to customer first interaction.
    public class WelcomeUserState
    {
        public bool DidBotWelcomeUser { get; set; } = false;
    }

    /// Initializes a new instance of the <see cref="WelcomeUserStateAccessors"/> class.
    public class WelcomeUserStateAccessors
    {
        public WelcomeUserStateAccessors(UserState userState)
        {
            this.UserState = userState ?? throw new ArgumentNullException(nameof(userState));
        }

        public IStatePropertyAccessor<bool> DidBotWelcomeUser { get; set; }

        public UserState UserState { get; }
    }
}
