//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OtelApi.GlobalEntity
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class Price
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Price()
        {
            this.Room = new HashSet<Room>();
        }
    
        public int ID { get; set; }
        public int Number { get; set; }
        public int ValueID { get; set; }
        
        [JsonIgnore]
        public virtual Value Value { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Room> Room { get; set; }
    }
}
