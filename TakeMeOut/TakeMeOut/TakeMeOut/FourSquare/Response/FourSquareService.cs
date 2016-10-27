using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using FourSquare.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FourSquare.Response;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;


namespace FourSquare.Response 
{
    public class FourSquareService
    {
        private enum HttpMethod
        {
            GET,
            POST
        }

        private class AccessToken
        {
            public string access_token;
        }

        private string clientId = "X12DV0HUDW1WDH2LTI3EAGPINK1TFF5FJCSN5WAJV5OQJP2I";
        private string clientSecret = "ZPH15443VIP5D5LHNYXA1BO0AV2ZZQJ2FXRCTTM4L3ODYAXT";
        private string accessToken = null;
        private string authenticateUrl = "https://foursquare.com/oauth2/authenticate";
        private string accessTokenUrl = "https://foursquare.com/oauth2/access_token";
        private string apiUrl = "https://api.foursquare.com/v2";
        private string apiVersion = "20140101";

		public FourSquareService()
		{
		}

        public FourSquareService(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        public FourSquareService(string clientId, string clientSecret, string accessToken)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.accessToken = accessToken;
        }

        private string Request(string url, HttpMethod httpMethod)
        {
            return Request(url, httpMethod, null);
        }

        private string Request(string url, HttpMethod httpMethod, string data)
        {
		    string result = string.Empty;
            return result;
        }

		public async Task<string> RequestGetAsync(string requestUrl,string data)
		{
				using (var client = new HttpClient())
				{
					return await client.GetStringAsync(requestUrl);
				}
		}

		public async Task<string> RequestPostAsync(string requestUrl, string data)
		{
			string responseBody = string.Empty;
			ByteArrayContent content = null;
			if (!string.IsNullOrWhiteSpace(data))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(data.ToString());
				content = new ByteArrayContent(bytes);
				content.Headers.ContentLength = bytes.Length;

			}

			using (var client = new HttpClient())
			{
				var response = await client.PostAsync(requestUrl, content);
				if (response.IsSuccessStatusCode)
				{
					responseBody = await response.Content.ReadAsStringAsync();
					//Debug.WriteLine(responseBody);
				}
			}

			return responseBody;
		}

        private string SerializeDictionary(Dictionary<string, string> dictionary)
        {
            StringBuilder parameters = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            return parameters.Remove(parameters.Length - 1, 1).ToString();
        }

        private async Task<FourSquareSingleResponse<T>> GetSingle<T>(string endpoint) where T : FourSquareEntity
        {
            return await GetSingle<T>(endpoint, null, false);
        }

        private async Task<FourSquareSingleResponse<T>> GetSingle<T>(string endpoint, bool unauthenticated) where T : FourSquareEntity
        {
            return await GetSingle<T>(endpoint, null, unauthenticated);
        }

        private async Task<FourSquareSingleResponse<T>> GetSingle<T>(string endpoint, Dictionary<string, string> parameters) where T : FourSquareEntity
        {
            return await GetSingle<T>(endpoint, parameters, false);
        }

