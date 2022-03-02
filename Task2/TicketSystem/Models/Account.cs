using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
	[Table("Account")]
	public class Account
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Role { get; set; }
		public string LoginAccount { get; set; }
		public string Password { get; set; }
	}
}
