﻿using System;

namespace HomeDoctor.Models.Database.Tables
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}