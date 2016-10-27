using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinForms.Views;

namespace TakeMeOut
{
    public partial class CategoryPage : ContentPage
    {
        public CategoryPage()
        {
            InitializeComponent();
            MainListView.ItemsSource = new List<CategoryList>
            {
                new CategoryList
                {
               
                    ImageUrl = "rest8.png"

                },
                    new CategoryList
                {
                    
                    ImageUrl = "rest4.png"

                },
                        new CategoryList
                {

                    ImageUrl = "rest3.png"

                },
                    new CategoryList
                {
                   
                    ImageUrl = "rest5.png"


                }
                    ,
                        new CategoryList
                {

                    ImageUrl = "rest6.png"

                },
                    new CategoryList
                {

                    ImageUrl = "rest7.png"


                }

            };
             
    }

        private async void ListButton_OnTapped(object sender, EventArgs e)
        {

           
                await Navigation.PushAsync(new FoursquareViewPage());
           
            }
           
    }
}
