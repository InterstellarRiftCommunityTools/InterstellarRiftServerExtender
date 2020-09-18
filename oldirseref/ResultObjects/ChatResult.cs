
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRSE.Managers.Handlers;

namespace IRSE.ResultObjects
{
	public class ChatResult
	{
		public bool Error { get; set; }
		public string Status { get; set; }
		public List<ChatMessage> ChatMessages { get; set;}
		public int LastMessageIndex { get {return ChatMessages.Count - 1; } }

		public ChatResult(bool error, string status, List<ChatMessage> messageList)
		{

			Error = error;
			Status = status;
			ChatMessages = messageList;
		}
	}

	public class GetLastMessageId
	{
		public bool Error { get; set; }
		public string Status { get; set; }
		public int LastMessageID { get; set; }

		public GetLastMessageId(bool error, string status, int lastId)
		{
			Error = error;
			Status = status;
			LastMessageID = lastId;
		}
	}
	
}
