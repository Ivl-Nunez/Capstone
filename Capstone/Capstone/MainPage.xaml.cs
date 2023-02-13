using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace Capstone
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        /********************
         ***** EXPENSES *****
         ********************/
        async void addBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Brings up new screen, with stuff to add for expense
            await Navigation.PushAsync(new AddExpense());

        }


        /****************
         ***** HOME *****
         ****************/
        void budgetBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Input Validation
            if (moneyInAccount.Text == null) { DisplayAlert("Empty", "Please enter a bank balance", "Ok"); return; }
            if (moneyInAccount.Text.Trim() == "") { DisplayAlert("Empty", "Please enter a bank balance", "Ok"); return; }
            if (!(float.TryParse(moneyInAccount.Text, out float number))) { DisplayAlert("Error", "Balance needs to be a number", "Ok"); return; }

            // Create budget
            Budget budget = new Budget()
            {
                Date = nextPayDate.Date,
                BankAmount = float.Parse(moneyInAccount.Text),
                ExpenseTotal = getExpenseTotal(),
                Free2Spend = float.Parse(moneyInAccount.Text) - getExpenseTotal(),
                LastBudget = DateTime.Now
            };

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<Budget>();
            int rows = conn.Insert(budget);
            conn.Close();

            if (rows > 0)
            {
                DisplayAlert("Success", "Successfully added new budget", "Ok");
                free2Spend.Text = budget.Free2Spend.ToString();
                lastBudgetDate.Text = budget.LastBudget.ToString();
            }
            else DisplayAlert("Error", "Failed to make new budget", "Ok");
        }

        // Gets the total of expenses where the due date is between NOW & Next Pay
        float getExpenseTotal()
        {
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<Expense>();
            return conn.Table<Expense>()
                .Where(x => x.DueDate >= DateTime.Now && x.DueDate <= nextPayDate.Date)
                .Sum(x => x.Amount);
        }


        /*******************
         ***** REPORTS *****
         *******************/
        void pastBudgetsBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Needs to pull informatino regarding past budgets and display them in a table form (rows / columns)
            // (ability to generate reports with multiple columns, multiple rows, date-time stamp, and title)

            // If empty, should say something so user knows its not broken
            DisplayAlert("Past Budget", "You clicked to display past budgets", "Ok");
        }

        void searchBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Make sure there is something in the search field, if not, give error about empty search

            // Look throughout the fields shown and select the one/ones matching
            // If no matches, should dispay a mesage saying so
            DisplayAlert("Search", "You clicked to search", "Ok");
        }





        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            //var tabbedPage = (TabbedPage)sender;
            //var currentPage = tabbedPage.CurrentPage;
            //var animation = new Animation(v => currentPage.TranslationX = v, 0, -1000);
            //danimation.Commit(currentPage, "PageSlideAnimation", 16, 1000, Easing.CubicOut);
        }

    }
}

