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
    
    public partial class ImageOfOtel
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }
        public Nullable<int> OtelID { get; set; }

        [JsonIgnore]
        public virtual Otel Otel { get; set; }
    }
}
