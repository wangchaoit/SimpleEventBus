using Microsoft.Extensions.DependencyInjection;

namespace SimpleEventBus
{
    /// <summary>
    /// 事件总线对象
    /// </summary>
    public class SimpleEventBus : ISimpleEventBus
    {
        private readonly IServiceProvider _provider;
        public SimpleEventBus(IServiceProvider provider)
        {
            _provider = provider;
        }
        /// <summary>
        /// 调用带返回参数的事件总线
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            var handler = _provider.GetService<IRequestHandler<TRequest, TResponse>>();
            if (handler == null)
                return default!;
            return await handler.HandleAsync(request);
        }
        /// <summary>
        /// 调用不带返回参数的事件总线，存在多个处理函数时执行最后一个注入的处理程序
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task Send<TRequest>(TRequest request) where TRequest : IRequest
        {
            var handler = _provider.GetService<IRequestHandler<TRequest>>();
            if (handler == null)
                return;
            await handler.HandleAsync(request);
        }
        /// <summary>
        /// 调用不带返回参数的事件总线，存在多个处理函数时挨个处理
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task Publish<TRequest>(TRequest request) where TRequest : IRequest
        {
            var handlers = _provider.GetServices<IRequestHandler<TRequest>>();
            if (handlers == null)
                return;
            foreach (var handler in handlers)
            {
                await handler.HandleAsync(request);
            }
        }
    }

}
