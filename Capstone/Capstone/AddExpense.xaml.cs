using System;
using System.Collections.Generic;

using Xamarin.Forms;

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
            // Make sure name and amount are not empty
            // make sure amount is a number

        }

        void cancelBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}

