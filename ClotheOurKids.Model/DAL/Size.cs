//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClotheOurKids.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Size
    {
        public int SizeId { get; set; }
        public string Name { get; set; }
        public byte AgeGroupId { get; set; }
        public string Gender { get; set; }
        public byte ClothesTypeId { get; set; }
        public int SortOrder { get; set; }
    
        public virtual AgeGroup AgeGroup { get; set; }
        public virtual ClothesType ClothesType { get; set; }
    }
}
