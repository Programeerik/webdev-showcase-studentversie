using System.ComponentModel.DataAnnotations;

namespace Showcase_Contactpagina.Models
{
    public class Contactform
    {
        [Required]
        [StringLength(60)]
        public string FirstName {  get; set; }

        [Required]
        [StringLength(60)]
        public string LastName {  get; set; }

        [Required]
        [EmailAddress]
        [StringLength(80, ErrorMessage = "E-mailadres mag niet langer dan 80 karakters zijn.")]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
