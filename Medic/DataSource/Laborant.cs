namespace Medic.DataSource
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Laborant : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Laborant()
        {
            Analizis = new HashSet<Analizi>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Fio { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        public int IdUser { get; set; }

        public int IdLaboratory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Analizi> Analizis { get; set; }

        public virtual Laboratory Laboratory { get; set; }

        public virtual User User { get; set; }

        public override string ToString()
        {
            return Fio;
        }
    }
}
