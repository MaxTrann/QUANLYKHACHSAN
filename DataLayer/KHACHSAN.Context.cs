﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_CongTy> tb_CongTy { get; set; }
        public virtual DbSet<tb_DatPhong> tb_DatPhong { get; set; }
        public virtual DbSet<tb_DatPhong_CT> tb_DatPhong_CT { get; set; }
        public virtual DbSet<tb_DatPhong_SanPham> tb_DatPhong_SanPham { get; set; }
        public virtual DbSet<tb_DonVi> tb_DonVi { get; set; }
        public virtual DbSet<tb_KhachHang> tb_KhachHang { get; set; }
        public virtual DbSet<tb_LoaiPhong> tb_LoaiPhong { get; set; }
        public virtual DbSet<tb_Param> tb_Param { get; set; }
        public virtual DbSet<tb_Phong> tb_Phong { get; set; }
        public virtual DbSet<tb_Phong_ThietBi> tb_Phong_ThietBi { get; set; }
        public virtual DbSet<tb_SanPham> tb_SanPham { get; set; }
        public virtual DbSet<tb_SYS_FUNC> tb_SYS_FUNC { get; set; }
        public virtual DbSet<tb_SYS_GROUP> tb_SYS_GROUP { get; set; }
        public virtual DbSet<tb_SYS_RIGHT> tb_SYS_RIGHT { get; set; }
        public virtual DbSet<tb_SYS_RIGHT_REP> tb_SYS_RIGHT_REP { get; set; }
        public virtual DbSet<tb_SYS_USER> tb_SYS_USER { get; set; }
        public virtual DbSet<tb_Tang> tb_Tang { get; set; }
        public virtual DbSet<tb_ThietBi> tb_ThietBi { get; set; }
        public virtual DbSet<tb_SYS_REPORT> tb_SYS_REPORT { get; set; }
        public virtual DbSet<V_USER_NOTIN_GROUP> V_USER_NOTIN_GROUP { get; set; }
        public virtual DbSet<V_USER_IN_GROUP> V_USER_IN_GROUP { get; set; }
        public virtual DbSet<V_FUNC_SYS_RIGHT> V_FUNC_SYS_RIGHT { get; set; }
        public virtual DbSet<V_REP_SYS_RIGHT_REP> V_REP_SYS_RIGHT_REP { get; set; }
    }
}
