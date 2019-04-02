using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppAlexey.BLL.ViewModels
{
    class CreateUserViewModel
    {
        [Required(ErrorMessage = "First name not specified!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be from 3 to 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name not specified!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be from 3 to 50 characters.")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Incorrect phone")]
        [RegularExpression(@"/^(\s*)?(\+)?([- _():=+]?\d[- _():=+]?){10,14}(\s*)?$/", ErrorMessage = "incorrect entry format")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password not specified!")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Incorrect address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect entry format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name of carrier not specified!")]
        public string CarrierName { get; set; }
    }
}