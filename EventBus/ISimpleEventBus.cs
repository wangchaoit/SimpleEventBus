using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventBus
{
    /// <summary>
    /// 事件总线接口
    /// </summary>
    public interface ISimpleEventBus
    {
        /// <summary>
        /// 调用带返回参数的事件总线
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> Send<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;
        /// <summary>
        /// 调用不带返回参数的事件总线，存在多个处理函数时执行最后一个注入的处理程序
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Send<TRequest>(TRequest request) where TRequest : IRequest;
        /// <summary>
        /// 调用不带返回参数的事件总线，存在多个处理函数时挨个处理
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Publish<TRequest>(TRequest request) where TRequest : IRequest;
    }
}
