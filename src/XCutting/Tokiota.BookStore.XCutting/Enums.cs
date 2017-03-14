using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tokiota.BookStore.XCutting
{
    public static class Enums
    {
        public static class Repository
        {
            public static class Response {
                public enum Status
                {
                    Ok = 0,
                    Error = 1,
                    NotFound = 3
                }
            }
        }
    }
}
