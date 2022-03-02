using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
	[Flags]
	public enum WebFunction
	{
		bug_Create = 1 << 0,
		bug_Edit = 1 << 1,
		bug_Delete = 1 << 2,
		bug_Query = 1 << 3,
		bug_Resolve = 1 << 4,

		feature_Create = 1 << 5,
		feature_Edit = 1 << 6,
		feature_Delete = 1 << 7,
		feature_Query = 1 << 8,
		feature_Resolve = 1 << 9,

		Create = bug_Create | feature_Create,
		Edit = bug_Edit | feature_Edit,
		Delete = bug_Delete | feature_Delete,
		Query = bug_Query | feature_Query,
		Resolve= bug_Resolve |feature_Resolve
	}
}
