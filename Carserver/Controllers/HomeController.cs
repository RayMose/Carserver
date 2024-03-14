using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Carserver.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: About
        public ActionResult About()
        {
            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        // GET: Services
        public ActionResult Services()
        {
            return View();
        }

        // POST: Email
        [HttpPost]
        public ActionResult SendEmail(FormCollection form)
        {
            string name = form["w3lName"];
            string senderEmail = form["w3lSender"];
            string subject = form["w3lSubject"];
            string message = form["w3lMessage"];

            string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
            string password = ConfigurationManager.AppSettings["Password"];

            string smtpServer = "smtp.gmail.com";
            int port = 587;

            using (var smtpClient = new SmtpClient(smtpServer, port))
            {
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(fromEmail, password);

                using (MailMessage mailMessage = new MailMessage(fromEmail, senderEmail))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = "Name: " + name + "\nEmail: " + senderEmail + "\n\nMessage:\n" + message;

                    try
                    {
                        smtpClient.Send(mailMessage);
                        ViewBag.Message = "Email sent successfully!";
                    }
                    catch (SmtpException ex)
                    {
                        ViewBag.Message = "Failed to send email: " + ex.Message;
                    }
                }
            }

            return RedirectToAction("Contact");
        }
    }
}
