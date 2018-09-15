using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeiliBeauty.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<Models.OrderDetail>();
        }

        [Required(ErrorMessage = "فیلد اجباری")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "کد پستی 10 رقمی")]
        [StringLength(10, ErrorMessage = "کد پستی 10 رقمی")]
        public string PostallCode { get; set; }

        [Required(ErrorMessage = "شماره تلفن با کد")]
        [MinLength(10, ErrorMessage = "شماره تلفن با کد")]
        [MaxLength(11, ErrorMessage = "شماره تلفن با کد")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "فیلد اجباری")]
        public string Address { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        
    }

    public class OrderDetail
    {
        public OrderDetail(long code)
        {
            this.ProductCode = code;

            if (code == 10000)
            {
                this.Count = 1;
                this.Price = AppConstants.PostCost;
                this.Name = "هزینه ارسال به تمام نقاط ایران ثابت میباشد";
                this.MaxCount = 1;
                this.ImageUrl = Utils.GetProductFirstImage(code);
            }
            else
            {
                this.Count = 1;
                this.Price = 1000;
                this.Name = "محصول آزمایشی";
                this.MaxCount = 3;
                this.ImageUrl = Utils.GetProductFirstImage(code);
            }
        }

        public OrderDetail()
        {
        }

        public long ProductCode { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public long Price { get; set; }

        public int MinCount { get { return 1; } }
        public int MaxCount { get; set; }

        [DynamicRange("MinCount", "MaxCount", ErrorMessage = "ماکزیمم {0} عدد")]
        public int Count { get; set; }
        public string Index { get; set; }
    }

    public class PayResultViewModel
    {
        public string PayStatus { get; set; }
        public string PayComment { get; set; }
        public int TrackingCode { get; set; }
        public string TetstResult { get; set; }

    }
}