using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using TicketSystem.Models;
using TicketSystem.Repository;

namespace TicketSystem.Services
{
	public class TicketService
	{
		TicketRepository _repository;
		public TicketService(TicketRepository TicketRepository)
		{
			_repository = TicketRepository;
		}

		public Account GetAccount(string account)
		{
			return _repository.GetAccount(account);
		}

		public IEnumerable<int> GetPermissions(int id)
		{
			return _repository.GetPermissions(id).Select(x => (int)Math.Pow(2, x-1));
		}

		public IEnumerable<Tickets> GetTickets(string user, string name)
		{
			return _repository.GetTickets(user, name);
		}
		public TicketViewModel GetTicket(int id)
		{
			var entity = _repository.GetTicket(id);

			var model = new TicketViewModel
			{
				Id = entity.Id,
				Summary = entity.Summary,
				Description = entity.Description,
				Type = entity.Type
			};
			return model;
		}

		public ViewResult CreateTicket(TicketViewModel insert, string user)
		{
			var model = new Tickets
			{
				Type = insert.Type,
				Summary = insert.Summary,
				Description = insert.Description,
				CreateTime = DateTime.Now,
				CreateUser = user,
				Status = 0
			};

			var result =  _repository.Insert(model);

			if (result > 0)
				return new SResult("Create Success!");
			else
				return new FResult("Create Fail!");
		}

		public ViewResult UpdateTicket(TicketViewModel insert, string user)
		{
			var entity = _repository.GetTicket(insert.Id);
			entity.Summary = insert.Summary;
			entity.Description = insert.Description;
			entity.UpdateUser = user;
			entity.UpdateTime = DateTime.Now;
			var result = _repository.Update(entity);

			if (result)
				return new SResult("Create Success!");
			else
				return new FResult("Create Fail!");
		}

		public ViewResult UpdateTicketStatus(int id, string user, int status)
		{
			var entity = _repository.GetTicket(id);
			entity.Status = (EnumTicketStatus)status;
			entity.UpdateUser = user;
			entity.UpdateTime = DateTime.Now;
			var result = _repository.Update(entity);

			if (result)
				return new SResult("Create Success!");
			else
				return new FResult("Create Fail!");
		}

		public bool HasAuth(ClaimsPrincipal user, WebFunction function)
		{
			var json = user.Claims.FirstOrDefault(x => x.Type == "p")?.Value;
			if (string.IsNullOrWhiteSpace(json))
				return false;

			var permissions = JsonSerializer.Deserialize<int[]>(json);
			var auth = permissions.Any(x => function.HasFlag((WebFunction)x));

			return auth;
		}
	}
}
