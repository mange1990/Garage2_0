using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage2_0.Models;

namespace Garage2_0.Data
{
    public class Garage2_0Context : DbContext
    {
        public Garage2_0Context (DbContextOptions<Garage2_0Context> options)
            : base(options)
        {
        }

        public DbSet<Garage2_0.Models.ParkedVehicle> ParkedVehicle { get; set; }
    }
}
