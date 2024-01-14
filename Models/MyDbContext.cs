using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace linebot02.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Meal> Meals { get; set; } = null!;
        public virtual DbSet<MealAlias> MealAliases { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<RestaurantAlias> RestaurantAliases { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAlias> UserAliases { get; set; } = null!;

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {

            
//             if (!optionsBuilder.IsConfigured)
//             {
// // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                 optionsBuilder.UseMySql("", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("meal");

                entity.Property(e => e.MealId).HasColumnName("meal_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.MealName)
                    .HasMaxLength(255)
                    .HasColumnName("meal_name");

                entity.Property(e => e.MealPrice).HasColumnName("meal_price");

                entity.Property(e => e.MealPriceUnit)
                    .HasColumnType("enum('NTD','USD')")
                    .HasColumnName("meal_price_unit");
            });

            modelBuilder.Entity<MealAlias>(entity =>
            {
                entity.ToTable("meal_alias");

                entity.HasIndex(e => e.MealId, "meal_id");

                entity.Property(e => e.MealAliasId).HasColumnName("meal_alias_id");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .HasColumnName("alias");

                entity.Property(e => e.AliasPrice).HasColumnName("alias_price");

                entity.Property(e => e.AliasPriceUnit)
                    .HasColumnType("enum('NTD','USD')")
                    .HasColumnName("alias_price_unit");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.MealId).HasColumnName("meal_id");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.MealAliases)
                    .HasForeignKey(d => d.MealId)
                    .HasConstraintName("meal_alias_ibfk_1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.RestaurantId, "restaurant_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_ibfk_1");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("order_detail");

                entity.HasIndex(e => e.MealAliasId, "meal_alias_id");

                entity.HasIndex(e => e.OrderId, "order_id");

                entity.HasIndex(e => e.UserAliasId, "user_alias_id");

                entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.MealAliasId).HasColumnName("meal_alias_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UserAliasId).HasColumnName("user_alias_id");

                entity.HasOne(d => d.MealAlias)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.MealAliasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_detail_ibfk_2");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_detail_ibfk_1");

                entity.HasOne(d => d.UserAlias)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.UserAliasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_detail_ibfk_3");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("restaurant");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.Location)
                    .HasMaxLength(1000)
                    .HasColumnName("location");

                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");

                entity.Property(e => e.RestaurantName)
                    .HasMaxLength(255)
                    .HasColumnName("restaurant_name");
            });

            modelBuilder.Entity<RestaurantAlias>(entity =>
            {
                entity.ToTable("restaurant_alias");

                entity.HasIndex(e => e.RestaurantId, "restaurant_id");

                entity.Property(e => e.RestaurantAliasId).HasColumnName("restaurant_alias_id");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .HasColumnName("alias");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantAliases)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("restaurant_alias_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<UserAlias>(entity =>
            {
                entity.ToTable("user_alias");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.UserAliasId).HasColumnName("user_alias_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserNameAlias)
                    .HasMaxLength(255)
                    .HasColumnName("user_name_alias");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAliases)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_alias_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
