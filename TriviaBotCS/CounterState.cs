// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using TriviaBotT5.Models;

namespace TriviaBotT5
{
    /// <summary>
    /// Stores counter state for the conversation.
    /// Stored in <see cref="Microsoft.Bot.Builder.ConversationState"/> and
    /// backed by <see cref="Microsoft.Bot.Builder.MemoryStorage"/>.
    /// </summary>
    public class QuestionState
    {
        /// <summary>
        /// Gets or sets the number of turns in the conversation.
        /// </summary>
        /// <value>The number of turns in the conversation.</value>
        public QuestionModel CurrentQuestion { get; set; }
    }
}
