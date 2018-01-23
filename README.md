# ServiceFabricExample
Simple example of a Web API communicating to a stateless service using custom JSON serialization middleware.

Built using service fabric runtime `6.0.219` and SDK `2.8.219`.

Example demonstrates a WebAPI container communicating with a stateless service container with custom JSON serialize/deserialize middleware. 

## Steps to repro issue with serialization ##

The above solution repros an issue with the boilerplate serialization library offered by Microsoft.

Issuing a request where the method signature contains a `Guid` datatype causes a `InvalidCastException`.

** Works **

    POST localhost:8066/api/foo
    RESPONSE
    {
        "key": "198f",
        "value": "5a2835e46ca3445cb8afa8bea9e16872"
    }

** Works **

    GET localhost:8066/api/foo/198f
    RESPONSE
    {
        "key": "198f",
        "value": "5a2835e46ca3445cb8afa8bea9e16872"
    }

** Works **

    POST localhost:8066/api/bar
    RESPONSE
    {
        "key": "e712a398-c0b8-43bb-ba03-7a4fdd155b25",
        "value": "2a660e22be4747e88fcb3f4aec7bf26d"
    }

** Boom **

    GET localhost:8066/api/bar/e712a398-c0b8-43bb-ba03-7a4fdd155b25
    EXCEPTION
    InvalidCastException: Specified cast is not valid.
    Microsoft.ServiceFabric.Services.Communication.Client.ServicePartitionClient+<InvokeWithRetryAsync>d__24.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    Microsoft.ServiceFabric.Services.Remoting.V2.Client.ServiceRemotingPartitionClient+<InvokeAsync>d__2.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    Microsoft.ServiceFabric.Services.Remoting.Builder.ProxyBase+<InvokeAsyncV2>d__16.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    Microsoft.ServiceFabric.Services.Remoting.Builder.ProxyBase+<ContinueWithResultV2>d__15.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    lambda_method(Closure, object)
    Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeActionMethodAsync>d__12.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeNextActionFilterAsync>d__10.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
    Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(ref State next, ref Scope scope, ref object state, ref bool isCompleted)
    Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker+<InvokeInnerFilterAsync>d__14.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker+<InvokeNextResourceFilter>d__22.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Rethrow(ResourceExecutedContext context)
    Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker.Next(ref State next, ref Scope scope, ref object state, ref bool isCompleted)
    Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker+<InvokeFilterPipelineAsync>d__17.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    Microsoft.AspNetCore.Mvc.Internal.ResourceInvoker+<InvokeAsync>d__15.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    Microsoft.AspNetCore.Builder.RouterMiddleware+<Invoke>d__4.MoveNext()
    System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
    System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
    Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware+<Invoke>d__7.MoveNext()