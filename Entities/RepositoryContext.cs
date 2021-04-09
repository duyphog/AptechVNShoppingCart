using System;
using System.IO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(WriteLogToFile);
        }

        private async void WriteLogToFile(string str)
        {
            var path = $"/Users/phongnd/Desktop/Aptech/Logs/datalogs/context_log.txt";

            using var streamWriter = File.AppendText(path);
            await streamWriter.WriteLineAsync(str);
        }
    }
}
