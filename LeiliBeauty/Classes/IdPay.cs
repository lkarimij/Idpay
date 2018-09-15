using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeiliBeauty
{
    public class IdPay
    {
        public static void callAPI(int amountToman, string description, out string id)
        {
            id = string.Empty;
            var client = new RestClient("https://api.idpay.ir/v1");            

            var request = new RestRequest("payment", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                order_id = Guid.NewGuid().ToString(),
                amount = amountToman * 10,
                phone = "02188559210",
                desc = description,
                callback = "http://leilibeauty.com/order/returnpayment"

            });

            // easily add HTTP Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-API-KEY", "904692db-892c-43fe-9cc1-13d2d7285b31");
            request.AddHeader("X-SANDBOX", "false");


            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content;

            var idPayResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<IDPayResponse>(response.Content);
            HttpContext.Current.Response.Redirect(idPayResponse.link);
            id = idPayResponse.id;

        }

        class IDPayResponse
        {
            public string id { get; set; }
            public string link { get; set; }
        }

        public class PayResult
        {
            public int status { get; set; }
            public int track_id { get; set; }

            public string id { get; set; }
            public string order_id { get; set; }
            public int amount { get; set; }
            //public object date { get; set; }
        }


    }
}