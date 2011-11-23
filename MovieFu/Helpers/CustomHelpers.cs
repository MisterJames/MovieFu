using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieFu.Models;

namespace MovieFu.HtmlHelpers
{
    public static class CustomHelpers
    {
        public static string RatingsText(this HtmlHelper helper, ICollection<Rating> ratings)
        {
            string result = "No ratings.";

            if (ratings != null && ratings.Count() >0)
            {
                result = string.Format("{0:#.#} stars.", ratings.Average(r => r.Stars));
            }

            return result;
        }

    }

}