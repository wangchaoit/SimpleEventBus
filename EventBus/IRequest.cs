namespace SimpleEventBus
{
    /// <summary>
    /// 事件总线请求对象接口
    /// </summary>
    public interface IRequest { }
    /// <summary>
    /// 事件总线处理程序接口
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IRequestHandler<TRequest> where TRequest : IRequest
    {
        /// <summary>
        /// 执行对象处理方法
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task HandleAsync(TRequest request);
    }
    /// <summary>
    /// 事件总线请求对象接口（带返回参数）
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IRequest<TResponse> { }
    /// <summary>
    /// 事件总线处理程序接口（带返回参数）
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// 执行对象处理方法
        /// </summary>
        /// <param name="request">返回处理结果</param>
        /// <returns></returns>
        Task<TResponse> HandleAsync(TRequest request);
    }
}