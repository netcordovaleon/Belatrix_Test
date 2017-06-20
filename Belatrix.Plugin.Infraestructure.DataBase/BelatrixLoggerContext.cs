using Belatrix.Plugin.Infraestructure.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.Infraestructure.DataBase
{
    public class BelatrixLoggerContext : DbContext 
    {
        public DbSet<LoggerTable> Loggers { get; set; } 
    }
}
