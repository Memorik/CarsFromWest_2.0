using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CarsFromWest_master
{
    class AppContextDeal : DbContext
    {
        public DbSet<Deal> Deals { get; set; }

        public AppContextDeal() : base("DefaultConnection") { }

    }
}
