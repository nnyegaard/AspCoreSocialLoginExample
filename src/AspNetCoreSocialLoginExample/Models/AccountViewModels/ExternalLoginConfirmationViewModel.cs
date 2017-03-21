namespace AspNetCoreSocialLoginExample.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
