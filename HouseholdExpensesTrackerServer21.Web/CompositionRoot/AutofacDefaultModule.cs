using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Configuration;
using HouseholdExpensesTrackerServer21.Common.Event;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Repositories;
using HouseholdExpensesTrackerServer21.Domain.Households.Repositories;
using HouseholdExpensesTrackerServer21.Domain.Identities.Repositories;
using HouseholdExpensesTrackerServer21.Domain.Savings.Repositories;
using HouseholdExpensesTrackerServer21.Infrastructure.Dispatchers;
using HouseholdExpensesTrackerServer21.Infrastructure.Repositories;
using HouseholdExpensesTrackerServer21.Web.Configurations;
using Microsoft.Extensions.Configuration;
using static HouseholdExpensesTrackerServer21.Web.Core.Consts;

namespace HouseholdExpensesTrackerServer21.Web.CompositionRoot
{
    public class AutofacDefaultModule : Autofac.Module
    {
        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            this.RegisterConfigurations(builder);
            RegisterEvents(builder);
            RegisterCommands(builder);
            RegisterQueries(builder);
            RegisterRepositories(builder);
        }

        private void RegisterConfigurations(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                return ApplicationConfiguration.Create(
                    this.Configuration.GetConnectionString(ApplicationConfigurationKeys.HouseholdConnectionString),
                    60);
            })
            .As<IApplicationConfiguration>()
            .SingleInstance();
            
        }

        private static void RegisterEvents(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return new EventDispatcher(context);
            })
            .As<IEventDispatcherAsync>()
            .InstancePerLifetimeScope();
        }

        private static void RegisterCommands(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return new CommandDispatcher(context);
            })
            .As<ICommandDispatcherAsync>()
            .InstancePerLifetimeScope();

            var assembly = Assembly.Load(new AssemblyName("HouseholdExpensesTrackerServer21.Application"));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandlerAsync<>));
        }

        private static void RegisterQueries(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return new QueryDispatcher(context);
            })
            .As<IQueryDispatcherAsync>()
            .InstancePerLifetimeScope();

            var assembly = Assembly.Load(new AssemblyName("HouseholdExpensesTrackerServer21.Application"));
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IQueryHandlerAsync<,>));
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<HouseholdRepository>()
                .As<IHouseholdRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SavingTypeRepository>()
                .As<ISavingTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExpenseTypeRepository>()
                .As<IExpenseTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionRepository>()
                .As<IPermissionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>()
                .As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CredentialTypeRepository>()
                .As<ICredentialTypeRepository>().InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(EntityFrameworkRepository<,>))
            //    .As(typeof(IRepository<,>));
        }
    }
}
