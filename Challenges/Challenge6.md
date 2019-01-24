# Challenge 6 - Messaging Extension

## Background

Microsoft Teams puts your most important information at your fingertips through Messaging Extensions. Messaging Extensions allow you to search 3rd party data/record and quickly pulling it into a conversation. They serve to streamline all the times you copy/paste information or links into a conversation. In Challenge 6, you will build a Messaging Extension for Microsoft Teams.

## Challenge

Add a messaging extension to your Microsoft Teams app that searches trivia players and renders a "player card" for a conversation. The card must contain the trivia player name, score, achievement badge and profile photo. The card photo should be the users profile photo with their achievement badge as an overlay. It is recommended you build an Azure Function to create this photo mashup. You can use the [Search Trivia](https://msopenhack.azurewebsites.net/swagger/ui/index#!/Trivia/Trivia_Search) API (documented in OpenHack APIs) to search players. When you are done, send your "player card" to the OpenHack bot to validate the completion of the challenge.

## Success Criteria

- Your application must call the Search API to lookup trivia player information and expose it through a Messaging Extension.

- The messaging extension must contain the trivia player's name, score, achievement badge, and profile photo.

- The card photo must be the users profile photo with their achievement badge as an overlay ([EXAMPLE](https://msopenhack.azurewebsites.net/content/msgext.png)).

- You must send the OpenHack bot your "player card" using the Messaging Extension you build.

## References

- [OpenHack API documentation](https://aka.ms/msopenhackapi)

- [Developing messaging extensions for Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/messaging-extensions)

- [Cards and card actions in Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/bots/bots-cards)

- [Combining multiple Microsoft Graph requests in one HTTP call](https://developer.microsoft.com/en-us/graph/docs/concepts/json_batching)