using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeDoctor.Models.Database.Tables
{
    public class Profile
    {
        public int Id { get; set; }

        public string SkypeLogin { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int? Age { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}