using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusBookingSystem.Models
{

	[Serializable]
	public class InvalidEmailOrPasswordException : Exception
	{
		public InvalidEmailOrPasswordException() { }
		public InvalidEmailOrPasswordException(string message) : base(message) { }
		public InvalidEmailOrPasswordException(string message, Exception inner) : base(message, inner) { }
		protected InvalidEmailOrPasswordException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}