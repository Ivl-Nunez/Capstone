using System;
namespace Capstone
{
	public class Budget
	{
		private DateTime Date { get; set; }
		private float BankAmount { get; set; }
		private float ExpenseTotal { get; set; }

		public Budget(DateTime date, float bankAmount, float expenseTotal)
		{
			this.Date = date;
			this.BankAmount = bankAmount;
			this.ExpenseTotal = expenseTotal;
		}
	}
}

