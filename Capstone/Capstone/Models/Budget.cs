using System;
using SQLite;

namespace Capstone
{
	public class Budget
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public float BankAmount { get; set; }
		public float ExpenseTotal { get; set; }
		public float Free2Spend { get; set; }
		public DateTime LastBudget { get; set; }

		public Budget()
		{

		}
	}
}

