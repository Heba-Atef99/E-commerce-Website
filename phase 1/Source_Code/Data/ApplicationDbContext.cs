using E_commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Data
{
    //public class ApplicationDbContext : DbContext
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //    {

    //    }


    //    //public DbSet<Participants2> Participants2 { get; set; }
    //}

    public class APDbContext : DbContext
    {
        public APDbContext(DbContextOptions<APDbContext> options) : base(options)
        {

        }
        public DbSet<Participants> Participants { get; set; }
        public DbSet<ITEM> ITEM { get; set; }
        public DbSet<ACCOUNT> ACCOUNT { get; set; }
        public DbSet<CART> CART { get; set; }
        public DbSet<USER> USER { get; set; }
        public DbSet<PROMOTED_ITEM> PROMOTED_ITEM { get; set; }
        public DbSet<PURCHASED_ITEM> PURCHASED_ITEM { get; set; }
        public DbSet<TRANSACTION_HISTORY> TRANSACTION_HISTORY { get; set; }

    }
    public class SADbContext : DbContext
    {
        public SADbContext(DbContextOptions<SADbContext> options) : base(options)
        {

        }
        public DbSet<ITEM> ITEM { get; set; }
        public DbSet<ACCOUNT> ACCOUNT { get; set; }
        public DbSet<CART> CART { get; set; }
        public DbSet<USER> USER { get; set; }
        public DbSet<PROMOTED_ITEM> PROMOTED_ITEM { get; set; }
        public DbSet<PURCHASED_ITEM> PURCHASED_ITEM { get; set; }
        public DbSet<TRANSACTION_HISTORY> TRANSACTION_HISTORY { get; set; }

    }

}
