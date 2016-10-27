using System;

namespace FourSquare.Entities
{
    public class FourSquareEntityUsers : FourSquareEntity
    {
        public Int64 count
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
