using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage2_0.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set; }
        public string RegistrationNumber { get; set; }

    }
}
