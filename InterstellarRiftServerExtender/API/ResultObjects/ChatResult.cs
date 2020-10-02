using System.Collections.Generic;

namespace IRSE.API.ResultObjects
{
    public class ChatResult : BaseResult
    {
        public List<ChatMessage> ChatMessages { get; set; }
        public int LastMessageIndex { get { return ChatMessages.Count - 1; } }

        public ChatResult(bool error, string status, List<ChatMessage> messageList)
             : base(error, status)
        {
            ChatMessages = messageList;
        }
    }

    public class ChatMessage
    {
        public string Name { get; set; }
        public ulong ID { get; set; }
        public string Message { get; set; }

        public string Channel { get; set; }

        public ChatMessage(string name, string message, ulong id = 0UL, string channel = "All")
        {
            ID = id;
            Name = name;
            Message = message;
            Channel = channel;
        }

    }



    public class GetLastMessageIdResult : BaseResult
    {

        public int LastMessageID { get; set; }

        public GetLastMessageIdResult(bool error, string status, int lastId)
              : base(error, status)
        {
            LastMessageID = lastId;
        }
    }
}