# Challenge 4 - Leaderboard Tab

## Background

Custom tabs in Microsoft Teams offers developers a canvas to integrate 3rd party applications. Where a bot offers a conversational user experience, a tab provides a more traditional user interface. Challenge 4 sets out to ensure you can integrate 3rd party web content into Microsoft Teams as a custom tab.

The Microsoft Graph API offers a single endpoint into all data the drives productivity...mail, calendar, contacts, documents, directory, devices, and more. Challenge 4 will have you use the Microsoft Graph to query and display rich profile information for trivia players. Because the Microsoft Graph is secured by Azure Active Directory, you will integrate authentication into your custom tab.

## Challenge

Add a configurable tab to your Microsoft Teams app to display trivia leaderboards. The configuration should allow a user to select either a team or individual leaderboard (based on aggregated trivia score). The individual leaderboard should display user profile photos from the Microsoft Graph. This will require your tab to be secured by Azure Active Directory using a new app registration in Azure AD. Trivia leaderboard information can be retrieved via the [Get Trivia Leaderboard](https://msopenhack.azurewebsites.net/swagger/ui/index#!/Trivia/Trivia_GetLeaderboard) API (documented in OpenHack APIs).

## Success Criteria

- Your team must build a configurable tab to display trivia leaderboards.

- The configurable tab must allow a user to select either a team or individual leaderboard.

- The individual leaderboard must use the Microsoft Graph to display profile pictures for each user.

## Important Notes

- Although the bot created in the previous challenge includes an app registration in Azure Active Directory, your team should create a new app registration to complete this challenge. Do not re-leverage the bot's app registration as it is used exclusively to secure bot communication. This will mean you have two app ids and two app secrets.

- The OpenHack APIs will only function in the context of your registered team in Microsoft Teams. You can test these APIs in the swagger documentation or using a tool like [Postman](https://www.getpostman.com/) or [Fiddler](https://www.telerik.com/fiddler), but you will need the team details (Teams ID and Azure AD Object ID).

## References

- [OpenHack API documentation](https://aka.ms/msopenhackapi)

- [Develop tabs for Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/tabs/tabs-overview)

- [Developing Configurable Tabs in Microsoft Teams](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/tabs/tabs-configuration)

- [Registering your application with the Azure AD v1 endpoint](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-integrating-applications#adding-an-application)

- [Register your app with the Azure AD v2 endpoint](https://developer.microsoft.com/en-us/graph/docs/concepts/auth_register_app_v2)

Some other useful resources are:

- [Get context for your Microsoft Teams tab](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/tabs/tabs-context)

- [Authenticate a user in your Microsoft Teams tab](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/authentication)

- [Authentication, SSO, and Microsoft Graph in Microsoft Teams Tabs](https://techcommunity.microsoft.com/t5/Microsoft-Teams-Blog/Authentication-SSO-and-Microsoft-Graph-in-Microsoft-Teams-Tabs/ba-p/125366)

- [Microsoft Graph documentation](https://developer.microsoft.com/en-us/graph/docs/concepts/overview)

- [Microsoft Graph documentation for get profile photo](https://developer.microsoft.com/en-us/graph/docs/api-reference/v1.0/api/profilephoto_get)

- Hint: Profile photos requires at least the User.ReadBasic.All permission scope.