using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.Interfaces.Base;
using RestWithASPNET.Domain.Interfaces.Repositories;
using RestWithASPNET.Domain.Mediator.Interfaces;
using RestWithASPNET.Domain.Messages;
using RestWithASPNET.FrameWrkDrivers.Extensions;
using RestWithASPNET.FrameWrkDrivers.Mapping;
using System.Data.Common;

namespace RestWithASPNET.FrameWrkDrivers.Data.Context
{
    public class MySQLContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public MySQLContext(DbContextOptions<MySQLContext> options, IMediatorHandler mediatorHandler) : 
            base(options) 
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            base.OnModelCreating(modelBuilder);

            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IBase).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }

            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new BookMap());
        }

        public async Task<bool> Commit()
        {
            if (await base.SaveChangesAsync() <= 0)
                return false;

            await _mediatorHandler.PublishEvents(this);

            return true;
        }

        public bool DatabaseExists()
        {
            try
            {
                return Database.GetService<IRelationalDatabaseCreator>().Exists();
            }
            catch (DbException)
            {
                return false;
            }
        }
    }
}