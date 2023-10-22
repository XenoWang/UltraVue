using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Week01.Models
{
    public partial class Mock_Data_Model : DbContext
    {
        public Mock_Data_Model()
            : base("name=Mock_Data_Model")
        {
        }

        public virtual DbSet<MOCK_DATA> MOCK_DATA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MOCK_DATA>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<MOCK_DATA>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<MOCK_DATA>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<MOCK_DATA>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<MOCK_DATA>()
                .Property(e => e.ip_address)
                .IsUnicode(false);
        }
    }
}
