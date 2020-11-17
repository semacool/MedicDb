namespace Medic.DataSource
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnalizeMocha")]
    public partial class AnalizeMocha
    {
        public int Id { get; set; }

        public float ZhelezoMocha { get; set; }

        public float Mocha { get; set; }


        public int IdAnalize { get; set; }

        public virtual Analizi Analizi { get; set; }
    }
}
