using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaBotT5.Models
{

    public class TeamRosterResponseModel
    {
        public bool success;
        public string message;
    }

    public class QuestionOptionModel
    {
        public int id;
        public string text;
    }

    public class QuestionModel
    {
        public int id;
        public string text;
        public QuestionOptionModel[] questionOptions;
    }

    public class AnswerResponseModel
    {
        public bool correct;
        public string achievementBadge;
        public string achievementBadgeIcon;
    }


    public class GridEvent<T> where T : class
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string EventType { get; set; }
        public T Data { get; set; }
        public DateTime EventTime { get; set; }
    }

}
