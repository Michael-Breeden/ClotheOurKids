﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ClotheOurKidsEntities : DbContext
    {
        public ClotheOurKidsEntities()
            : base("name=ClotheOurKidsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AgeGroup> AgeGroups { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<OfficeType> OfficeTypes { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<PositionOfficeType> PositionOfficeTypes { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolDistrict> SchoolDistricts { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus1 { get; set; }
        public virtual DbSet<Urgency> Urgencies { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<ContactMethod> ContactMethods { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<PantSize> PantSizes { get; set; }
        public virtual DbSet<ShirtSize> ShirtSizes { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
    }
}
