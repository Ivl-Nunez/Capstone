using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Capstone
{
    public partial class App : Application // #2 Use of Inheritance
    {
        public static string DatabaseLocation = string.Empty;
        public App ()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

