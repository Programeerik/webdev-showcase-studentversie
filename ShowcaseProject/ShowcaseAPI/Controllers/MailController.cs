using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ShowcaseAPI.Models;
using System.Net;
using System.Net.Mail;

namespace ShowcaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        private readonly ILogger<MailController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MailController(ILogger<MailController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contactform form)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

                return BadRequest(new
                {
                    message = "Validatiefout: controleer uw invoer.",
                    errors
                });
            }



            var recaptchaSecret = _configuration["ReCaptchaSettings:SecretKey"];
            if (string.IsNullOrEmpty(form.RecaptchaResponse))
            {
                return BadRequest(new { message = "ReCAPTCHA verificatie ontbreekt." });
            }

            var googleResponse = await _httpClient.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={recaptchaSecret}&response={form.RecaptchaResponse}",
                null
            );

            var jsonResponse = await googleResponse.Content.ReadAsStringAsync();
            var captchaResult = JsonSerializer.Deserialize<RecaptchaVerificationResponse>(jsonResponse);

            if (!captchaResult.Success || captchaResult.Score < 0.5) 
            {
                return BadRequest(new { message = "ReCAPTCHA verificatie mislukt." });
            }

            try
            {
                using (SmtpClient client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525))
                {
                    client.Credentials = new NetworkCredential("0226739d356de2", "8bea34b49c08d3");
                    client.EnableSsl = true;

                    string messageBody = $"Nieuw contactverzoek van {form.FirstName} {form.LastName}.\n\n" +
                                         $"Email: {form.Email}\nTelefoonnummer: {form.Phone}\n\n" +
                                         $"Bericht:\n{form.Message}";

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(form.Email),
                        Subject = "Nieuw contactverzoek",
                        Body = messageBody,
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add("s1157193@student.windesheim.nl");

                    client.Send(mailMessage);
                }

                return Ok(new { message = "Uw bericht is succesvol verzonden!" });
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP fout tijdens het verzenden van de e-mail.");
                return StatusCode(500, new
                {
                    error = "Er is een fout opgetreden bij het verzenden van uw bericht.",
                    details = smtpEx.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Algemene fout bij e-mail verzenden.");
                return StatusCode(500, new
                {
                    error = "Er is een onverwachte fout opgetreden.",
                    details = ex.Message
                });
            }
        }
    }
    public class RecaptchaVerificationResponse
    {
        public bool Success { get; set; }
        public float Score { get; set; }
    }

}
