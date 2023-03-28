using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atali_len.Model
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<merki> Merkis { get; set; } = null!;
        public DbSet<Work> Works { get; set; } = null!;
        public DbSet<Zakaz> Zakazs { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source="+ AppDomain.CurrentDomain.BaseDirectory+ "/Data/ataliDB.db");
        }
    }
}
