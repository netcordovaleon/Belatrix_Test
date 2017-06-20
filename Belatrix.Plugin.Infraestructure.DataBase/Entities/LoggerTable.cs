using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belatrix.Plugin.Infraestructure.DataBase.Entities
{
    [Table("tm_logger")]
    public class LoggerTable
    {
        [Key]
        public int codLogger { get; set; }
        public int typeLogger { get; set; }
        public string messageLogger { get; set; }
        public Nullable<DateTime> dateInsertLogger { get; set; }
        public string userInsertLogger { get; set; }

        public LoggerTable() { 
        }
    }
}
