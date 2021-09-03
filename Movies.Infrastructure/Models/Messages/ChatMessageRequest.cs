using Movies.Infrastructure.Models.Reviewer;

namespace Movies.Infrastructure.Models.Messages
{
    public class ChatMessageRequest
    {
        public ReviewerResponse Reviewer { get; set; }
        public string Message { get; set; }
        public int? ParentMessageId { get; set;  }
    }
}
