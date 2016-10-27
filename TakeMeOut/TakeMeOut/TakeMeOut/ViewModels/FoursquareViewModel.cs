using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FourSquare.Response;
using Newtonsoft.Json;
using SQLite.Net;
using TakeMeOut;
using XamarinForms.Models;

namespace XamarinForms.ViewModels
{
    public class FoursquareViewModel : INotifyPropertyChanged
    {

        // use this link to get an api_key : https://foursquare.com/developers/register/
        private const string ClientId = "X12DV0HUDW1WDH2LTI3EAGPINK1TFF5FJCSN5WAJV5OQJP2I";
        private const string ClientSecret = "ZPH15443VIP5D5LHNYXA1BO0AV2ZZQJ2FXRCTTM4L3ODYAXT";
        private const string v = "20160611";
        private const string venueId = "40a55d80f964a52020f31ee3";

		DatabaseManager mgr = new DatabaseManager();

        // doc : https://developer.foursquare.com/docs/venues/search
        private string apiUrlForVenues = $"https://api.foursquare.com/v2/venues/explore?near=copenhagen&client_id={ClientId}&client_secret={ClientSecret}&v={v}&radius=1000&venuePhotos=1&categoryId=4bf58dd8d48988d1c9941735";

        private FoursquareVenues _foursquareVenues;

        public FoursquareVenues FoursquareVenues
        {
            get { return _foursquareVenues; }
            set
            {
                _foursquareVenues = value;
                OnPropertyChanged();
            }
        }

        public FoursquareViewModel()
        {
			// Uncomment this line to add pre-filled categeories to local database
			//AddDataToDatabase();
            InitDataAsync();
        }

		public void AddDataToDatabase()
		{
			
			mgr.SaveItem(new Categories()
			{
				Name = "Go Cart",
				MainCategoryId = 1,
				CategoryId = "52e81612bcbc57f1066b79ea",
				Radius = 1000
			});

			mgr.SaveItem(new Categories()
			{
				Name = "Historic Site",
				MainCategoryId = 1,
				CategoryId = "4deefb944765f83613cdba6e",
				Radius = 1000
			});

			mgr.SaveItem(new Categories()
			{
				Name = "Botanical Garden",
				MainCategoryId = 2,
				CategoryId = "52e81612bcbc57f1066b7a22",
				Radius = 1000
			});
		}

		private void ShowCategoriesToUser()
		{
			var result = mgr.GetCategories();

			
		}

		private async void OnDropdownItemSelected(object sender, EventArgs e)
		{
			var selection = (Categories)e;
			var selectedCategoryId = selection.CategoryId;
			var catObject = mgr.GetCategory(selectedCategoryId);

			FourSquareService squareApi = new FourSquareService(ClientId, ClientSecret);

			//// Search venues
			var venues = await squareApi.SearchVenues(new Dictionary<string, string>
			{
				{ "near","copenhegen" },
				{ "radius",catObject.Radius.ToString()},
				{ "categoryId",selectedCategoryId}, //"4bf58dd8d48988d1c9941735"
			});
		}

		public async Task InitDataAsync(string location, string selectedCategory)
		{
			var result = mgr.GetCategories();
			var categoryToFind = result.FirstOrDefault(c => c.CategoryId == "4deefb944765f83613cdba6e");



			FourSquareService squareApi = new FourSquareService(ClientId, ClientSecret);

			//// Search venues
			var venues = await squareApi.SearchVenues(new Dictionary<string, string>
			{
				{ "near",location },
				{ "radius",categoryToFind.Radius.ToString()},
				{ "categoryId",selectedCategory}, //"4bf58dd8d48988d1c9941735"
			});
		
			// Explore venues
			var venuesExplore = await squareApi.ExploreVenues(new Dictionary<string, string>
			{
				{ "near","copenhagen" },
				{ "radius",categoryToFind.Radius.ToString()},
				{ "categoryId",categoryToFind.CategoryId}, //"4bf58dd8d48988d1c9941735"
			});

            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(apiUrlForVenues);
            FoursquareVenues = JsonConvert.DeserializeObject<FoursquareVenues>(json);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}