using FourSquare.Entities;

namespace FourSquare.Response
{
    public class FourSquareMultipleVenuesResponse<T> : FourSquareResponse where T : FourSquareEntity
    {
        public VenueResponse<T> response
        {
            get;
            set;
        }
    }
}