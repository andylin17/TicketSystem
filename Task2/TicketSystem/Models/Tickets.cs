using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    [Table("Tickets")]
    public class Tickets
	{
        public int Id { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public EnumTicketStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateUser { get; set; }
    }
}
