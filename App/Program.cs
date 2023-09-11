using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleEventBus;

namespace App
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                // 注入容器
                .ConfigureServices(service => service.AddSimpleEventBus())
                .Build();

            var eventBus = host.Services.GetRequiredService<ISimpleEventBus>();

            // 发送一个带返回参数的消息
            var ret = await eventBus.Send<UserQueryRequest, UserQueryResponse>(new UserQueryRequest { Id = 0, Name = "zhangsan" });

            // 发送一个不带返回参数的消息
            await eventBus.Send(new NoResponseRequest { Id = 1 });

            // 发布一个消息可以被多个处理函数处理，无法携带返回参数
            await eventBus.Publish(new NoResponseRequest { Id = 2 });

            await host.RunAsync();
        }
    }

    public class UserQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class UserQueryRequest : IRequest<UserQueryResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// 带返回参数的事件处理程序
    /// </summary>
    public class UserQueryHandler : IRequestHandler<UserQueryRequest, UserQueryResponse>
    {
        public Task<UserQueryResponse> HandleAsync(UserQueryRequest request)
        {
            // todo 业务逻辑的处理
            return Task.FromResult(new UserQueryResponse { CreateTime = DateTime.UtcNow, Id = request.Id, Name = request.Name });
        }
    }

    public class NoResponseRequest : IRequest
    {
        public int Id { get; set; } = 0;
    }

    /// <summary>
    /// 不带返回参数的事件处理程序
    /// </summary>
    public class NoResponseRequestHandler : IRequestHandler<NoResponseRequest>
    {
        public Task HandleAsync(NoResponseRequest request)
        {
            // todo 业务逻辑的处理
            return Task.CompletedTask;
        }
    }
    /// <summary>
    /// 不带返回参数的事件处理程序；
    /// Publish 时 NoResponseRequestHandler， MultipleHandler 均会响应并执行 HandleAsync
    /// </summary>
    public class MultipleHandler : IRequestHandler<NoResponseRequest>
    {
        public Task HandleAsync(NoResponseRequest request)
        {
            // todo 业务逻辑的处理
            return Task.CompletedTask;
        }
    }
}