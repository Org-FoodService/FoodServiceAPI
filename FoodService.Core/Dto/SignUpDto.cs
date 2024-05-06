using System.ComponentModel.DataAnnotations;

namespace FoodService.Core.Dto
{
    /// <summary>
    /// Data transfer object for sign-up operation.
    /// </summary>
    public partial class SignUpDto
    {
        /// <summary>
        /// Gets or sets the CPF/CNPJ.
        /// </summary>
        [Required(ErrorMessage = "CPF/CNPJ is required")]
        public required string CpfCnpj { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password.
        /// </summary>
        [Required(ErrorMessage = "Confirm password is required")]
        public required string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Required(ErrorMessage = "PhoneNumber is required")]
        public required string PhoneNumber { get; set; }
    }
}
