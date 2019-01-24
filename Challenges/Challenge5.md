# Challenge 5 - Process Achievements

## Background

When an event occurs in an application, developers often need to respond to that event in many different places and ways. Azure Event Grid offers an intelligent event routing service that allows for uniform event consumption using a publish-subscribe model. Challenge 5 will have your team build an Event Grid Topic and create several subscriptions to it.

Bot conversations initiated by a user might be the most common and familiar use case for bots. However, bots can initiate their entirely new conversations via a proactive message. This is a very popular developer scenario for bots and will be the function you perform in your Event Grid subscription.

In this challenge, you will also update user profiles using the Microsoft Graph. The data available through Microsoft Graph isn't limited 1st party data. Resources in the Microsoft Graph can be extended with 3rd party data through extensions. This challenge will use an open extension on users to store achievement badges the players earn throughout the trivia game.

## Challenge

Your bot calls the [Submit Answer](https://msopenhack.azurewebsites.net/swagger/ui/index#!/Trivia/Trivia_SubmitAnswer) API, which returns an achievement badge for the user. When the user earns a new achievement badge, you should create and send that data to an Azure Event Grid Topic. Create a subscription to the Event Grid Topic using an Azure Functions that sends a proactive message individually to each team member in Microsoft Teams to announce the achievement. You can use the roster information to find the details required to proactively message each user (feel free to hard-code this). Create a second subscription to your Event Grid Topic that updates the user's profile in the Microsoft Graph when a new achievement badge is earned. The achievement badge should be stored on a "**badge**" property of a user open extension using the "**com.msopenhack.trivia**" extensionName.

> NOTE: Event Grid is used here because it allows you to de-couple heavy processing from the bot's conversation logic (which would make the bot feel sluggish to end-users).

## Success Criteria

- Your bot must send proactive messages individually to each team member when a user earns a new achievement badge.

- The proactive messages must be sent from a subscription on an Azure Event Grid Topic.

- When a user earns a new achievement badge, you must store its value on a user open extension using the extensionName **com.msopenhack.trivia** and property named **badge**.

## Important Notes

- Similar to the Leaderboard, this process will need a token to perform operations against the Microsoft Graph. Creating/Updating a user open extension will require the delegated permission User.ReadWrite.

## References

- [Create and route custom events to Azure Event Grid](https://docs.microsoft.com/en-us/azure/event-grid/custom-event-quickstart)

- [Getting Started with Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-overview)

- [Creating a conversation with Microsoft Teams bot](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/bots/bots-conversations#creating-a-conversation)

- [Adding custom data to the Microsoft Graph using open extensions](https://developer.microsoft.com/en-us/graph/docs/concepts/extensibility_open_users)

- [BotAuth package for OAuth in Bot Framework](https://github.com/MicrosoftDX/botauth)

- [Microsoft Teams authentication flow for bots](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/authentication/auth-flow-bot)

- [Add authentication to your bot via Azure Bot Service](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-tutorial-authentication?view=azure-bot-service-3.0)
