namespace SimpleEventBus
{
    /// <summary>
    /// 事件总线请求对象接口
    /// </summary>
    public interface IRequest { }
    /// <summary>
    /// 事件总线处理程序接口
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    public interface IRequestHandler<TIn> where TIn : IRequest
    {
        /// <summary>
        /// 执行对象处理方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task HandleAsync(TIn request);
    }
    /// <summary>
    /// 事件总线请求对象接口（带返回参数）
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public interface IRequest<TOut> { }
    /// <summary>
    /// 事件总线处理程序接口（带返回参数）
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public interface IRequestHandler<TIn, TOut> where TIn : IRequest<TOut>
    {
        /// <summary>
        /// 执行对象处理方法
        /// </summary>
        /// <param name="request">返回处理结果</param>
        /// <returns></returns>
        Task<TOut> HandleAsync(TIn request);
    }
}