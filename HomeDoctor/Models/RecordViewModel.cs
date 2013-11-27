using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeDoctor.Models
{
    public class RecordViewModel
    {
        public int RecordId { get; set; }
        public bool Answered { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Сообщение")]
        public string Message { get; set; }
        public string UserName { get; set; }
    }
}