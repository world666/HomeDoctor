using System;
using System.ComponentModel.DataAnnotations;

namespace HomeDoctor.Models
{

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен быть не менее {2} символов длиной.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "Логин в скайп*")]
        public string SkypeLogin { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} должен быть числом.")]
        [Display(Name = "Возраст")]
        public int? Age { get; set; }

        [Display(Name = "Баланс (грн)")]
        public decimal Money { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя*")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен быть не менее {2} символов длиной.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль*")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль*")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Логин в скайп*")]
        public string SkypeLogin { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "{0} должен быть числом.")]
        [Display(Name = "Возраст")]
        public int? Age { get; set; }

        [Display(Name = "Баланс (грн)")]
        public decimal Money { get; set; }
    }
}
