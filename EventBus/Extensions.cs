using Microsoft.Extensions.DependencyInjection;

namespace SimpleEventBus
{
    public static class Extensions
    {
        /// <summary>
        /// 将SimpleEventBus添加到Ioc容器
        /// </summary>
        /// <param name="services"></param>
        public static void AddSimpleEventBus(this IServiceCollection services)
        {
            var eventHandlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.ExportedTypes)
                .Where(x => !x.IsInterface && !x.IsAbstract)
                .Where(x => x.GetInterfaces().Any(x => x.IsGenericType && new Type[] { typeof(IRequestHandler<>), typeof(IRequestHandler<,>) }.Contains(x.GetGenericTypeDefinition())))
                .ToArray();
            foreach (var handler in eventHandlers)
            {
                var interfaces = handler.GetInterfaces();
                // 根据自己的应用场景，这里需要调整
                services.AddTransient(handler.GetInterfaces()[0], handler);
            }
            services.AddSingleton<ISimpleEventBus, SimpleEventBus>();
        }
    }
}
