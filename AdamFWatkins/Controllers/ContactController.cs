using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdamFWatkins.Models;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using Newtonsoft.Json;

namespace AdamFWatkins.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/


        dbContext db = new dbContext();

        public ActionResult Index()
        {
            SiteText st = db.SiteTexts.Where(s => s.key == "ContactContent").FirstOrDefault();
            return View(st);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void SendEmail(string name, string email, string subject, string message)
        {


            var response = Request["g-recaptcha-response"];
            var secret = "6LdcaxMTAAAAAJXJjsI0fVRV95bYJuDVN8Cwa9Kz";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            //if (!captchaResponse.Success)
            //{
            //    if (captchaResponse.ErrorCodes.Count > 0)
            //    {

            //        var error = captchaResponse.ErrorCodes[0].ToLower();
            //        switch (error)
            //        {
            //            case ("missing-input-secret"):
            //                ViewBag.Message = "The secret parameter is missing.";
            //                break;
            //            case ("invalid-input-secret"):
            //                ViewBag.Message = "The secret parameter is invalid or malformed.";
            //                break;

            //            case ("missing-input-response"):
            //                ViewBag.Message = "The response parameter is missing.";
            //                break;
            //            case ("invalid-input-response"):
            //                ViewBag.Message = "The response parameter is invalid or malformed.";
            //                break;

            //            default:
            //                ViewBag.Message = "Error occured. Please try again";
            //                break;
            //        }
            //    }

            //}
            //else
            //{
            //    ViewBag.Message = "Valid";
            //}


            if (captchaResponse.Success)
            {
                string _userName = "shnhwk";
                string _password = "xhlrzjsrpjigxkut";

                var body = "<p>Name: " + name + "</p>";
                body += "<p>Message: " + message + "</p>";
                body += "<p>Email: " + email + "</p>";

                var plain = "Name: " + name + " Email: " + email + " Message: " + message;


                //do whatever you want to the message        
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress("adam@adamfwatkins.com"));  //
                msg.Subject = subject;
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plain, null, MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_userName, _password)
                };


                smtp.Send(msg);
            }

        }
    }

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
