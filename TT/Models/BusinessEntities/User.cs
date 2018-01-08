using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TT.Models.BusinessEntities
{
    public class User
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }


        //This method allows to check if a User contains a name, used for validation.
        //Similar methods are present in the DTOs, adapted to their particular needs.
        //For these methods Name has been deemed required, but not birthdate, this could be subject to future changes.
        public bool Validate()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}