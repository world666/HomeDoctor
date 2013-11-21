using System;

namespace DataRepository.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int? ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}