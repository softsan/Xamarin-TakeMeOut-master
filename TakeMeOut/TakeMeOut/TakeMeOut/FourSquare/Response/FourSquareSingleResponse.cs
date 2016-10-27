using System.Collections.Generic;
using FourSquare.Entities;

namespace FourSquare.Response
{
    public class FourSquareSingleResponse<T> : FourSquareResponse where T : FourSquareEntity
    {
        public Dictionary<string, T> response
        {
            get;
            set;
        }
    }
}