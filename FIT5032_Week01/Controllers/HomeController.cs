using FIT5032_Week01.Models;
using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.IO;

namespace FIT5032_Week01.Controllers
{
    public class HomeController : Controller
    {
        [RequireHttps]
        public ActionResult Index()
        {
            // Please comment out these codes once you have registered your API key.
            //EmailSender es = new EmailSender();
            //es.RegisterAPIKey();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Staff,Admin")]
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }


        [HttpPost]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<ActionResult> Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var apiKey = "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"; // Replace with your SendGrid API key

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("wlh20000704@163.com", "XenoW"); // Replace with your email and name
                var to = new EmailAddress("wlh20000704@163.com"); // Replace with the recipient's email
                var subject = "Test";
                var plainTextContent = "Email content in plain text";
                var htmlContent = $"<p>Email From: {model.ToEmail}</p><p>Message:</p><p>{model.Contents}</p>";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                if (model.Upload != null && model.Upload.ContentLength > 0)
                {
                    var fileBytes = new byte[model.Upload.InputStream.Length];
                    model.Upload.InputStream.Read(fileBytes, 0, fileBytes.Length);

                    var base64Content = Convert.ToBase64String(fileBytes);

                    msg.AddAttachment(model.Upload.FileName, base64Content, model.Upload.ContentType, "attachment");
                }

                var response = await client.SendEmailAsync(msg);

                // Set the result message
                ViewBag.Result = "Email has been sent.";

                // Clear the model state
                ModelState.Clear();

                // Return the view with a new model
                return View(new SendEmailViewModel());
            }

            return View(model);
        }
    }
}