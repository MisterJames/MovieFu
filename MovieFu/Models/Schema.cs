using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieFu.Models
{
    public class Movie
    {
        public int MovieID { get; set; }

        [Required]
        public string Title { get; set; }        
        [Required]
        public string Description { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

    }

    public class Rating
    {
        public int RatingID { get; set; }

        [DisplayName("User Name")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Comments { get; set; }
        
        public float Stars { get; set; }

        public virtual int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
    }


}