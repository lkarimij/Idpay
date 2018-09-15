using LeiliBeauty.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace LeiliBeauty
{
    public static class ExceptionExtensions
    {
        public static string ToLogString(this Exception ex, string moreInfo, params object[] extendedParameters)
        {

            StringBuilder message = new StringBuilder();
            if (ex != null)
                message.AppendLine(ex.GetType().ToString());
            message.AppendLine(string.Format(moreInfo, extendedParameters));

            Exception iex = ex;
            while (iex != null)
            {
                message.AppendLine(iex.Message);
                iex = iex.InnerException;
            }
            message.AppendLine(ex.StackTrace);
            return message.ToString();
        }
    }
    public static class Utils
    {
        public static string RelativeFromAbsolutePath(string path)
        {
            if (HttpContext.Current != null)
            {
                var request = HttpContext.Current.Request;
                var applicationPath = request.PhysicalApplicationPath;
                var virtualDir = request.ApplicationPath;
                virtualDir = virtualDir == "/" ? virtualDir : (virtualDir + "/");
                return path.Replace(applicationPath, virtualDir).Replace(@"\", "/");
            }

            throw new InvalidOperationException("We can only map an absolute back to a relative path if an HttpContext is available.");
        }

        public static async Task<bool> SendEmail(EmailFormModel model)
        {

            try
            {
                var body = "<p>{3} Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(string.IsNullOrEmpty(model.ToEmail) ? "To" : model.ToEmail));// "Admin@leilakarimi.ir"
                message.From = new MailAddress("From");
                message.Subject = string.IsNullOrEmpty(model.Subject) ? "From My Personal Website" : model.Subject;
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message, string.IsNullOrEmpty(model.ToEmail) ? "" : "Dear " + model.ToEmail + " <br/>");
                message.IsBodyHtml = true;

                if (model.Upload != null && model.Upload.ContentLength > 0)
                {
                    message.Attachments.Add(new Attachment(model.Upload.InputStream, Path.GetFileName(model.Upload.FileName)));
                }
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "fromUser",
                        Password = "fromPass"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "fromHost";
                    smtp.Port = 587;
                    smtp.EnableSsl = false;
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
                    await smtp.SendMailAsync(message);
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetEnumDescription(object enumItem)
        {
            DescriptionAttribute[] attr = (enumItem.GetType().GetField(enumItem.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true) as DescriptionAttribute[]);
            if (attr.Length == 0)
                return null;
            else
                return attr[0].Description;
        }

        public static string GetProductFirstImage(long productCode)
        {
            return GetProductImages(productCode).DefaultIfEmpty("~/img/logo.png").First();
        }

        public static string[] GetProductImages(long productCode)
        {
            DirectoryInfo directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(@"~\ProductImage"));
            return directory.GetFiles(string.Format("{0}_*", productCode)).Select(f => "~" + Utils.RelativeFromAbsolutePath(f.FullName)).ToArray();
        }

        public static List<Order> OrdersInMemory
        {
            get
            {
                var ordersInMemory = (Dictionary<string, Order>)HttpContext.Current.Application[AppConstants.OrdersInMemory];
                return ordersInMemory.Values.ToList();
            }
        }

        public static Order CurrentOrder
        {
            get
            {
                if (HttpContext.Current.Session == null)
                    return null;

                var ordersInMemory = (Dictionary<string, Order>)HttpContext.Current.Application[AppConstants.OrdersInMemory];
                if (ordersInMemory.Keys.Contains(HttpContext.Current.Session.SessionID))
                    return ordersInMemory[HttpContext.Current.Session.SessionID];
                else
                    return null;
            }
            set
            {
                var ordersInMemory = (Dictionary<string, Order>)HttpContext.Current.Application[AppConstants.OrdersInMemory];
                if (value == null)
                {
                    if (ordersInMemory.Keys.Contains(HttpContext.Current.Session.SessionID))
                        ordersInMemory.Remove(HttpContext.Current.Session.SessionID);
                }
                else
                {
                    if (ordersInMemory.Keys.Contains(HttpContext.Current.Session.SessionID))
                    {
                        ordersInMemory[HttpContext.Current.Session.SessionID] = value;
                    }
                    else
                    {
                        ordersInMemory.Add(HttpContext.Current.Session.SessionID, value);
                    }
                }
                HttpContext.Current.Application[AppConstants.OrdersInMemory] = ordersInMemory;
            }
        }

        public static string GenerateSlug(long productCode,string productPersianName)
        {
            string str = string.Format("{0}-{1}", productCode, productPersianName).ToLower();

            //string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^\u0600-\u06FFa-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        //private static string RemoveAccent(string text)
        //{
        //    byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(text);
        //    return System.Text.Encoding.ASCII.GetString(bytes);
        //}

    }



    public static class SessionConstants
    {
        public static string Basket { get; set; }
    }

    public static class AppConstants
    {
        public static string OrdersInMemory { get; set; }
        public static long PostCost = 8000;
    }

}