using Microsoft.EntityFrameworkCore;
using Movies.API.Models;
using System.Collections.Generic;
using System;

namespace Movies.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
