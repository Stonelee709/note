# 	过滤器

步骤：

1)新建类实现种过滤器的接口(IResourceFilter,IActionFilter) 

2) 注册过滤器

​	a) 通过属性。在接口继承 Attribute 并在类名添加Attribute 后缀,然后将此属性添加到控制器或控制器方法上

​	b) 配置全局过滤器。在 startup 的 ConfigurationServices 中：

```c#
		 services.AddMvc(options =>
            {
                //所有请示使用一个实例
                options.Filters.Add(new CacheResourceAttribute());
                //每个请求创建一个实例
                options.Filters.Add(typeof(CacheResourceAttribute));
            });
```

示例：

通过资源过滤器判断缓存中是否有要的信息，如果有直接返回，如果没有就执行 ACTION，在执行 ACTION 后，将 ACTION 的结果放入缓存



```c#
public class CacheResourceAttribute: IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
		}

​    public void OnResourceExecuting(ResourceExecutingContext context)
​    {
​        //需要using Microsoft.Extensions.DependencyInjection;
​        IMemoryCache cache = (IMemoryCache)context.HttpContext.RequestServices.GetService<IMemoryCache>();
​        //将路径设置为 KEY
​        string path=context.HttpContext.Request.Path.ToString();
​        object value = null;
​        //判断是否能找到
​        if(cache.TryGetValue(path,out value))
​        {
​            //拿到缓存的结果
​            string result = value.ToString();
​            //在缓存中找到，直接返回视图，后续就不会再执行了
​            context.Result = new ContentResult() { Content = result };
​        }
​    }
}
```

```c#
public class CacheActionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //获取动作结果
            IActionResult result = context.Result;
            //判断结果是否是ContentResult，如果是，获取数据并缓存
            if(result is ContentResult)
            {
                ContentResult contentResult = result as ContentResult;
                string content = contentResult.Content;
                string path = context.HttpContext.Request.Path.ToString();
                IMemoryCache cache = context.HttpContext.RequestServices.GetService<IMemoryCache>();
                cache.Set(path, content);

​        }
​    }

​    public void OnActionExecuting(ActionExecutingContext context)
​    {
​       
​    }
}
```

