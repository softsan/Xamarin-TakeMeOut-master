namespace FourSquare.Entities
{
    public class VenueExplore : FourSquareEntity
    {
        public FourSquareEntityItems<Reasons> reasons
        {
            get;
            set;
        }

        public Venue venue
        {
            get;
            set;
        }
	}
}