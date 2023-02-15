using System;
using SQLite;

namespace Capstone.Models
{
	public class LoginModel
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}

