using Microsoft.EntityFrameworkCore;
using MyProperty.Data.Entities;
using System;
using System.Linq;

namespace MyProperty.Data
{
    public class MyPropertyContext : DbContext
    {
        #region Constructor(s)

        public MyPropertyContext(DbContextOptions<MyPropertyContext> options) : base(options)
        {

        }

        #endregion

        #region DbSet Properties

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<AssignedProperty> AssignedProperties { get; set; }

        public DbSet<AssignedPropertyHistory> AssignedPropertyHistories { get; set; }

        public DbSet<PropertyOwner> PropertyOwners { get; set; }

        public DbSet<Payment> Payments { get; set; }

        #endregion

        #region Overridable method(s)

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.OwnerId);

                entity.ToTable("owners");

                entity.Property(e => e.OwnerId).HasColumnName("id");

                entity.Property(e => e.UserName).HasColumnName("username");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Deleted).HasColumnName("deleted");
            });

            builder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.PropertyId);

                entity.ToTable("properties");

                entity.Property(e => e.PropertyId).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Rent).HasColumnName("rent");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.PropertyOwnerId).HasColumnName("property_owner_id");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Deleted).HasColumnName("deleted");
            });

            builder.Entity<Tenant>(entity =>
            {
                entity.HasKey(e => e.TenantId);

                entity.ToTable("tenants");

                entity.Property(e => e.TenantId).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Deleted).HasColumnName("deleted");
            });

            builder.Entity<AssignedProperty>(entity =>
            {
                entity.HasKey(e => e.AssignedPropertyId);

                entity.ToTable("assigned_properties");

                entity.Property(e => e.AssignedPropertyId).HasColumnName("id");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.Rent).HasColumnName("rent");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Deleted).HasColumnName("deleted");
            });


            builder.Entity<AssignedPropertyHistory>(entity =>
            {
                entity.HasKey(e => e.AssignedPropertyHistoryId);

                entity.ToTable("assigned_property_histories");

                entity.Property(e => e.AssignedPropertyHistoryId).HasColumnName("id");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.Rent).HasColumnName("rent");

                entity.Property(e => e.DateFrom).HasColumnName("date_from");

                entity.Property(e => e.DateTo).HasColumnName("date_to");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Deleted).HasColumnName("deleted");
            });

            builder.Entity<PropertyOwner>(entity =>
            {
                entity.HasKey(e => e.PropertyOwnerId);

                entity.ToTable("properties_owners");

                entity.Property(e => e.PropertyOwnerId).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Deleted).HasColumnName("deleted");
            });

            builder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.ToTable("payments");

                entity.Property(e => e.PaymentId).HasColumnName("id");

                entity.Property(e => e.PropertyId).HasColumnName("property_id");

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");

                entity.Property(e => e.PropertyOwnerId).HasColumnName("property_owner_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Credit).HasColumnName("credit");

                entity.Property(e => e.Debit).HasColumnName("debit");

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

                entity.Property(e => e.UpdatedOn).HasColumnName("updated_on");

                entity.Property(e => e.Deleted).HasColumnName("deleted");
            });
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            return base.SaveChanges();
        }

        #endregion

        #region Private Method(s)

        private void UpdateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }

        #endregion
    }
}
