using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
	public class ViewResult
	{
		public bool IsSuccess { get; set; }
		public string Msg { get; set; }
	}
	public class SResult: ViewResult
	{
		public SResult(string msg)
		{
			IsSuccess = true;
			Msg = msg;
		}
	}
	public class FResult : ViewResult
	{
		public FResult(string msg)
		{
			IsSuccess = false;
			Msg = msg;
		}
	}
}
