﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.Entities.Models;

#nullable disable

namespace WebApi.Entities.DdContextTcrb
{
    public partial class TcrbContext : DbContext
    {
        public TcrbContext()
        {
        }

        public TcrbContext(DbContextOptions<TcrbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MsGroup> MsGroup { get; set; }
        public virtual DbSet<MsQuestion> MsQuestion { get; set; }
        public virtual DbSet<QuestionGroup> QuestionGroup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<MsGroup>(entity =>
            {
                entity.Property(e => e.IdGroup).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MsQuestion>(entity =>
            {
                entity.Property(e => e.IdQuestion).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<QuestionGroup>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}