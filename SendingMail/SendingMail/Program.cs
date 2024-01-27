using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SendingMail
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Sender's email address and credentials
            string senderEmail = ConfigurationManager.AppSettings["SenderEmail"];
            string senderPassword = ConfigurationManager.AppSettings["SenderPassword"];

            //  get password using 2-factor authentication 

            // Recipient's email address
            string recipientEmail = "sekhar16194@gmail.com";

            // Create the MailMessage object
            MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail);

            // Set the subject and body of the email
            mailMessage.Subject = "Subject of the email";
            mailMessage.Body = "Body of the email";

            // Create the SmtpClient and set its properties
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587; // Use the appropriate port for your email provider
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;
            Console.WriteLine("sending the mail...");

            try
            {
                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }

        }
    }
}
