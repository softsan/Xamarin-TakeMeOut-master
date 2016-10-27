using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;
using XamarinForms.Views;

namespace TakeMeOut
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private async void AreaButton_OnClicked(object sender, EventArgs e)
        {
           
                await Navigation.PushAsync(new HomePage());

            }

            

        }
}
