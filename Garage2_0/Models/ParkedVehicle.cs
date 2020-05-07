using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2_0.Models
{

    public enum VehicleType
    {
        Motorcycle,
        Car,
        Bus,
        Airplane,
        Boat
    }
    public class ParkedVehicle
    {
        public int ID { get; set; }

        [Display(Name = "Vehicle type")]
        [Required]
        public VehicleType VType { get; set; }

        [Display(Name = "Wheel count")]
        [Required]
        [Range(0, 30)]
        public int Wheels { get; set; }

        [Display(Name = "Registration number")]
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(15)]
        public string Manufacturer { get; set; }



        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy:MM:dd HH:mm:ss}")]

        [Display(Name = "Vehicle parked time")]
        public DateTime Arrival{ get ;set ; }

        [Required]
        [StringLength(15)]
        public string Color { get; set; }

        [Display(Name = "Vehicle model")]
        [Required]
        [StringLength(15)]
        public string VehicleModel { get; set; }
        
        
    }
}
