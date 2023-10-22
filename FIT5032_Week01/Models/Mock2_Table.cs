using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Week01.Models
{
    public partial class Mock2_Table : DbContext
    {
        public Mock2_Table()
            : base("name=Mock2_Table")
        {
        }

        public virtual DbSet<Mock> Mocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mock>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<Mock>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<Mock>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Mock>()
                .Property(e => e.gender)
                .IsUnicode(false);
        }
    }
}
