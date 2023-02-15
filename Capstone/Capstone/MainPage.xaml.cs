using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Capstone
{
    public partial class MainPage : TabbedPage // #3 Use of Inheritance
    {
        private StackLayout content;
        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public StackLayout Content
        {
            get { return content;  }
            set { content = value;  }
        }

        public MainPage(string username)
        {
            this.Username = username;
            InitializeComponent();
            //BindingContext = new Models.BudgetModelView();
        }

        /********************
         ***** EXPENSES *****
         ********************/
        async void addBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Brings up new screen, with stuff to add for expense
            await Navigation.PushAsync(new AddExpense(this.Username));

        }

        void editBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Bring up new screen, sending over data for filling it in
            int expenseId = (int)((sender as Button)?.CommandParameter);
            Navigation.PushAsync(new UpdateExpense(expenseId, this.Username));
        }

        async void deleteBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            bool result = await DisplayAlert("Confirmation", "Are you sure you want to delete this item?", "Yes", "No");

            if (result)
            {
                int item = (int)((sender as Button)?.CommandParameter);
                // delete the item from the list using the item object
                var options = new SQLiteConnectionString(App.DatabaseLocation, true, "password",
                                postKeyAction: c =>
                                {
                                    c.Execute("PRAGMA cipher_compatibility=3");
                                });
                SQLiteConnection conn = new SQLiteConnection(options);
                conn.CreateTable<Expense>();
                int rows = conn.Delete<Expense>(item);
                if (rows > 0) DisplayAlert("Success", "Deleted Expense", "Ok");
                else DisplayAlert("Error", "Failed to delete expense", "Ok");
                OnAppearing();
            }
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
                Username = this.Username, 
                Date = nextPayDate.Date,
                BankAmount = float.Parse(moneyInAccount.Text),
                ExpenseTotal = getExpenseTotal(),
                Free2Spend = float.Parse(moneyInAccount.Text) - getExpenseTotal(),
                LastBudget = DateTime.Now
            };

            var options = new SQLiteConnectionString(App.DatabaseLocation, true, "password",
                            postKeyAction: c =>
                            {
                                c.Execute("PRAGMA cipher_compatibility=3");
                            });
            SQLiteConnection conn = new SQLiteConnection(options);
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
            var options = new SQLiteConnectionString(App.DatabaseLocation, true, "password",
                            postKeyAction: c =>
                            {
                                c.Execute("PRAGMA cipher_compatibility=3");
                            });
            SQLiteConnection conn = new SQLiteConnection(options);
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


            var options = new SQLiteConnectionString(App.DatabaseLocation, true, "password",
                            postKeyAction: c =>
                            {
                                c.Execute("PRAGMA cipher_compatibility=3");
                            });
            SQLiteConnection conn = new SQLiteConnection(options);
            conn.CreateTable<Budget>();
            List<Budget> budgetItems = conn.Table<Budget>().ToList();

            // Set to values
            if (budgetItems.Count > 0)
            {
                entryA1.Text = budgetItems[0].Date.ToString() ?? "";
                entryA2.Text = budgetItems[0].Username ?? "";
                entryA3.Text = budgetItems[0].Free2Spend.ToString() ?? "";
            }

            if (budgetItems.Count > 1)
            {
                entryB1.Text = budgetItems[1].Date.ToString() ?? "";
                entryB2.Text = budgetItems[1].Username ?? "";
                entryB3.Text = budgetItems[1].Free2Spend.ToString() ?? "";
            }

            if (budgetItems.Count > 2)
            {
                entryC1.Text = budgetItems[2].Date.ToString() ?? "";
                entryC2.Text = budgetItems[2].Username ?? "";
                entryC3.Text = budgetItems[2].Free2Spend.ToString() ?? "";
            }

            if (budgetItems.Count > 3)
            {
                entryD1.Text = budgetItems[3].Date.ToString() ?? "";
                entryD2.Text = budgetItems[3].Username ?? "";
                entryD3.Text = budgetItems[3].Free2Spend.ToString() ?? "";
            }

            if (budgetItems.Count > 4)
            {
                entryE1.Text = budgetItems[4].Date.ToString() ?? "";
                entryE2.Text = budgetItems[4].Username ?? "";
                entryE3.Text = budgetItems[4].Free2Spend.ToString() ?? "";
            }

            conn.Close();
        }

        void searchBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 11);

            switch(randomNumber)
            {
                case 1:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: A budget tells your money where to go instead of wondering where it went.\n\n" +
                        "Row 2: Failing to plan is planning to fail.\n\n" +
                        "Row 3: You must gain control over your money or the lack of it will forever control you.\n",
                        "Ok..");
                    break;
                case 2:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: Wealth is not about having a lot of money; it's about having a lot of options.\n\n" +
                        "Row 2: Budgeting is telling your money where to go, instead of wondering where it went.\n\n" +
                        "Row 3: The secret to wealth is simple: Find a way to do more for others than anyone else does.\n",
                        "Ok..");
                    break;
                case 3:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: A penny saved is a penny earned.\n\n" +
                        "Row 2: The biggest mistake people make is not making a budget, or sticking to one.\n\n" +
                        "Row 3: A budget is a plan for your money, just like a roadmap is a plan for your trip.\n",
                        "Ok..");
                    break;
                case 4:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: The greatest wealth is to live content with little.\n\n" +
                        "Row 2: If you want to be rich, you need to be financially literate.\n\n" +
                        "Row 3: The future belongs to those who prepare for it today.\n",
                        "Ok..");
                    break;
                case 5:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: The more you save, the more you have to invest, and the more you have to invest, the more you can earn.\n\n" +
                        "Row 2: It's not how much money you make, but how much money you keep, how hard it works for you, and how many generations you keep it for.\n\n" +
                        "Row 3: The habit of saving is itself an education; it fosters every virtue, teaches self-denial, cultivates the sense of order, trains to forethought, and so broadens the mind.\n",
                        "Ok..");
                    break;
                case 6:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: The biggest challenge in life is learning how to manage your finances.\n\n" +
                        "Row 2: The most important thing about budgeting is learning how to prioritize your spending.\n\n" +
                        "Row 3: Wealth is not a salary or a bank balance, it is a state of mind.\n",
                        "Ok..");
                    break;
                case 7:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: Budgeting is not about denying yourself the things you love, it's about making sure you can afford the things you love.\n\n" +
                        "Row 2: The best way to predict your financial future is to create it.\n\n" +
                        "Row 3: The habit of saving is the cornerstone of wealth.\n",
                        "Ok..");
                    break;
                case 8:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: Budgeting isn't about being cheap, it's about being smart.\n\n" +
                        "Row 2: You can't have financial peace if you have debt.\n\n" +
                        "Row 3: A goal without a plan is just a wish.\n",
                        "Ok..");
                    break;
                case 9:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: Money is like a sixth sense without which you cannot make a complete use of the other five.\n\n" +
                        "Row 2: Don't tell me what you value, show me your budget, and I'll tell you what you value.\n\n" +
                        "Row 3: Financial freedom is available to those who learn about it and work for it.\n",
                        "Ok..");
                    break;
                case 10:
                    DisplayAlert("AI Powered Quotes",
                        "Row 1: Successful people are simply those with successful habits.\n\n" +
                        "Row 2: Budgeting is about giving every dollar a job.\n\n" +
                        "Row 3: It's not how much money you make, but how much money you keep, how hard it works for you, and how many generations you keep it for.\n",
                        "Ok..");
                    break;
            }
        }





        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            //var tabbedPage = (TabbedPage)sender;
            //var currentPage = tabbedPage.CurrentPage;
            //var animation = new Animation(v => currentPage.TranslationX = v, 0, -1000);
            //danimation.Commit(currentPage, "PageSlideAnimation", 16, 1000, Easing.CubicOut);
        }

        protected override async void OnAppearing() // #1 Use of Polymorphism
        {
            base.OnAppearing();

            var options = new SQLiteConnectionString(App.DatabaseLocation, true, "password",
                            postKeyAction: c =>
                            {
                                c.Execute("PRAGMA cipher_compatibility=3");
                            });
            SQLiteConnection conn = new SQLiteConnection(options);
            conn.CreateTable<Expense>();
            conn.CreateTable<Budget>();

            // Find the latest budget and use info to update budget tab
            // Update the balance, due date, free to spend & last budget
            Budget budget = conn.Table<Budget>().OrderByDescending(x => x.Id).Where(x => x.Username == this.Username).FirstOrDefault();
            //moneyInAccount.Text = budget.BankAmount.ToString();
            //nextPayDate.Date = budget.Date.Date;
            //free2Spend.Text = budget.Free2Spend.ToString();
            //lastBudgetDate.Text = budget.LastBudget.ToString();

            // Update list of expenses
            //SQLiteConnection conn = new SQLiteConnection(WGU_Xamarin_Project.App.DatabaseLocation);
            //conn.CreateTable<Course>();
            var expenses = conn.Table<Expense>().Where(x => x.Username == this.Username).ToList();
            conn.Close();
            listExpenses.ItemsSource = expenses;


        }

    }
}

