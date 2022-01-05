using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Prod_2.Models
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> opciones) : base(opciones) { }

        public DbSet<Value> Value { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Value>(entidad =>
            {
                entidad.ToTable("Value");
                entidad.HasKey(p => p.Id);

                entidad.Property(p => p.Value1)
                .IsRequired()
                .IsUnicode(false);

                entidad.Property(p => p.Value2)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            });
        }
    }
}
