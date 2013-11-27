using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Model
{
    public class UserRecord
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Answered { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; } 
    }
}
