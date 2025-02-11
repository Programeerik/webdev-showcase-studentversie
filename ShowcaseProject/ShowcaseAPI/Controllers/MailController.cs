using Microsoft.AspNetCore.Mvc;
using ShowcaseAPI.Models;
using System.Net;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ShowcaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        // POST api/<MailController>
        [HttpPost]
        public ActionResult Post([FromBody] Contactform form)
        {
            //Op brightspace staan instructies over hoe je de mailfunctionaliteit werkend kunt maken:
            //Project Web Development > De showcase > Week 2: contactpagina (UC2) > Hoe verstuur je een mail vanuit je webapplicatie met Mailtrap?

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { errors });
            }

            try
            {

                // Mail versturen
                SmtpClient client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("0226739d356de2", "8bea34b49c08d3"),
                    EnableSsl = true
                };

                string messageBody = $"Nieuw contactverzoek van {form.FirstName} {form.LastName}.\n\n" +
                                     $"Email: {form.Email}\nTelefoonnummer: {form.Phone}";

                client.Send(form.Email, "s1157193@student.windesheim.nl", "Nieuw contactverzoek", messageBody);

                // Geen persoonsgegevens opslaan
                form = null;

                ViewBag.Message = "Uw bericht is succesvol verzonden!";
                return Ok(new { message = ViewBag.Message });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Er is een fout opgetreden bij het verzenden van uw bericht.";
                return StatusCode(500, new { error = ViewBag.Message, details = ex.Message });
            }
        }
    }
}
