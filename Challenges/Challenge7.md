# Challenge 7 - Tweet Completion

## Background

Azure Logic Apps offer a growing list of triggers, conditions, and actions for for building powerful, integrated, and "codeless" applications. In your 7th and final OpenHack challenge, you will explore more of these capabilities, including integration with Azure Event Grid and integration with popular 3rd party applications/services.

## Challenge

Setup a second Event Grid Topic with a subscription to tweet data.tweet of events sent to it. You can use your own Twitter account or create a test Twitter account for the tweet. When the Event Grid Topic and subscription are configured, you will register your Event Grid Topic details (endpoint and access key) with the OpenHack bot. OpenHack will use these details to add an event your subscription should tweet.  When OpenHack validates the tweet, your team will reach a successful completion of your OpenHack!

## Success Criteria

- You must register your Event Grid Topic details (endpoint and access key) with the OpenHack bot.

- You must tweet the data.tweet of events sent to your Event Grid Topic as per the schema shown below:

```
{
	"id": "1234",
	"eventType": "MSOpenHack.Tweet",
	"subject": "msopenhack/tweet",
	"eventTime": "2018-07-02T21:03:07+00:00",
	"data": {
		"tweet": "Some text that you should tweet"
	}
}
```

## References

- [Create and route custom events to Azure Event Grid](https://docs.microsoft.com/en-us/azure/event-grid/custom-event-quickstart)

- [Create your first Logic App](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-create-a-logic-app)

- [Handle content types in Logic Apps](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-content-type)