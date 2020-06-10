using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contexts
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(@"Data Source=SQL5047.site4now.net;Initial Catalog=DB_A57C8B_admin;User Id=DB_A57C8B_admin_admin;Password=admin123456;");

            return new DataContext(builder.Options);
        }
    }
}
