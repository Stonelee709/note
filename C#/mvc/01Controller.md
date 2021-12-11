# 控制器

### 如何成为控制器：

1）以 Controller结尾命名

2）或者在类上添加 [Controller]，并类继承: Controller

3)如果某些类不是控制器但以Controller结尾，可以在类上加[NonController]



### 路由的默认规则：

域名/{控制器}/{Action}/id

在 startup.cs 中的 Configure 方法中有一段路由配置的信息

```
 app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
```

也可以直接在Controller的类和方法添加以下路由配置

[Route("admin/Test")]// 如果是[Route("/index")] 以/开头就是绝对路径

```C#
[HttpPut("{id}/state/{state}")]
        public JsonResult modifyState(int id, bool state)
        {
            var response = Database.getUserList(1, 2);
            JsonResult result = new JsonResult(response);
            return result;
        }
```



#### Area

1）当分模块开发时，例如 Admin 和 UserCenter，可以按 Areas 目录-> 模块目录 -> 每个模块目录下面分别建 Controllers 目录和 Views 目录

2）在控制器类添加[Area("admin")] 特性

3）在 startup.cs 的 configure 中添加以下路由配置：



```c#
endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
```

如果不想用 Areas 作为根目录名，那么可以通过在startup.cs 的 ConfigurationService 方法中添加以下内容，更改名称为 Custom：

```c#
//{0} 表示视图名称 {1}控制器名称 {2} Area 名称
services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/Custom/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Custom/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
```



### 接收数据的形式：

1. QueryString: 地址栏输入

2. Form

3. Cookie：保存在客户端

4. Session：保存在服务器端

5. Header

6. 路由数据

   

### 接收用户请求



```C#
//通过Request和Session来获取
string value= Request.Query["key1"];//接受QueryString
string value1 = Request.Form["key2"];
string value2 = Request.Cookies["key3"];
string value3 = Request.Headers["key4"];
string value4=HttpContext.Session.GetString("key5");//session
int? age = HttpContext.Session.GetInt32("age");//返回的是一个可空的整数

//同时可能通过方法参数来获取，Key的名称必须与参数一样，参数也可以是一个 Plain Class
//并非所有的数据都可以绑定，例如 Header 的数据就不行，需要添加[FromHeader]
public IActionResult Index(string name, Person person) {
    
}
//如果同一个 Key 在多个数据源出现，例如同时在 Query 和 Form 时，可通过特性来指定
 public IActionResult Index([FromQuery]string name,[FromRoute] int id){}
```



### 返回结果

return Content("Hello");//Content 方法是由基类 Controller 提供

形式有：

- View视图
- Json数据
- 跳转请求
- 文件数据





IActionResult 接口有以下实现

| 实现           | 封装方法      |
| -------------- | ------------- |
| JsonResult     | Json(Object)  |
| RedirectResult | Redirect(Url) |
| FileResult     | File()        |
| ViewResult     | View()        |
| ContentResult  | Content()     |

```c#
 public IActionResult say()
        {
            JsonResult result = new JsonResult(new { username= "zhangsan"});
            return result;
     //或者直接通过封闭方法
     		return Json(new { username= "zhangsan"});
        }
```

如果涉及到 IO（网络读取、数据库、文件读写），建议使用 Task<IActionResult> 作为返回类型而不是IActionResult