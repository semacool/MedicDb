using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medic.DataSource
{
    public class BarcodeChar
    {
        public int Id { get; set; }
        [Column(TypeName ="char")]
        public string Character { get; set; }
        public int? Value { get; set; }
        public string Pattern { get; set; }
    }
}
