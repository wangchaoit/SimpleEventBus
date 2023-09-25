一个基于 dotnet ioc 实现的简单事件总线组件，项目参考了 MediatR 项目的设计思路.
### 支持一下几种使用方式
1. 发送带返回数据的消息
2. 发送不带返回数据的消息
3. 发布消息可以被多个消息处理程序接收并处理
 
### 使用方式

#### 1. 将 SimpleEventBus 注入到容器
```csharp
services.AddSimpleEventBus();
```
#### 2. 定义消息
```csharp
// 带返回参数的消息定义
public class OrderMessage : IRequest<string>
{
	public int OrderId { get; set; }
}
```
#### 3. 定义消息处理类(带返回参数)
```csharp
public class RequestHandler : IRequest<OrderMessage>
{
	private readonly ILogger<RequestHandler> _logger;
	public RequestHandler(ILogger<RequestHandler> logger) 
	{
		_logger = logger;
	}

	public Task<string> HandleAsync(OrderMessage request)
	{
		// todo 业务逻辑的处理
		return Task.FromResult(request.OrderId);
	}
}
```
#### 4. 定义消息处理类(不带返回参数)
```csharp
public class RequestHandler : IRequest
{
	private readonly ILogger<RequestHandler> _logger;
	public RequestHandler(ILogger<RequestHandler> logger) 
	{
		_logger = logger;
	}

	public Task HandleAsync(SomeRequest request)
	{
		// todo 业务逻辑的处理
		return Task.CompleteTask;
	}
}
```
#### 5. 定义消息处理类(多个处理程序)
```csharp
public class RequestHandler : INotify
{
	private readonly ILogger<RequestHandler> _logger;
	public RequestHandler(ILogger<RequestHandler> logger) 
	{
		_logger = logger;
	}

	public Task HandleAsync(SomeRequest request)
	{
		// todo 业务逻辑的处理
		return Task.CompleteTask;
	}
}
```
#### 6. 从容器中获取 SimpleEventBus 对象
```csharp
var eventBus = provider.GetRequiredService<ISimpleEventBus>();
```
#### 7. 调用并发送/发布消息
a. 发送一条带返回数据的消息
```csharp
eventBus.Send<TIn, TOut>(TInObject)

```
b. 发送一条不带返回参数的消息
```csharp
eventBus.Send(TInObject)
```
c. 发布一条消息可以被多个处理程序接收并处理
```csharp
eventBus.Publish(TInObject)
```