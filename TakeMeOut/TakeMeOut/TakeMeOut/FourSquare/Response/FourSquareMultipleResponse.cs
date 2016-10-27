using System.Collections.Generic;
using FourSquare.Entities;

namespace FourSquare.Response
{
    public class FourSquareMultipleResponse<T> : FourSquareResponse where T : FourSquareEntity
    {
        public Dictionary<string, List<T>> response
        {
            get;
            set;
        }
    }
}