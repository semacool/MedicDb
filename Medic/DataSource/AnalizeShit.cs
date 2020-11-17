namespace Medic.DataSource
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnalizeShit")]
    public partial class AnalizeShit
    {
        public int Id { get; set; }

        public float ZhelezoShit { get; set; }

        public float Shit { get; set; }

        public int IdAnalize { get; set; }

        public virtual Analizi Analizi { get; set; }
    }
}
