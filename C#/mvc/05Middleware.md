# 中间件

Token 验证机制：

将 request 请求的参数全部叠加在一起成一个字符串，忽略 Token 这个参数，然后将这个字符串与一个设定的字符串拼接，再进行 MD5 加密，然后将 TOKEN 参数与这个加密后的结果对比，判断是否正确。



1）编写中间件类

```c#
public class SignMiddleWare
    {
    //next 用于调用下一个中间件
        private RequestDelegate _next;
        public SignMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            IQueryCollection query= context.Request.Query;
            string str = "";
            foreach (var key in query.Keys)
            {
                if(key!="Sign")
                {
                    str += key + query[key];
                }
                
            }
            str += "指定字符串";
            string MD5str = GetMD5(str);

            if(MD5str==query["Sign"])
            {
                await _next(context);
            }else
            {
                context.Response.WriteAsync("Token Invalid");
            }
        }

        private string GetMD5(string str)
        {
            using (MD5 md5provider=MD5.Create())
            {
                byte[] result=md5provider.ComputeHash(Encoding.UTF8.GetBytes(str));
                string Resultstr = BitConverter.ToString(result);
                return Resultstr.Replace("-", string.Empty).ToLower();
            }
        }
    }
```

2) 在 startup 的 Configure 中注册中间件

```
app.UseMiddleware<SignMiddleWare>();
```



![image-20211113193515984](C:\Users\admin\AppData\Roaming\Typora\typora-user-images\image-20211113193515984.png)



#### 中间件

1判断请求的路径，然后做出不同的响应。

```c#
app.Use(next =>
            {
                return async httpContext =>
                {
                    if (httpContext.Request.Path.StartsWithSegments("/error"))
                    {
                        await httpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("hello"), 0, "hello".Length);
                    }
                    else
                    {
                        await next(httpContext);
                    }

                };
            });
```

2）显示异常页面

```c#
    if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
```



