namespace FourSquare.Entities
{
    public class Mayorship : FourSquareEntity
    {
        public string type
        {
            get;
            set;
        }
        
        public string checkins
        {
            get;
            set;
        }

        public User user
        {
            get;
            set;
        }
    }
}