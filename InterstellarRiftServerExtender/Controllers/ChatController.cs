using IRSE.Managers;
using IRSE.ResultObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace IRSE.Controllers
{
    public class ChatController : ApiController
    {
        [HttpGet]
        public ChatResult SendChatMessage(string message)
        {
            try
            {
                ServerInstance.Instance.Handlers.ChatHandler.SendMessageFromServer(message);

                return new ChatResult(false, "Message Sent!", new List<ChatMessage>());
            }
            catch (Exception ex)
            {
                return new ChatResult(true, ex.ToString(), new List<ChatMessage>());
            }
        }

        [HttpGet]
        public ChatResult ClearOutAllMessages()
        {
            try
            {
                ServerInstance.Instance.Handlers.ChatHandler.ChatMessages = new List<ChatMessage>();

                return new ChatResult(false, "All messages cleared out!", new List<ChatMessage>());
            }
            catch (Exception ex)
            {
                return new ChatResult(true, ex.ToString(), new List<ChatMessage>());
            }
        }

        [HttpGet]
        public GetLastMessageId GetLastMessageID()
        {
            int lastID = ServerInstance.Instance.Handlers.ChatHandler.ChatMessages.Count - 1;
            string message = null;
            if (lastID >= 0)
                message = "Last message ID is " + lastID;
            else
                message = "There is currently no message in the list";
            return new GetLastMessageId(false, message, lastID);
        }

        [HttpGet]
        public ChatResult GetChatMessagesFrom(int id)
        {
            try
            {
                if (id < 0)
                    return new ChatResult(true, "Id can't be smaller than 0", new List<ChatMessage>());
                if (ServerInstance.Instance.Handlers.ChatHandler.ChatMessages.Count < id)
                    return new ChatResult(true, "Id is greater than last message Id", new List<ChatMessage>());
                if (ServerInstance.Instance.Handlers.ChatHandler.ChatMessages.Count == id)
                    return new ChatResult(false, "No new messages", new List<ChatMessage>());

                var messageList = new List<ChatMessage>();

                for (int i = id; i < ServerInstance.Instance.Handlers.ChatHandler.ChatMessages.Count; i++)
                {
                    ChatMessage curChatMessage = ServerInstance.Instance.Handlers.ChatHandler.ChatMessages[i];

                    messageList.Add(new ChatMessage(curChatMessage.Name, curChatMessage.Message));
                }

                return new ChatResult(false, "Success", messageList);
            }
            catch (Exception ex)
            {
                return new ChatResult(true, ex.ToString(), new List<ChatMessage>());
            }
        }
    }
}