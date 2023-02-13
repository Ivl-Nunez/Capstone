using System;
namespace Capstone
{
	public class Expense
	{
		private string Name { get; set; }
		private int Amount { get; set; }
		private DateTime DueDate { get; set; }

		public Expense(string name, int amount, DateTime dueDate)
		{
			this.Name = name;
			this.Amount = amount;
			this.DueDate = dueDate;
		}
	}
}

