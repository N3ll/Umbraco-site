using AarhusWebDevCoop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers
{
    public class MessageSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        // GET: MessageSurface
        public ActionResult Index()
        {
            return PartialView("MessageForm", new Message());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(Message model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            IContent comment = Services.ContentService.CreateContent(model.Title, CurrentPage.Id, "Message");
            comment.SetValue("title", model.Title);
            comment.SetValue("text", model.Text);

            Services.ContentService.SaveAndPublishWithStatus(comment);


            TempData["success"] = true;

            return RedirectToCurrentUmbracoPage();
        }
    }
}