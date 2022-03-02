using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.Repository
{
	public class TicketRepository
	{
		string _connString;
		public TicketRepository(IConfiguration configuration)
		{
			_connString = configuration.GetConnectionString("Ticket");
		}

		public IDbConnection GetConnection()
		{
			return new SqlConnection(_connString);
		}

		public Account GetAccount(string acc)
		{
			using (var conn = GetConnection())
			{
				string sql = @"
				SELECT 
					[Id]
				  ,[Name]
				  ,[Role]
				  ,[LoginAccount]
				  ,[Password]
				FROM [dbo].[Account]
				WHERE LoginAccount=@acc
				";
				return conn.QueryFirstOrDefault<Account>(sql, new { acc });
			}
		}

		public IEnumerable<int> GetPermissions(int id)
		{
			using (var conn = GetConnection())
			{
				string sql = @"
				SELECT 
					[Code]
				FROM [Ticket].[dbo].[PermissionMap]
				WHERE RoleId=@id
				";
				return conn.Query<int>(sql, new { id });
			}
		}

		public Role GetRole(int id)
		{
			using (var conn = GetConnection())
			{
				string sql = @"
				SELECT 
					[Id]
				  ,[Name]
				FROM [dbo].[Role]
				WHere Id=@id
				";
				return conn.QueryFirstOrDefault<Role>(sql, new { id });
			}
		}

		public Tickets GetTicket(int id)
		{
			using (var conn = GetConnection())
			{
				string sql = @"
				SELECT 
					[Id]
					,[Type]
					,[Summary]
					,[Description]
					,[Status]
					,[CreateTime]
					,[CreateUser]
					,[UpdateTime]
					,[UpdateUser]
				FROM [dbo].[Tickets]
				WHere Id=@id
				";
				return conn.QueryFirstOrDefault<Tickets>(sql, new { id });
			}
		}

		public IEnumerable<Tickets> GetTickets(string user, string name)
		{
			using (var conn = GetConnection())
			{
				string sql = @"
				SELECT 
					 t.[Id]
					,t.[Type]
					,t.[Summary]
					,t.[Description]
					,t.[Status]
					,t.[CreateTime]
					,t.[CreateUser]
					,t.[UpdateTime]
					,t.[UpdateUser]
				FROM [Ticket].[dbo].[Account] a
				JOIN [dbo].[Role] b on a.Role=b.Id
				LEFT JOIN [dbo].[PermissionMap] c on b.Id=c.RoleId 
				JOIN [dbo].[Function] d on c.Code=d.Code and d.Action='Query'
				JOIN [Tickets] t on d.Type=t.Type
				WHERE a.LoginAccount=@user
				AND t.status < 3
				";

				if(!string.IsNullOrEmpty(name))
				{
					sql += " AND CreateUser=@name ";
				}

				return conn.Query<Tickets>(sql, new { user, name });
			}
		}

		public long Insert<T>(T entity) where T : class
		{
			try
			{
				using (var conn = GetConnection())
				{
					return conn.Insert(entity);
				}
			}
			catch(Exception ex)
			{
				return 0;
			}
		}
		public bool Update<T>(T entity) where T : class
		{
			try
			{
				using (var conn = GetConnection())
				{
					return conn.Update(entity);
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
