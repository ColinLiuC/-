using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CoreSolution.Domain;
using CoreSolution.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreSolution.EntityFrameworkCore
{
    public partial class EfCoreDbContext : DbContext
    {
        public EfCoreDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                DbContextConfigurer.Configure(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*var typesToRegister = Assembly.Load("CoreSolution.Domain").GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(Entity<>));*/
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("CoreSolution.Domain"));
            //foreach (var type in typesToRegister)
            //{
            //    var instance = Activator.CreateInstance(type);
            //    //获取实体的类型
            //    Type typeEntity = type.GetInterfaces().First(t => IsIEntityTypeConfigurationType(t)).GenericTypeArguments[0];
            //    modelBuilder.ApplyConfiguration((typeEntity)instance);
            //}
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
   
        public virtual DbSet<AuditLog> AuditLogs { get; set; }


        public virtual DbSet<DataDictionary> DataDictionarys { get; set; }

        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Notice> Notices { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Street> Street { get; set; }

        public virtual DbSet<ResidentWork> ResidentWork { get; set; }

        public virtual DbSet<ResidentWork_Attach> ResidentWork_Attach { get; set; }

        public virtual DbSet<WorkDispose> WorkDispose { get; set; }

        public virtual DbSet<WorkTransact> WorkTransact { get; set; }

        public virtual DbSet<ReceptionService> ReceptionServices { get; set; }

        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<PeopleAndActivity> PeopleAndActivities { get; set; }
        public virtual DbSet<PeopleAndReceptionService> PeopleAndReceptionServices { get; set; }
        public virtual DbSet<ActivityRegister> ActivityRegisters { get; set; }
        public virtual DbSet<ServiceApplication> ServiceApplications { get; set; }

        public virtual DbSet<DoorCard> DoorCard { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Quarters> Quarters { get; set; }
        public virtual DbSet<House> House { get; set; }
        public virtual DbSet<JuWei> JuWei { get; set; }
        public virtual DbSet<ParkingLot> ParkingLot { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<RegisterHistory> RegisterHistory { get; set; }
        public virtual DbSet<OldPeople> OldPeoples { get; set; }
        public virtual DbSet<Register> Registers { get; set; }
        public virtual DbSet<Disability> Disabilities { get; set; }
        public virtual DbSet<SpecialCare> SpecialCares { get; set; }
        public virtual DbSet<OtherPeople> OtherPeoples { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<OrganizationProject> OrganizationProject { get; set; }
        public virtual DbSet<ServiceEvaluation> ServiceEvaluations { get; set; }
        public virtual DbSet<ActivityEvaluation> ActivityEvaluations { get; set; }

    }
}
