using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2_0.Models.ViewModels
{
    public class ReceiptViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd HH:mm:ss}")]
        [Display(Name = "Vehicle parked time")]
        public DateTime Arrival { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd HH:mm:ss}")]
        [Display(Name = "Vehicle checked out time")]
        public DateTime CheckOut { get; set; }

        [Display(Name = "The Cost")]
        public int Price { get; set; }
        


    }
}
