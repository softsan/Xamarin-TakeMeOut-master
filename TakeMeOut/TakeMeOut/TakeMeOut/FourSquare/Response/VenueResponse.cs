using System.Collections.Generic;

namespace FourSquare.Response
{
    public class VenueResponse<T>
    {
        public Dictionary<string, object> geocoded
        {
            get;
            set;
        }
        public List<T> venues
        {
            get;
            set;
        }
    }
}