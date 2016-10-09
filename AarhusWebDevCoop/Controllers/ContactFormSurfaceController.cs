using AarhusWebDevCoop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();
            
           /* Sending an e-mail trhrough a non-secure SMTP connection because gmail does not allow changes on the security for my account
            * The credentials are not correct since I had use my personal information
            * If they are replaced with real the sending e-mail functionality is working
            * 
            * MailMessage msg = new MailMessage();
            msg.To.Add("neli.chakarova@yahoo.com");
            msg.Subject = model.Subject;
            msg.From = new MailAddress(model.Email, model.Name);
            msg.Body = model.Message + "\n my email is: " + model.Email;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("exampley@gmail.com", "password");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }*/

            IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Comment");
            comment.SetValue("commentName", model.Name);
            comment.SetValue("email", model.Email);
            comment.SetValue("subject", model.Subject);
            comment.SetValue("message", model.Message);

            Services.ContentService.Save(comment);

           // Services.ContentService.SaveAndPublishWithStatus(comment);


            TempData["success"] = true;

                return RedirectToCurrentUmbracoPage();
        }
    }
}