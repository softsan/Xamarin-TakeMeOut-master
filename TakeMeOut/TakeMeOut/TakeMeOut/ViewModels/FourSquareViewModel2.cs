using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinForms.Models;

namespace XamarinForms.ViewModels
{
    public class FoursquareViewModel2 : INotifyPropertyChanged
    {

        // use this link to get an api_key : https://foursquare.com/developers/register/
        private const string ClientId = "X12DV0HUDW1WDH2LTI3EAGPINK1TFF5FJCSN5WAJV5OQJP2I";
        private const string ClientSecret = "ZPH15443VIP5D5LHNYXA1BO0AV2ZZQJ2FXRCTTM4L3ODYAXT";
        private const string v = "20160611";
        private const string venueId = "40a55d80f964a52020f31ee3";



        // doc : https://developer.foursquare.com/docs/venues/search
        private string apiUrlForVenues = $"https://api.foursquare.com/v2/venues/explore?near=aalborg&client_id={ClientId}&client_secret={ClientSecret}&v={v}&radius=1000&venuePhotos=1&categoryId=4bf58dd8d48988d116941735";

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

        public FoursquareViewModel2()
        {
            InitDataAsync();
        }

        public async Task InitDataAsync()
        {

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
}