# Challenge 2 - Team Notifications

## Background

Throughout the OpenHack, your team will build (and play) a multiple-choice trivia application (similar to bar/pub trivia). Microsoft Teams will eventually host the trivia application since it is already organized into teams. In Challenge 2, you will build a process to notify Microsoft Teams when new trivia questions are available in a 3rd party application (in this case SharePoint).

Sending notifications into Microsoft Teams is a very poplar scenario with developers and can be accomplished in a number of ways. The Microsoft Graph, Microsoft Flow, and Azure Logic Apps can all post messages into Microsoft Teams as a user. To post messages as an application, you need a bot or connector in Microsoft Teams. Microsoft Teams even offers a generic "Incoming Webhook" connector that can be leveraged with little configuration. Your team can use any of these approaches to accomplish the challenge goals.

## Challenge

Monitor the Trivia Questions list in SharePoint and send a notification into Microsoft Teams when new trivia questions are available. Notifications should be sent only to your team and only for questions in the category assigned to you (**{0}**). Notifications should be formatted as a rich card, including the question Title and Category. For the sake of testing, you can expect new questions added every minute (this will stop once you complete the challenge).

[SharePoint Trivia Questions List](https://{1}.sharepoint.com/Lists/Trivia%20Questions)

## Success Criteria

- Your team in Microsoft Teams must be notified when new items are added to the Trivia Questions list in SharePoint.

- Notifications must only be sent for questions in your assigned category (**{0}**).

- Notifications must be formatted as a rich card and include the question Title and Category.

## References

- [Create your first Logic App](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-create-a-logic-app)

- [Create your first Microsoft Flow](https://docs.microsoft.com/en-us/flow/getting-started)

- [Create chat thread via Microsoft Graph](https://developer.microsoft.com/en-us/graph/docs/api-reference/beta/api/channel_post_chatthreads)

- [Connectors in Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/connectors#setting-up-a-custom-incoming-webhook)

- [Message Card Playground](https://messagecardplayground.azurewebsites.net/)