namespace Medic.DataSource
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Analizi")]
    public partial class Analizi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Analizi()
        {
            AnalizeBloods = new HashSet<AnalizeBlood>();
            AnalizeMochas = new HashSet<AnalizeMocha>();
            AnalizeShits = new HashSet<AnalizeShit>();
        }

        public int Id { get; set; }

        public int IdClient { get; set; }

        public int IdAnalizType { get; set; }

        public int IdLaborant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnalizeBlood> AnalizeBloods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnalizeMocha> AnalizeMochas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnalizeShit> AnalizeShits { get; set; }

        public virtual AnalizType AnalizType { get; set; }

        public virtual Client Client { get; set; }

        public virtual Laborant Laborant { get; set; }
    }
}
