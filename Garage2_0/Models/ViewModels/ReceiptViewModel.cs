using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2_0.Models.ViewModels
{
    public class ReceiptViewModel
    {
        [Display(Name = "Registration number")]

        public string RegistrationNumber { get; set; }

        public int ID { get; set; }
        [Display(Name = "Vehicle parked time")]

        public DateTime Arrival { get; set; }

        public DateTime CheckOut { get; set; }
        
        public int Price { get; set; }
        


    }
}
