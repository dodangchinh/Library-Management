using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien
{
    public static class AddHelpers
    {
        public static bool IsLogged(this HtmlHelper htmlHelper)
        {
            if (HttpContext.Current.Session["Logged"] != null)
                return true;
            return false;
        }

        public static string GetUsername(this HtmlHelper htmlHelper)
        {
            var account = HttpContext.Current.Session["Logged"] as Account;
            if (account != null)
                return account.Name;
            return null;
        }
        public static Account GetAccount()
        {
            var account = HttpContext.Current.Session["Logged"] as Account;
            if (account != null)
                return account;
            return null;
        }

        public static async Task<bool> IsEmailValid(string email)
        {
            string APIKey = "/*b519c74947324ea8bca40864706a8562*/";
            string apiUrl = $"https://emailvalidation.abstractapi.com/v1/?api_key={APIKey}&email={email}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                var json = JObject.Parse(response);
                string deliverability = json["deliverability"]?.ToString();

                if (deliverability == "DELIVERABLE")
                    return true; 
                else
                    return false;
            }
        }
    }
}