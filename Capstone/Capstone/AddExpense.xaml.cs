using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Capstone
{	
	public partial class AddExpense : ContentPage
	{	
		public AddExpense ()
		{
			InitializeComponent ();
		}

        void addBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Input Validation
            if (expenseName.Text == null || expenseAmount.Text == null || expenseDueDate == null)
            {
                DisplayAlert("Error", "One or more fields are empty.", "Error");
                return;
            }
            if (expenseName.Text.Trim() == "")
            {
                DisplayAlert("Empty", "Please enter a name for the expense.", "Ok");
                return;
            }
            if (expenseAmount.Text.Trim() == "")
            {
                DisplayAlert("Empty", "Please enter an amount for the expense", "Ok");
                return;
            }
            if (!(float.TryParse(expenseAmount.Text, out float number)))
            {
                DisplayAlert("Amount Error", "Please enter a number for the expense", "Ok");
                return;
            }

            // Create expense
            Expense expense = new Expense()
            {
                Name = string.IsNullOrEmpty(expenseName.Text) ? "" : expenseName.Text,
                Amount = float.Parse(string.IsNullOrEmpty(expenseAmount.Text) ? "0" : expenseAmount.Text),
                DueDate = expenseDueDate.Date,
            };

            // Create connection & add expense
            try
            {
                SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
                conn.CreateTable<Expense>();
                int rows = conn.Insert(expense);
                conn.Close();

                // Verify success / failure
                if (rows > 0)
                    DisplayAlert("Success", "Expense added!", "Ok");
                else
                    DisplayAlert("Error", "Error adding expense", "Ok");
            }
            catch
            {
                DisplayAlert("DB Error", "Error inserting an expense into the db", "Ok");
            }
        }

        void cancelBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}

