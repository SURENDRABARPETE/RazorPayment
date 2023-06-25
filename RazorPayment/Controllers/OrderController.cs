using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Razorpay.Api;
using RazorPayment.Models;

namespace RazorPayment.Controllers
{
    public class OrderController : Controller
    {
        [BindProperty]
        public OrderEntity _orderDetails { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InitiateOrder()
        {
            string key = "rzp_test_NHUurRC4SWVs8v";
            string secret = "WLVVmvL2knj4zPTBYU0Xyz2K";
            Random _Random = new Random();
            string TransectionId = _Random.Next(0, 10000).ToString();        
            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDecimal(_orderDetails.TotalAmout)*100); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", TransectionId);
            RazorpayClient client = new RazorpayClient(key, secret);
            Razorpay.Api.Order order = client.Order.Create(input);
            ViewBag.orderid = order["id"].ToString();

            return View("Payment", _orderDetails);
        }

        public IActionResult Payment(string razorpay_payment_id,string razorpay_order_id, string razorpay_signature)
        {
            Dictionary<string, string> Attributs = new Dictionary<string, string>();
            Attributs.Add("razorpay_payment_id", razorpay_payment_id);
            Attributs.Add("razorpay_order_id", razorpay_order_id);
            Attributs.Add("razorpay_signature", razorpay_signature);
            Utils.verifyPaymentSignature(Attributs);
            OrderEntity orderDetails = new OrderEntity();
            orderDetails.TransectionID = razorpay_payment_id;
            orderDetails.OrderID = razorpay_order_id;
            return View("PaymentSuccess", orderDetails);
        }
        }
}
