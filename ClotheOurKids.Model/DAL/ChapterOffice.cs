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
    
    public partial class ChapterOffice
    {
        public int ChapterOfficeId { get; set; }
        public int ChapterId { get; set; }
        public short OfficeId { get; set; }
    
        public virtual Chapter Chapter { get; set; }
        public virtual Office Office { get; set; }
    }
}
