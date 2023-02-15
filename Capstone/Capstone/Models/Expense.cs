using System;
using SQLite;

namespace Capstone
{
	public class Expense
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public DateTime DueDate { get; set; }
	}
}

