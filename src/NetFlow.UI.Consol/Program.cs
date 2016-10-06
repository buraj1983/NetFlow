using System;
using System.Linq;
using System.Reflection;
using Autofac;
using NetFlow.Api;
using NetFlow.Infrastructure.EventSourcing;
using NetFlow.Infrastructure.EventSourcing.NEventStore;
using NetFlow.Infrastructure.EventSourcing.NEventStore.Sql;
using NetFlow.Infrastructure.Extensions;
using NetFlow.Infrastructure.Messaging;
using NetFlow.Infrastructure.Messaging.Handling;
using NetFlow.Infrastructure.Messaging.InMemory;

namespace NetFlow.UI.Consol
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IContainer container = CreateContainer())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var accounts = scope.Resolve<IAccountService>();
                    accounts.Register("name", "xxx", null, null, null);
                }
            }
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            // Register API services
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.IsClass && typeof(IService).IsAssignableFrom(t))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //Register Event handlers
            builder.RegisterAssemblyTypes(DomainAssemblies)
                .Where(t => t.IsHandler(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // Register Command handlers
            builder.RegisterAssemblyTypes(DomainAssemblies)
                .Where(t => t.IsHandler(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            #region - Messaging -

            builder.Register<CommandHandlerFactory>(
                delegate(IComponentContext context)
                {
                    var ctx = context.Resolve<IComponentContext>();
                    return
                        commandType =>
                            ctx.Resolve(typeof(ICommandHandler<>).MakeGenericType(commandType)) as ICommandHandler;
                }).SingleInstance();

            builder.Register<EventHandlerFactory>(
                delegate(IComponentContext context)
                {
                    var ctx = context.Resolve<IComponentContext>();
                    return
                        eventType =>
                            ctx.Resolve(typeof(IEventHandler<>).MakeGenericType(eventType)) as IEventHandler;
                }).SingleInstance();

            builder.Register(
                (c, p) =>
                    new CommandDispatcher(
                        c.Resolve<CommandHandlerFactory>())
                        .RegisterCommandHandlerFromAssemblies(DomainAssemblies.ToArray()))
                .As<ICommandDispatcher>()
                .SingleInstance();

            builder.Register(
                (c, p) =>
                    new EventDispatcher(
                        c.Resolve<EventHandlerFactory>())
                        .RegisterEventHandlerFromAssemblies(DomainAssemblies.ToArray()))
                .As<IEventDispatcher>()
                .SingleInstance();
            
            #endregion

            #region - Event Sourcing -
            
            builder.RegisterGeneric(typeof(EventSourcedFactory<>))
                .As(typeof(IEventSourcedFactory<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(EventSourcedRepository<>))
                .As(typeof(IEventSourcedRepository<>))
                .InstancePerDependency();

            builder.Register(
                (c, p) =>
                    NStoreBuilder.Setup()
                        .UseSqlPersistence("EventStore")
                        .UseDialect(SqlDialect.MsSql)
                        .InitializeStorageEngine()
                        .Build())
                .As<IEventStore>()
                .InstancePerDependency();

            #endregion

            return builder.Build();
        }

        private static Assembly[] DomainAssemblies => new[] { Assembly.Load("NetFlow.Domain") };
    }
}
