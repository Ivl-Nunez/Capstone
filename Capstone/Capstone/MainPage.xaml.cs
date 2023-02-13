using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Capstone
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // EXPENSES
        async void addBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Brings up new screen, with stuff to add for expense
            await Navigation.PushAsync(new AddExpense());

        }


        // HOME
        void budgetBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // This is pretty much it.

            // Make sure that money in account is not empty and is a number if not give error (only input validation necessary on this page)


            //int money = int32.Parse(moneyInAccount.Text);
            //int totalExpenses = 0; //Need to pull total through db using nextpaydate for a filter

            // Free 2 spend = money - totalExpenses
            // lastBudget = DateTime.Now();

            DisplayAlert("Budget Update", "You've updated your budget!", "Sweet");
        }


        // REPORTS
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
            //animation.Commit(currentPage, "PageSlideAnimation", 16, 1000, Easing.CubicOut);
        }

    }
}

