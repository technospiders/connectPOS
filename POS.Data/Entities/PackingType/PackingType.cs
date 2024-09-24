using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    [Table("PackingType", Schema = "logistics")]
    public class PackingType : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsStatus { get; set; }
    }

}
