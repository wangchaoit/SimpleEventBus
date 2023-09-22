# 基于 dotnet ioc 实现的简单事件总线组件，项目参考了 MediatR 项目的设计思路
## 一、发送一个带返回数据的消息
### SimpleEventBus.Send<TIn, TOut>(TInObject)
## 二、发送一个不带返回数据的消息
### SimpleEventBus.Send(TInObject)
## 三、发送一个消息可以被多个消息处理程序接收并处理
### SimpleEventBus.Publish(TInObject)