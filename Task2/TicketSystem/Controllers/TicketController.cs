using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Filter;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
	[Authorize(Roles = "User")]
	public class TicketController : Controller
	{
		TicketService _service;
		public TicketController(TicketService TicketService)
		{
			_service = TicketService;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[PageAuthorize(WebFunction.bug_Query)]
		public ActionResult Get(string name)
		{
			var table = _service.GetTickets(User.Identity.Name, name);
			return PartialView("_Table", table);
		}
		[PageAuthorize(WebFunction.bug_Create)]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[PageAuthorize(WebFunction.bug_Create)]
		public ActionResult Create(TicketViewModel input)
		{
			var result = _service.CreateTicket(input, User.Identity.Name);
			return Json(result);
		}
		[PageAuthorize(WebFunction.bug_Edit)]
		public ActionResult Edit(int id)
		{
			var model = _service.GetTicket(id);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[PageAuthorize(WebFunction.bug_Edit)]
		public ActionResult Edit(TicketViewModel input)
		{
			var result = _service.UpdateTicket(input, User.Identity.Name);
			return Json(result);
		}

		[HttpPost]
		[PageAuthorize(WebFunction.bug_Delete)]
		public ActionResult Delete(int id)
		{
			var result = _service.UpdateTicketStatus(id, User.Identity.Name, 3);
			return Json(result);
		}

		[HttpPost]
		[PageAuthorize(WebFunction.bug_Resolve)]
		public ActionResult Resolve(int id)
		{
			var result = _service.UpdateTicketStatus(id, User.Identity.Name, 2);
			return Json(result);
		}
	}
}
