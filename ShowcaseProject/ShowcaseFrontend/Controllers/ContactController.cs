﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Showcase_Contactpagina.Models;
using System.Numerics;
using System.Text;
using System.Net.Http;

namespace Showcase_Contactpagina.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClient;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("NoSSL");
        }

        // GET: ContactController
        public ActionResult Index()
        {
            return View();
        }

        // POST: ContactController
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Index(Contactform form)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        ViewBag.Message = "De ingevulde velden voldoen niet aan de gestelde voorwaarden";
        //        return View();
        //    }

        //    var settings = new JsonSerializerSettings
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    };

        //    var json = JsonConvert.SerializeObject(form, settings);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

           


            //Gebruik _httpClient om een POST-request te doen naar ShowcaseAPI die de Mail uiteindelijk verstuurt met Mailtrap (of een alternatief).
            //Verstuur de gegevens van het ingevulde formulier mee aan de API, zodat dit per mail verstuurd kan worden naar de ontvanger.
            //Hint: je kunt dit met één regel code doen. Niet te moeilijk denken dus. :-)
            //Hint: vergeet niet om de mailfunctionaliteit werkend te maken in ShowcaseAPI > Controllers > MailController.cs,
            //      nadat je een account hebt aangemaakt op Mailtrap (of een alternatief).

        //    HttpResponseMessage response = await _httpClient.PostAsync("api/Mail", content);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        ViewBag.Message = "Er is iets misgegaan";
        //        return View();
        //    } else
        //    {
        //        ViewBag.Message = "Het contactformulier is verstuurd";
        //    }

        //    return View();
        //}
    }
}
