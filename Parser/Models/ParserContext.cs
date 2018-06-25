using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Parser.Models
{
    public class ParserContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}