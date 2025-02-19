using ShowcaseFrontend.Models.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Showcase_Contactpagina.Models
{
    public class Contactform
    {
        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [StringLength(60, ErrorMessage = "Voornaam mag niet langer dan 60 tekens zijn.")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [StringLength(60, ErrorMessage = "Achternaam mag niet langer dan 60 tekens zijn.")]
        public required string LastName { get; set; }

        //https://stackoverflow.com/questions/8989081/email-model-validation-with-dataannotations-and-datatype
        //https://regexr.com/3ashn
        [Required(ErrorMessage = "E-mailadres is verplicht.")]
        [RegularExpression(@"^(?:(?:[\w`~!#$%^&*\-=+;:{}'|,?\/]+(?:(?:\.(?:\""(?:\\?[\w`~!#$%^&*\-=+;:{}'|,?\/\.()<>\[\] @]|\\""|\\\\)*\""|[\w`~!#$%^&*\-=+;:{}'|,?\/]+))*\.[\w`~!#$%^&*\-=+;:{}'|,?\/]+)?)|(?:\""(?:\\?[\w`~!#$%^&*\-=+;:{}'|,?\/\.()<>\[\] @]|\\""|\\\\)+\""))@(?:[a-zA-Z\d\-]+(?:\.[a-zA-Z\d\-]+)*|\[\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\])$", ErrorMessage = "Voer een geldig e-mailadres in.")]
        public required string Email { get; set; }

        //https://regexr.com/38pvb
        [Required(ErrorMessage = "Telefoonnummer is verplicht.")]
        [StringLength(20, ErrorMessage = "Telefoonnummer mag niet langer dan 20 tekens zijn.")]
        [RegularExpression(@"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$", ErrorMessage = "Voer een geldig telefoonnummer in.")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Onderwerp is een verplicht veld.")]
        [StringLength(200, ErrorMessage = "Onderwerp mag niet langer dan 200 tekens zijn.")]
        public required string Subject { get; set; }

        [StringLength(600, ErrorMessage = "Bericht mag niet langer dan 600 tekens zijn.")]
        [AllowSpecificHtml("b", "strong", "i", "u", "ul", "ol", "li", "h1", "h2", "h3", "p", "br")]
        public required string Message { get; set; }

        public string RecaptchaResponse { get; set; }
    }
}

