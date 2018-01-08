using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT.Models.DTOs
{
    public class UpdateUserAltDTO
    {
       

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}