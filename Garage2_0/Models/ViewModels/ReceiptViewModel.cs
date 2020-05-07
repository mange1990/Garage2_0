using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2_0.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public string RegistrationNumber { get; set; }

        public int ID { get; set; }
        public DateTime Arrival { get; set; }

        public DateTime CheckOut { get; set; }
        
        public int Price { get; set; }
        


    }
}
