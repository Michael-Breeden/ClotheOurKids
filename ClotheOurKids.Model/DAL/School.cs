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
    
    public partial class School
    {
        public short SchoolId { get; set; }
        public short OfficeId { get; set; }
        public short SchoolDistrictId { get; set; }
        public byte SchoolTypeId { get; set; }
    
        public virtual Office Office { get; set; }
        public virtual SchoolDistrict SchoolDistrict { get; set; }
        public virtual SchoolType SchoolType { get; set; }
    }
}
