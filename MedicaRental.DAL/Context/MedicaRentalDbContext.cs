﻿using MedicaRental.DAL.Configurations;
using MedicaRental.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MedicaRental.DAL.Context
{
    public class MedicaRentalDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubCategory> SubCategories => Set<SubCategory>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Report> Reports => Set<Report>();
        public DbSet<RentOperation> RentOperations => Set<RentOperation>();

        public MedicaRentalDbContext(DbContextOptions<MedicaRentalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ItemEntityTypeConfiguration());
            builder.ApplyConfiguration(new ClientEntityTypeConfiguration());
            builder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new SubCategoryEntityTypeConfiguration());
            builder.ApplyConfiguration(new ReviewEntityTypeConfiguration());
            builder.ApplyConfiguration(new MessageEntityTypeConfiguration());
            builder.ApplyConfiguration(new ReportEntityTypeConfiguration());
            builder.ApplyConfiguration(new RentOperationEntityTypeConfiguration());
        }

        public override void RemoveRange(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is ISoftDeletable deletable)
                {
                    deletable.IsDeleted = true;
                    Entry(deletable).State = EntityState.Modified;
                }
                else
                {
                    Entry(entity).State = EntityState.Deleted;
                }
            }
        }

        public override void RemoveRange(params object[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity is ISoftDeletable deletable)
                {
                    deletable.IsDeleted = true;
                    Entry(deletable).State = EntityState.Modified;
                }
                else
                {
                    Entry(entity).State = EntityState.Deleted;
                }
            }
        }
        
        public override EntityEntry Remove(object entity)
        {
            if (entity is ISoftDeletable deletable)
            {
                deletable.IsDeleted = true;
                Entry(deletable).State = EntityState.Modified;

                return Entry(entity);
            }
            else
            {
                return base.Remove(entity);
            }
        }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            if (entity is ISoftDeletable deletable)
            {
                deletable.IsDeleted = true;
                Entry(deletable).State = EntityState.Modified;

                return Entry(entity);
            }
            else
            {
                return base.Remove(entity);
            }
        }

        public async Task UpdateDailyRatings()
        {
           await Items.ForEachAsync(async i =>
            {
                i.Rating = await CalculateDailyRatingForItem(i.Id);
            });

            await SaveChangesAsync();
        }

        private async Task<int> CalculateDailyRatingForItem(Guid id)
        {
            var averageRating = await  Reviews.Where(r => r.ItemId == id).AverageAsync(r => r.Rating);

            return  (int)averageRating;
        }
    }
}
