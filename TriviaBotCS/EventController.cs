using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TriviaBotT5
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly ILogger _logger;
        private IConfiguration _config;
        public EventController(ILoggerFactory loggerFactory, IConfiguration config)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }
            _config = config;
            _logger = loggerFactory.CreateLogger<EventController>();
            _logger.LogTrace("EventController start.");
        }


        // POST api/<controller>
        [HttpPost]
        public async Task<object> Post([FromBody]Object value)
        {

            /*
             * ([
  {
    "id": "788fbcbf-f516-4820-a8e2-b01d7aff8fea",
    "topic": "/subscriptions/44075180-60d2-4371-86ed-a168c1279e7f/resourceGroups/MusicBot/providers/Microsoft.EventGrid/topics/trivia-bot-grid",
    "subject": "",
    "data": {
      "validationCode": "01B597D6-C775-450C-BEC7-77B24D23B3C3",
      "validationUrl": "https://rp-westus2.eventgrid.azure.net/eventsubscriptions/triviaeventbot/validate?id=01B597D6-C775-450C-BEC7-77B24D23B3C3&t=2019-01-24T10:17:01.9089569Z&apiVersion=2018-09-15-preview&token=kAGLnCpJ23HpkqhRnY4cpXC1vshtoS9fpiYdcwptdBk%3d"
    },
    "eventType": "Microsoft.EventGrid.SubscriptionValidationEvent",
    "eventTime": "2019-01-24T10:17:01.9089569Z",
    "metadataVersion": "1",
    "dataVersion": "2"
  }
])

            {
  "validationResponse": "512d38b6-c7b8-40c8-89fe-f46f9e9622b6"
}
             */

            _logger.LogDebug("value >> " + JsonConvert.SerializeObject(value));
            //dynamic dataObject = value[0];

            
       
            //string response = string.Empty;
            string requestContent = JsonConvert.SerializeObject(value);//await req.Content.ReadAsStringAsync();

            EventGridSubscriber eventGridSubscriber = new EventGridSubscriber();

            // Optionally add one or more custom event type mappings
            eventGridSubscriber.AddOrUpdateCustomEventMapping("Badge.ItemReceived", typeof(NewBadgeReceivedEventData));

            var events = eventGridSubscriber.DeserializeEventGridEvents(requestContent);

            foreach (EventGridEvent receivedEvent in events)
            {
                if (receivedEvent.Data is SubscriptionValidationEventData)
                {
                    SubscriptionValidationEventData eventData = (SubscriptionValidationEventData)receivedEvent.Data;
                    _logger.LogDebug($"Got SubscriptionValidation event data, validationCode: {eventData.ValidationCode},  validationUrl: {eventData.ValidationUrl}, topic: {receivedEvent.Topic}");
                    // Handle subscription validation

                    return new ValidationResponse() { validationResponse = eventData.ValidationCode };

                }

                else if (receivedEvent.Data is NewBadgeReceivedEventData)
                {
                    NewBadgeReceivedEventData eventData = (NewBadgeReceivedEventData)receivedEvent.Data;

                    _logger.LogDebug($"Got a new badge event userName: {eventData.userName},  badgeName: {eventData.badgeName}, badgeIcon: {eventData.badgeIcon} , topic: {receivedEvent.Topic} ");
                    await notifyTeamMembers(eventData);
                }
                else
                {
                    string eventContent = JsonConvert.SerializeObject(receivedEvent);

                    _logger.LogDebug($" Got an unknown event >> " + eventContent);


                }
            }


            return String.Empty;
        }

        private async Task notifyTeamMembers(NewBadgeReceivedEventData eventData)
        {
            try
            {
                var connector = new ConnectorClient(new Uri(eventData.serviceUrl),
                        new MicrosoftAppCredentials(_config["MicrosoftAppId"], _config["MicrosoftAppPassword"]));
                var botAccount = new ChannelAccount(eventData.teamId, null, "bot");

                foreach (var member in eventData.teamMembers)
                {
                    var message = Activity.CreateMessageActivity() as Activity;
                    message.From = botAccount;
                    message.Text = $"Great news! {eventData.userName} has achieved the {eventData.badgeName} badge.";
                    message.Recipient = new ChannelAccount(member.userTeamsId, member.userName, "user", member.userAadId);
                    _logger.LogDebug($"About to send a message to {member.userName}");
                    try
                    {
                        await connector.Conversations.CreateDirectConversationAsync(botAccount, message.Recipient, message);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Error while attempting to send proactive conversation", e);
                    }
                }
            } catch(Exception ex)
            {
                _logger.LogError("General failure while processing proactive messages", ex);
            }            
        }

        private void notifyBadgeReceived(string id, NewBadgeReceivedEventData eventData)
        {
            throw new NotImplementedException();
        }
    }

    public class NewBadgeReceivedEventData
    {
        public string serviceUrl { get; set; }
        public string botId { get; set; }
        public string botName { get; set; }

        public string userId
        {
            get; set;
        }
        public string userName
        {
            get;set;
        }

        public string channelId
        {
            get; set;
        }

        public string badgeName
        {
            get; set;
        }

        public string badgeIcon
        {
            get; set;
        }

        public string teamId
        {
            get;
            set;
        }

        public TeamMember[] teamMembers
        {
            get;
            set;
        }
    }

    public class TeamMember
    {
        public TeamMember(ChannelAccount account)
        {
            userAadId = account.AadObjectId;
            userTeamsId = account.Id;
            userName = account.Name;
        }

        public string userAadId
        {
            get; set;
        }
        public string userTeamsId
        {
            get; set;
        }
        public string userName
        {
            get; set;
        }

        
    }

    public class ValidationResponse
    {
        public string validationResponse { get; set; }
    }

}
