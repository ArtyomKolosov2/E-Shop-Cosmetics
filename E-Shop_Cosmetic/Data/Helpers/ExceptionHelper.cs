using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Data.Helpers
{
    public static class ExceptionHelper
    {
        public static void ThrowIfObjectWasNull(object dataObject ,string objectName, string methodName = "")
        {
            if (dataObject is null)
            {
                throw new ArgumentNullException(objectName, $"Object was null in {methodName}");
            }
        }
    }
}
