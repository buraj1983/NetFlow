using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using NetFlow.Api;
using NetFlow.Infrastructure.Database;
using NetFlow.Infrastructure.Database.SqlQueries;
using NetFlow.Infrastructure.EventSourcing;
using NetFlow.Infrastructure.EventSourcing.NEventStore;
using NetFlow.Infrastructure.EventSourcing.NEventStore.Sql;
using NetFlow.Infrastructure.Extensions;
using NetFlow.Infrastructure.Messaging;
using NetFlow.Infrastructure.Messaging.Handling;
using NetFlow.Infrastructure.Messaging.InMemory;
using NetFlow.Infrastructure.Queries;
using NetFlow.Queries;
using NetFlow.Queries.Security.Dto;
using NetFlow.Queries.Security.Queries;
using Module = Autofac.Module;

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
                    try
                    {
                        //var accounts = scope.Resolve<IAccountService>();
                        //accounts.Register("name", "xxx", null, null, null);


                        var account =
                            scope.Resolve<IQueryProcessor>()
                                .Process<FindUserByLogin, UserDto>(new FindUserByLogin {Login = "test"});
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
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

            #region - CQRS -

            var assembliesToScan = new[] {Assembly.Load("NetFlow.Domain"), Assembly.Load("NetFlow.Queries")};

            var eventHandlerOpenType = typeof(IEventHandler<>);
            var commandHandlerOpenType = typeof(ICommandHandler<>);
            var queryHandlerOpenType = typeof(IQueryHandler<,>);

            //Event Handlers
            builder.RegisterAssemblyTypes(assembliesToScan)
                .Where(type => type.GetClosedTypeInterfaces(eventHandlerOpenType).Any())
                .AsClosedTypesOf(eventHandlerOpenType)
                .InstancePerDependency();
            
            //Command Handlers
            builder.RegisterAssemblyTypes(assembliesToScan)
                .Where(type => type.GetClosedTypeInterfaces(commandHandlerOpenType).Any())
                .AsClosedTypesOf(commandHandlerOpenType)
                .InstancePerDependency();

            //Query Handlers
            builder.RegisterAssemblyTypes(assembliesToScan)
                .Where(type => type.GetClosedTypeInterfaces(queryHandlerOpenType).Any())
                .AsClosedTypesOf(queryHandlerOpenType)
                .InstancePerDependency();

            //Query Handlers Factory
            builder.Register<QueryHandlerFactory>(
                context =>
                    ((queryType, resultType) =>
                        context.Resolve(queryHandlerOpenType.MakeGenericType(queryType, resultType)) as IQueryHandler))
                .SingleInstance();

            //Query Handler Processor
            builder.RegisterType<QueryProcessor>()
                .As<IQueryProcessor>()
                .OnActivated(e =>
                {
                    // Gets all registered in container query handlers and register them in processor
                    var supportedHandlersByClass =
                        e.Context.ComponentRegistry.Registrations.Select(
                            r =>
                                new
                                {
                                    type = r.Activator.LimitType,
                                    supportedHandlers =
                                        r.Services.OfType<IServiceWithType>()
                                            .Where(s => s.ServiceType.IsClosedType(queryHandlerOpenType))
                                })
                                .Where(x => x.supportedHandlers.Any())
                                .ToArray();

                    var registerMethod = typeof(IQueryProcessor).GetMethod("RegisterHandler");
                    foreach (var handlersByType in supportedHandlersByClass)
                    {
                        foreach (var handler in handlersByType.supportedHandlers)
                        {
                            var genericArguments = handler.ServiceType.GetGenericArguments();

                            var registerGenericMethod =
                                registerMethod.MakeGenericMethod(handlersByType.type, genericArguments[0],
                                    genericArguments[1]);
                            registerGenericMethod.Invoke(e.Instance, null);
                        }
                    }
                })
                .InstancePerDependency();


            // Query Data Access
            builder.RegisterType<QuerySqlContext>()
                .As<IReadOnlyDbContext>()
                .As<IDbContext>()
                .InstancePerDependency();
            
            #endregion

            #region - Messaging -

            //builder.Register<CommandHandlerFactory>(
            //    delegate(IComponentContext context)
            //    {
            //        var ctx = context.Resolve<IComponentContext>();
            //        return
            //            commandType =>
            //                ctx.Resolve(typeof(ICommandHandler<>).MakeGenericType(commandType)) as ICommandHandler;
            //    }).SingleInstance();

            //builder.Register<EventHandlerFactory>(
            //    delegate(IComponentContext context)
            //    {
            //        var ctx = context.Resolve<IComponentContext>();
            //        return
            //            eventType =>
            //                ctx.Resolve(typeof(IEventHandler<>).MakeGenericType(eventType)) as IEventHandler;
            //    }).SingleInstance();

            //builder.Register(
            //    (c, p) =>
            //        new CommandDispatcher(
            //            c.Resolve<CommandHandlerFactory>())
            //            .RegisterCommandHandlerFromAssemblies(DomainAssemblies.ToArray()))
            //    .As<ICommandDispatcher>()
            //    .SingleInstance();

            //builder.Register(
            //    (c, p) =>
            //        new EventDispatcher(
            //            c.Resolve<EventHandlerFactory>())
            //            .RegisterEventHandlerFromAssemblies(DomainAssemblies.ToArray()))
            //    .As<IEventDispatcher>()
            //    .SingleInstance();
            
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

    


    
    //class QueryModule : Module
    //{
    //    private readonly IEnumerable<Assembly> _assemblies;

    //    public QueryModule(IEnumerable<Assembly> assemblies)
    //    {
    //        if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
    //        _assemblies = assemblies;
    //    }

    //    protected override void Load(ContainerBuilder builder)
    //    {
            
    //        RegisterHandlersFromAssemblies(builder, typeof(IRequestHandler<,>));
    //        RegisterHandlersFromAssemblies(builder, typeof(IRequestHandlerAsync<,>));

    //        builder.Register(
    //            (c, p) =>
    //            {
    //                var processor = new RequestProcessor(null, null);
                    
    //                return processor;
    //            })
    //            .As<IRequestProcessor>()
    //            .As<IRequestProcessorAsync>()
    //            .OnActivated(OnProcessorActivated)
    //            .SingleInstance();
    //    }
        
    //    private void RegisterHandlersFromAssemblies(ContainerBuilder builder, Type openGenericHandler)
    //    {
    //        var handlersByTargetType =
    //            _assemblies.SelectMany(a => a.GetTypes())
    //                .Where(t => t.HasClosedGenericInterfaceFrom(openGenericHandler))
    //                .ToDictionary(k => k, e => e.GetClosedGenericInterfacesFrom(openGenericHandler));

    //        foreach (var lookup in handlersByTargetType)
    //        {
    //            builder.RegisterType(lookup.Key)
    //                .As(lookup.Value.ToArray())
    //                .InstancePerDependency();
    //        }
    //    }

    //    private static void OnProcessorActivated(IActivatedEventArgs<RequestProcessor> processorActivatedArgs)
    //    {
    //        RegisterInProcessorHandlersFromContainer(processorActivatedArgs);
    //        RegisterInProcessorAsyncHandlersFromContainer(processorActivatedArgs);
    //    }

    //    private static void RegisterInProcessorHandlersFromContainer(
    //        IActivatedEventArgs<RequestProcessor> processorActivatedArgs)
    //    {
    //        var ssss =
    //            processorActivatedArgs.Context.ComponentRegistry.Registrations.Where(
    //                r =>
    //                    r.Services.OfType<IServiceWithType>()
    //                        .Any(t => t.ServiceType.IsClosedGenericFrom(typeof(IRequestHandler<,>)))).ToArray();


    //        var kon = processorActivatedArgs.Context.ComponentRegistry.Registrations
    //            .Select(r => new
    //            {
    //                instanceType = r.Activator.LimitType,
    //                handlerTypes = r.Services.OfType<IServiceWithType>().Where(t => t.ServiceType.IsClosedGenericFrom(typeof(IRequestHandler<,>)))
    //            }).Where(x => x.handlerTypes.Any()).ToArray();


    //        foreach (var registerHandler in ssss)
    //        {
    //            var gg = 4;
    //        }
            
    //    }

    //    private static void RegisterInProcessorAsyncHandlersFromContainer(
    //        IActivatedEventArgs<RequestProcessor> processorActivatedArgs)
    //    {
    //        var ssss = processorActivatedArgs.Context.ComponentRegistry.Registrations.Where(r => r.Services.OfType<IServiceWithType>().Any(t => t.ServiceType.IsClosedGenericFrom(typeof(IRequestHandlerAsync<,>)))).ToArray();

            
    //    }
        
    //}
}