        private async Task<FourSquareSingleResponse<T>> GetSingle<T>(string endpoint, Dictionary<string, string> parameters, bool unauthenticated) where T : FourSquareEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "&" + SerializeDictionary(parameters);
            }

            string oauthToken = "";
            if (unauthenticated)
            {
                oauthToken = string.Format("client_id={0}&client_secret={1}", clientId, clientSecret);
            }
            else
            {
                oauthToken = string.Format("oauth_token={0}", accessToken);
            }

			var json = await RequestGetAsync(string.Format("{0}{1}?{2}{3}&v={4}", apiUrl, endpoint, oauthToken, serializedParameters, apiVersion), null);
            FourSquareSingleResponse<T> fourSquareResponse = JsonConvert.DeserializeObject<FourSquareSingleResponse<T>>(json);
            return fourSquareResponse;
        }

		private async Task<XamarinForms.Models.FoursquareVenues> GetSingle(string endpoint, Dictionary<string, string> parameters, bool unauthenticated)  
		{
			string serializedParameters = "";
			if (parameters != null)
			{
				serializedParameters = "&" + SerializeDictionary(parameters);
			}

			string oauthToken = "";
			if (unauthenticated)
			{
				oauthToken = string.Format("client_id={0}&client_secret={1}", clientId, clientSecret);
			}
			else
			{
				oauthToken = string.Format("oauth_token={0}", accessToken);
			}

			var json = await RequestGetAsync(string.Format("{0}{1}?{2}{3}&v={4}", apiUrl, endpoint, oauthToken, serializedParameters, apiVersion), null);
			var fourSquareResponse = JsonConvert.DeserializeObject<XamarinForms.Models.FoursquareVenues>(json);
			return fourSquareResponse;
		}

        private async Task<FourSquareMultipleResponse<T>> GetMultiple<T>(string endpoint) where T : FourSquareEntity
        {
            return await GetMultiple<T>(endpoint, null, false);
        }

        private async Task<FourSquareMultipleResponse<T>> GetMultiple<T>(string endpoint, bool unauthenticated) where T : FourSquareEntity
        {
            return await GetMultiple<T>(endpoint, null, unauthenticated);
        }

        private async Task<FourSquareMultipleResponse<T>> GetMultiple<T>(string endpoint, Dictionary<string, string> parameters) where T : FourSquareEntity
        {
            return await GetMultiple<T>(endpoint, parameters, false);
        }

        private async Task<FourSquareMultipleResponse<T>> GetMultiple<T>(string endpoint, Dictionary<string, string> parameters, bool unauthenticated) where T : FourSquareEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "&" + SerializeDictionary(parameters);
            }

            string oauthToken = "";
            if (unauthenticated)
            {
                oauthToken = string.Format("client_id={0}&client_secret={1}", clientId, clientSecret);
            }
            else
            {
                oauthToken = string.Format("oauth_token={0}", accessToken);
            }

            var json = await RequestGetAsync(string.Format("{0}{1}?{2}{3}&v={4}", apiUrl, endpoint, oauthToken, serializedParameters, apiVersion),null);
            FourSquareMultipleResponse<T> fourSquareResponse = JsonConvert.DeserializeObject<FourSquareMultipleResponse<T>>(json);
            return fourSquareResponse;
        }

        #region CustomCode

        private async Task<FourSquareMultipleVenuesResponse<T>> GetMultipleVenues<T>(string endpoint) where T : FourSquareEntity
        {
            return await GetMultipleVenues<T>(endpoint, null, false);
        }

        private async Task<FourSquareMultipleVenuesResponse<T>> GetMultipleVenues<T>(string endpoint, bool unauthenticated) where T : FourSquareEntity
        {
            return await GetMultipleVenues<T>(endpoint, null, unauthenticated);
        }

        private async Task<FourSquareMultipleVenuesResponse<T>> GetMultipleVenues<T>(string endpoint, Dictionary<string, string> parameters) where T : FourSquareEntity
        {
            return await GetMultipleVenues<T>(endpoint, parameters, false);
        }

		private async Task<FourSquareMultipleVenuesResponse<T>> GetMultipleVenues<T>(string endpoint, Dictionary<string, string> parameters, bool unauthenticated) where T : FourSquareEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "&" + SerializeDictionary(parameters);
            }

            string oauthToken = "";
            if (unauthenticated)
            {
                oauthToken = string.Format("client_id={0}&client_secret={1}", clientId, clientSecret);
            }
            else
            {
                oauthToken = string.Format("oauth_token={0}", accessToken);
            }

            string json = await RequestGetAsync(string.Format("{0}{1}?{2}{3}&v={4}", apiUrl, endpoint, oauthToken, serializedParameters, apiVersion),null);
            var fourSquareResponse = JsonConvert.DeserializeObject<FourSquareMultipleVenuesResponse<T>>(json);
            return fourSquareResponse;
        }

        #endregion

        private void Post(string endpoint)
        {
            Post(endpoint, null);
        }

        private void Post(string endpoint, Dictionary<string, string> parameters)
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "&" + SerializeDictionary(parameters);
            }

            string json = Request(string.Format("{0}{1}?oauth_token={2}{3}&v={4}", apiUrl, endpoint, accessToken, serializedParameters, apiVersion), HttpMethod.POST);
        }

        private FourSquareSingleResponse<T> Post<T>(string endpoint) where T : FourSquareEntity
        {
            string serializedParameters = "";

            string json = Request(string.Format("{0}{1}?oauth_token={2}{3}&v={4}", apiUrl, endpoint, accessToken, serializedParameters, apiVersion), HttpMethod.POST);
            FourSquareSingleResponse<T> fourSquareResponse = JsonConvert.DeserializeObject<FourSquareSingleResponse<T>>(json);
            return fourSquareResponse;
        }

        private FourSquareSingleResponse<T> Post<T>(string endpoint, Dictionary<string, string> parameters) where T : FourSquareEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "&" + SerializeDictionary(parameters);
            }

            string json = Request(string.Format("{0}{1}?oauth_token={2}{3}&v={4}", apiUrl, endpoint, accessToken, serializedParameters, apiVersion), HttpMethod.POST);
            FourSquareSingleResponse<T> fourSquareResponse = JsonConvert.DeserializeObject<FourSquareSingleResponse<T>>(json);
            return fourSquareResponse;
        }

        private JObject PostJObject(string endpoint, Dictionary<string, string> parameters)
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "&" + SerializeDictionary(parameters);
            }

            string json = Request(string.Format("{0}{1}?oauth_token={2}{3}&v={4}", apiUrl, endpoint, accessToken, serializedParameters, apiVersion), HttpMethod.POST);
            return JObject.Parse(json);
        }

        public string GetAuthenticateUrl(string redirectUri)
        {
            return string.Format("{0}?client_id={1}&response_type=code&redirect_uri={2}", authenticateUrl, clientId, redirectUri);
        }

        public async Task<string> GetAccessToken(string redirectUri, string code)
        {
            string url = string.Format("{0}?client_id={1}&client_secret={2}&grant_type=authorization_code&redirect_uri={3}&code={4}", accessTokenUrl, clientId, clientSecret, redirectUri, code);
			var json = await RequestGetAsync(url,null);
            AccessToken accessToken = JsonConvert.DeserializeObject<AccessToken>(json);
            SetAccessToken(accessToken.access_token);
            return accessToken.access_token;
        }

        public void SetAccessToken(string accessToken)
        {
            this.accessToken = accessToken;
        }

        #region User
        // User
        public async Task<User> GetUser(string userId)
        {
			var result = await GetSingle<User>("/users/" + userId);
			return result.response["user"];
        }

        public async Task<List<User>> SearchUsers(Dictionary<string, string> parameters)
        {
			var result = await GetMultiple<User>("/users/search", parameters);
			return result.response["results"];
        }

        public async Task<List<User>> GetUserRequests()
        {
			var result =  await GetMultiple<User>("/users/requests");
			return result.response["requests"];
        }

        public async Task<List<Checkin>> GetUserCheckins(string userId)
        {
            return await GetUserCheckins(userId, null);
        }

        public async Task<List<Checkin>> GetUserCheckins(string userId, Dictionary<string, string> parameters)
        {
			var result = await GetSingle<FourSquareEntityItems<Checkin>>("/users/" + userId + "/checkins", parameters);
			FourSquareEntityItems<Checkin> checkins = result.response["checkins"];
            return checkins.items;
        }

        public async Task<List<User>> GetUserFriends(string userId)
        {
            return await GetUserFriends(userId, null);
        }

        public async Task<List<User>> GetUserFriends(string userId, Dictionary<string, string> parameters)
        {
			var result = await GetSingle<FourSquareEntityItems<User>>("/users/" + userId + "/friends", parameters);
            FourSquareEntityItems<User> friends = result.response["friends"];
            return friends.items;
        }

        public async Task<List<VenueHistory>> GetUserVenueHistory()
        {
			return await GetUserVenueHistory(null);
        }

        public async Task<List<VenueHistory>> GetUserVenueHistory(Dictionary<string, string> parameters)
        {
			var result = await GetSingle<FourSquareEntityItems<VenueHistory>>("/users/self/venuehistory");
            FourSquareEntityItems<VenueHistory> venues = result.response["venues"];

            return venues.items;
        }

        /// <summary>
        /// https://api.foursquare.com/v2/users/USER_ID/request
        /// Sends a friend request to another user.
        /// </summary>
        public User SendUserRequest(string userId)
        {
            return Post<User>("/users/" + userId + "/request").response["user"];
        }

        /// <summary>
        /// https://api.foursquare.com/v2/users/USER_ID/unfriend
        /// </summary>
        public User SendUserUnfriend(string userId)
        {
            return Post<User>("/users/" + userId + "/unfriend").response["user"];
        }

        /// <summary>
        /// https://api.foursquare.com/v2/users/USER_ID/approve
        /// </summary>
        public User SendUserApprove(string userId)
        {
            return Post<User>("/users/" + userId + "/approve").response["user"];
        }

        /// <summary>
        /// https://api.foursquare.com/v2/users/USER_ID/deny
        /// </summary>
        public User SendUserDeny(string userId)
        {
            return Post<User>("/users/" + userId + "/deny").response["user"];
        }

        /// <summary>
        /// https://api.foursquare.com/v2/users/USER_ID/setpings
        /// </summary>
        public User SetUserPings(string userId, string value)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("value", value);

            return Post<User>("/users/" + userId + "/setpings", parameters).response["user"];
        }

        #endregion

        //Venue
        public async Task<Venue> GetVenue(string venueId)
        {
			var result = await GetSingle<Venue>("/venues/" + venueId, true);
            return result.response["venue"];
        }

        public Venue AddVenue(Dictionary<string, string> parameters)
        {
            return Post<Venue>("/venues/add", parameters).response["venue"];
        }

        public async Task<List<Category>> GetVenueCategories()
        {
			var result = await GetMultiple<Category>("/venues/categories", true);
            return result.response["categories"];
        }

        public async Task<XamarinForms.Models.FoursquareVenues> ExploreVenues(Dictionary<string, string> parameters)
        {
			var result  = await GetSingle("/venues/explore", parameters, true);
			return result;
			 
        }
         

        public async Task<List<Venue>> GetManagedVenues()
        {
            return  await GetManagedVenues(null);
        }

        public async Task<List<Venue>> GetManagedVenues(Dictionary<string, string> parameters)
        {
			var result = await GetMultiple<Venue>("/venues/managed", parameters, false);
            return result.response["venues"];
        }

        public async Task<List<Venue>> SearchVenues(Dictionary<string, string> parameters)
        {
			var result = await GetMultipleVenues<Venue>("/venues/search", parameters, true);
			return  result.response.venues;
        }

        
       	public async Task<List<Photo>> GetVenuePhotos(string venueId, Dictionary<string, string> parameters)
        {
			var result = await GetSingle<FourSquareEntityItems<Photo>>("/venues/" + venueId + "/photos", parameters, true);
            FourSquareEntityItems<Photo> photos = result.response["photos"];
            return photos.items;
        }

        public async Task<List<Link>> GetVenueLinks(string venueId)
        {
			var result = await GetSingle<FourSquareEntityItems<Link>>("/venues/" + venueId + "/links", true);
			FourSquareEntityItems<Link> links = result.response["links"];

            return links.items;
        }

        public Todo SetVenueToDo(string venueId, string text)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("text", text);

            return Post<Todo>("/venues/" + venueId + "/marktodo", parameters).response["todo"];
        }

        public void SetVenueFlag(string venueId, string problem)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("problem", problem);

            Post("/venues/" + venueId + "/flag", parameters);
        }

        public void SetVenueProposeEdit(string venueId)
        {
            Post("/venues/" + venueId + "/proposeedit");
        }

        public void SetVenueProposeEdit(string venueId, Dictionary<string, string> parameters)
        {
            Post("/venues/" + venueId + "/proposeedit", parameters);
        }

        
        public Checkin AddCheckin(Dictionary<string, string> parameters)
        {
            JObject jObject = PostJObject("/checkins/add", parameters);
            return JsonConvert.DeserializeObject<Checkin>(jObject["response"]["checkin"].ToString());
        }

        public async Task<List<Checkin>> GetRecentCheckin()
        {
            return await GetRecentCheckin(null);
        }

        public async Task<List<Checkin>> GetRecentCheckin(Dictionary<string, string> parameters)
        {
			var result = await GetMultiple<Checkin>("/checkins/recent", parameters);
				return result.response["recent"];
        }

        public void AddChekinComment(string checkinId, string text)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("text", text);

            Post("/checkins/" + checkinId + "/addcomment", parameters);
        }

        public void DeleteChekinComment(string checkinId, string commentId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("commentId", commentId);

            Post("/checkins/" + checkinId + "/deletecomment", parameters);
        }

        //Tips
        public async Task<Tip> GetTip(string tipId)
        {
			var result = await GetSingle<Tip>("/tips/" + tipId, true);
            return result.response["tip"];
        }

        public Tip AddTip(Dictionary<string, string> parameters)
        {
            return Post<Tip>("/tips/add", parameters).response["tip"];
        }

       	public async Task<List<Tip>> SearchTips(Dictionary<string, string> parameters)
        {
			var result = await GetMultiple<Tip>("/tips/search", parameters, true);
            return result.response["tips"];
        }

        public Todo SetTipToDo(string tipId)
        {
            return Post<Todo>("/tips/" + tipId + "/marktodo").response["todo"];
        }
		 
        public void SetTipDone(string tipId)
        {
            Post("/tips/" + tipId + "/markdone");
        }

        public void SetTipUnMark(string tipId)
        {
            Post("/tips/" + tipId + "/unmark");
        }

        //Photo
        public async Task<Photo> GetPhoto(string photoId)
        {
			var result = await GetSingle<Photo>("/photos/" + photoId);
			return result.response["photo"];
        }

        public Photo AddPhoto(Dictionary<string, string> parameters)
        {
            return Post<Photo>("/photos/add", parameters).response["photo"];
        }

        //Settings
       	public async Task<Setting> GetSettings()
        {
			var result = await GetSingle<Setting>("/settings/all");
			return result.response["settings"];
        }

        public void SetSetting(string settingId, string value)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("value", value);

            Post("/settings/" + settingId + "/set", parameters);
        }

        //Special
        public async Task<Special> GetSpecial(string specialId)
        {
			var result = await GetSingle<Special>("/specials/" + specialId, true);
			return result.response["special"];
        }

        public async Task<List<Special>> SearchSpecials(Dictionary<string, string> parameters)
        {
			var result = await GetSingle<FourSquareEntityItems<Special>>("/specials/search", parameters);
			FourSquareEntityItems<Special> specials = result.response["specials"];

            return specials.items;
        }
    }
}
