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
    
    public partial class Request
    {
        public int RequestId { get; set; }
        public System.DateTime DateRequested { get; set; }
        public System.DateTime DateEstimatedDelivery { get; set; }
        public System.DateTime DateDelivered { get; set; }
        public string SubmittedByUserId { get; set; }
        public byte RequestStatusId { get; set; }
        public short OfficeId { get; set; }
        public string Gender { get; set; }
        public short GradeId { get; set; }
        public byte UrgencyId { get; set; }
        public Nullable<int> ShirtSizeId { get; set; }
        public Nullable<int> PantSizeId { get; set; }
        public Nullable<int> PantLengthSizeId { get; set; }
        public string UnderwearSize { get; set; }
        public string ShoeSize { get; set; }
        public string Comments { get; set; }
        public System.DateTime RowCreatedTime { get; set; }
        public string RowCreatedByUserId { get; set; }
        public System.DateTime RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedByUserId { get; set; }
    }
}