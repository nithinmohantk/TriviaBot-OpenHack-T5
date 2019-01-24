# Challenge 3 - Trivia bot

## Background

The main trivia logic will be delivered through a bot in Microsoft Teams. This is a fun spin on Assessment/Quiz bots that have become very popular with software and learning management companies.

In Challenge 3, you build the trivia bot for Microsoft Teams. The challenge will ensure you understand popular patterns and best practices for developing bots for Microsoft Teams, such as retrieving team roster information and providing a good "first run" experience.

## Challenge

Develop a trivia app for Microsoft Teams that uses a bot to ask trivia questions. When the app is installed into a team, it should retrieve the team roster and POST it to the [Register Team](https://msopenhack.azurewebsites.net/swagger/ui/index#!/Trivia/Trivia_RegisterTeam) API (documented in OpenHack APIs).

For the trivia logic, the bot can retrieve questions via the [Get Trivia Question](https://msopenhack.azurewebsites.net/swagger/ui/index#!/Trivia/Trivia_GetQuestion) API and submit answers using the [Submit Answer](https://msopenhack.azurewebsites.net/swagger/ui/index#!/Trivia/Trivia_SubmitAnswer) API (documented in OpenHack APIs). Finally, all team members should be able to independently answer trivia questions. This can be accomplished in 1:1 chat or in team conversations (don't worry about implementing in both places).

## Success Criteria

- Your team must build a bot for Microsoft Teams that prompts users to answer multiple-choice trivia questions.

- Team members must be able to independently answer trivia questions.

- The trivia app must retrieve team roster information for the team it is installed in and send that roster to the RegisterTeam API provided.

## Important Notes

- The [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/releases) is useful for debugging basic bot dialog locally. However, Teams context is only available to bots running within Microsoft Teams. To debug bots running in Microsoft Teams, the bot endpoint(s) must be on the public internet. [Ngrok](https://msopenhack.azurewebsites.net/Home/Ngrok) is a helpful tool for creating an internet tunnel for localhost. See [Getting started with ngrok](https://msopenhack.azurewebsites.net/Home/Ngrok) for full instructions. **Seriously, use ngrok for this challenge.**

- The OpenHack APIs will only function in the context of your registered team in Microsoft Teams. You can test these APIs in the swagger documentation or using a tool like [Postman](https://www.getpostman.com/) or [Fiddler](https://www.telerik.com/fiddler), but you will need the team details (Teams ID and Azure AD Object ID).

## References

- [OpenHack API documentation](https://aka.ms/msopenhackapi)

- [Create a bot for Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/bots/bots-create)

- [Handle bot events in Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/bots/bots-notifications#team-member-or-bot-addition)

- [Get context for your Microsoft Teams bot](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/bots/bots-context)

- [Dialogs in .NET BotBuilder](https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-dialogs)

- [Dialogs in Node.js BotBuilder](https://docs.microsoft.com/en-us/bot-framework/nodejs/bot-builder-nodejs-dialog-prompt)

Some other useful resources are:

- [Local debugging a bot in Microsoft Teams with ngrok](https://docs.microsoft.com/en-us/microsoftteams/platform/resources/general/debug#locally-hosted)

- [Yeoman Generator for Microsoft Teams apps](https://github.com/OfficeDev/generator-teams)

- [Quickly develop apps with Teams App Studio](https://docs.microsoft.com/en-us/microsoftteams/platform/get-started/get-started-app-studio)

- [Bot Framework quickstart for .NET](https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-quickstart)

- [Bot Framework quickstart for Node.js](https://docs.microsoft.com/en-us/bot-framework/nodejs/bot-builder-nodejs-quickstart)

- [Bot Framework - Design and control conversation flow](https://docs.microsoft.com/en-us/bot-framework/bot-service-design-conversation-flow)
