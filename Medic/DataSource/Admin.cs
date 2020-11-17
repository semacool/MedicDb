namespace Medic.DataSource
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Admin
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Fio { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        public int IdUser { get; set; }

        public virtual User User { get; set; }
    }
}
