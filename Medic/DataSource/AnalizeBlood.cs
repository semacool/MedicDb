namespace Medic.DataSource
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnalizeBlood")]
    public partial class AnalizeBlood
    {
        public int Id { get; set; }

        public float Zhelezo { get; set; }

        public float Blood { get; set; }

        public int IdAnalize { get; set; }

        public virtual Analizi Analizi { get; set; }
    }
}
