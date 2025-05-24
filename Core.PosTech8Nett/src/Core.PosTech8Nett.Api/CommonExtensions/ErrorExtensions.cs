using FluentValidation.Results;
using System.Collections.Generic;
using System.Text;

namespace Core.PosTech8Nett.Api.CommonExtensions
{
    public static class ErrorExtensions
    {
        public static string ConvertToString(this List<ValidationFailure> listErrors)
        {
            StringBuilder result = new StringBuilder();
            foreach (var error in listErrors)
            {
                result.Append(string.Concat(error.ErrorMessage, " - "));
            }
            return result.ToString();
        }
    }
}

