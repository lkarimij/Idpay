# Idpay
implementation sample of Idpay payment gateway 

This project is an implementation sample Idpay payment gateway

I used two external libraries in this project:
Newtonsoft.Json
RestSharp

You can consume RestSharp with this Nuget:
pm> Install-Package RestSharp -Version 106.3.1

and consume Json with this Nuget:
pm> Install-Package Newtonsoft.Json -Version 11.0.2

‘Idpay.cs’ class is everything that you need, at first you must configure ‘X-SANDBOX’ property with ‘true’ and when you check all aspect of payment cycle, you can change its value to ‘false’, and configure the callback view that payment gateway call as post like this:
callback = "http://leilibeauty.com/order/returnpayment"

In next step you must design a view for your client that represents result of payment and tracking code, I create this page with this name: ‘returnpayment.cshtml’.

payment gateway callback to your web site as post with some parameters, so you must have some function like this in your controller

[HttpPost]
public ActionResult returnpayment(int status = 0, int track_id = 0, string id = "", string order_id = "", int amount = 0){}
