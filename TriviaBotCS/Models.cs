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

}
