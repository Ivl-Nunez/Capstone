using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

namespace Capstone
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        void loginBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Input validation (null, trim)
            if (username.Text == null || password.Text == null)
            {
                DisplayAlert("Error", "Username and Password required.", "Ok");
                return;
            }
            if (username.Text.Trim() == "" || password.Text.Trim() == "")
            {
                DisplayAlert("Error", "Username and Password required.", "Ok");
                return;
            }

            // Check for a match in the db
            var options = new SQLiteConnectionString(App.DatabaseLocation, true, "password",
                            postKeyAction: c =>
                            {
                                c.Execute("PRAGMA cipher_compatibility=3");
                            });
            SQLiteConnection conn = new SQLiteConnection(options);
            conn.CreateTable<Models.LoginModel>();
            string userName = username.Text;
            var result = conn.Table<Models.LoginModel>().Where(v => v.Username == username.Text && v.Password == password.Text).FirstOrDefault();
            if (result != null)
            {
                // Login pass creds
                Navigation.PushAsync(new MainPage(result.Username));
                conn.Close();
            }
            else
            {
                DisplayAlert("Error", "Incorrect Credentials", "Ok");
                conn.Close();
                return;
            }
        }

        void createBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            // Input validation (null, trim)
            if (username.Text == null || password.Text == null)
            {
                DisplayAlert("Error", "Username and Password required.", "Ok");
                return;
            }
            if (username.Text.Trim() == "" || password.Text.Trim() == "")
            {
                DisplayAlert("Error", "Username and Password required.", "Ok");
                return;
            }

            // Check to see if username already exists
            var options = new SQLiteConnectionString(App.DatabaseLocation, true, "password",
                            postKeyAction: c =>
                            {
                                c.Execute("PRAGMA cipher_compatibility=3");
                            });
            SQLiteConnection conn = new SQLiteConnection(options);
            conn.CreateTable<Models.LoginModel>();
            string userName = username.Text;
            var result = conn.Table<Models.LoginModel>().Where(v => v.Username == username.Text && v.Password == password.Text).FirstOrDefault();
            if (result != null)
            {
                DisplayAlert("Conflict", "User already exists", "Ok");
                conn.Close();
                return;
            }
            else
            {
                // Create a new login
                Models.LoginModel login = new Models.LoginModel()
                {
                    Username = username.Text,
                    Password = password.Text,
                };

                int rows = conn.Insert(login);
                if (rows > 0)
                {
                    Navigation.PushAsync(new MainPage(result.Username));
                }
                else
                {
                    DisplayAlert("Error", "Failed to create new account", "Ok");
                }
                conn.Close();
                return;
            }
        }

    }
}

