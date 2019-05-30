using MariusBudrauskas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MariusBudrauskas.Context
{
    public class ScoreContext : DbContext
    {
        public DbSet<Score> Scores {get;set;}
    }
}