using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPayment.Models
{
    public class OrderEntity
    {
        public int MyProperty { get; set; }

        public string  CustomarName { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string TotalAmout { get; set; }

        [NotMapped]
        public string TransectionID { get; set; }
        [NotMapped]
        public string OrderID { get; set; }
    }
}
