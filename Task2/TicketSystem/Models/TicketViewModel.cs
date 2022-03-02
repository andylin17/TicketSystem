using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
	public class TicketViewModel
	{
        public int Id { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}
