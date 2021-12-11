# 视图

### 映射

- 默认映射：Views/Controller类名/方法名.cshtml
- 不用默认的情况：return View("~/Views/....");
- 可以对 Controller 方法单击右键添加视图

### 数据传递

1. ViewData//键值对，只能在一个视图用
2. ViewBag//动态类型，对 ViewData 的封闭，只能在一个视图用
3. Model：只能在一个视图用
4. TempData: 只能消费一次，可以在不同视图间共享
5. Cache: 应用程序级别、服务器保存，可以设置有效期
6. Session：会话级别，服务器保存

```c#
public IActionResult say()
        {
            ViewData["age"] = 100;
            ViewData["name"] = "lee";
//ViewBage 是对 ViewData的封装，会覆盖值
​        ViewBag.age = 100;
​        ViewBag.name = "lee";
    
            List<string> listdata = new List<string>();
            listdata.Add("one");
            listdata.Add("two");
            ViewBag.lsdata = listdata;
​        return View();
​    }

在 View cshtml 中：
    @ViewBag.age
    @ViewData["age"]
    @foreach(var item in ViewBag.lsdata) {
    <li>@item</li>
	}
```

Model 传值：

```C#
Person p= new Person();

return View(p);
//页面
@Model.UserName
//如果要对 Model 的类型进行限定而不是 dynamic，需要在页面的第一行写上，这样就会出现智能提示
@model lession3.Controllers.TestController.Person
 
@Html.TextBoxFor(m=>m.UserName)
```

同时，可以在 Model 类添加 DisplayName 特性给属性添加 Label 标签

```C#
public class Person
    {
        [DisplayName("用户名")]
        string UserName { get; set; }

}
d
页面取值
 @Html.LabelFor(m=>m.UserName)
```

TempData

```c#
TempData["name"]="zhangsan";
```



Session

先要启用 Session，在 Startup.cs 的 ConfigureServices 方法中添加

services.AddSession();

再在 Configure 方法中添加

app.UseSession();



            ISession session = HttpContext.Session;
            session.SetInt32("age", 32);
            
            Int? age = session.GetInt32("age");
            if(age.HasValue){
            
            }


Cache

```C#
public class TestController : Controller
{
    public readonly IMemoryCache _cache;

    public TestController(IMemoryCache cache)
    {
        _cache = cache;
    }
    public IActionResult Index()
    {
        _cache.Set<int>("age", 32);
        int age=_cache.Get<int>("age");
        ViewBag.age = age;
        return View();
    }

}
```


### 表单

传统html方式与 HtmlHelper

```html
@Html.TextBox("username")
@Html.TextBox("password")
```

@Html.ActionLink("跳转","link")//跳转



### 模板

多个页面共享一个头部和尾部

1）在 Shared 新建一个模板

2）添加以下内容

```
这是头
@RenderBody()
这是尾
```

3）新建视图时选择模板

4）如果视图页面与模板是多层夹心状况时

```html
//视图页面
@section section1{

<p>区域1的内容</p>

}
@section section2{

<p>区域2的内容</p>

}
//也可以包含非模板的其他视图页面
@Html.Partial("@/Views/Shared/MyPartial.cshtml")

//模板页面
@RenderSection("section1",true)
@RenderSection("section2",false)//false 表示非必需

//控制器当中也可以通过返回部分视图来进行局部刷新
return PartialView("@/Views/Shared/MyPartial.cshtml");
```

5) 如果所有页面都需要共用一个模板页面，那么可以在_ViewStart.cshtml目录里面写

6) _ViewImports.cshtml 用于引入全局命名空间



### 视图组件

我们可以定义一个视图组件，然后可以通过三种方式来调用这个视图组件

1)新建一个视图组件

```c#
//继承ViewComponent并通过 Invoke 调用
public class Sample:ViewComponent
    {
        public IViewComponentResult Invoke() {

            return Content("Hello Component");
        }
    }

//带参数并返回 View 视图
//视图组件对应的默认 View 页面是 Views/Shared/Components/视图组件名称/Default.cshtml
public class Sample:ViewComponent
    {
        public IViewComponentResult Invoke(string name) {

            return View();
        }
    }
```

####  在 Controller 中调用

```c#
public IActionResult Index()
        {
            return ViewComponent("Sample");
        }


//带参数

 public IActionResult Index()
        {
            return ViewComponent("Sample", new { name="Zhangsan"});
        }
```

#### 在 View 中调用

```
@await Component.InvokeAsync("Sample")

@await Component.InvokeAsync("Sample"new { name="Zhangsan"})
```

#### 通过 TagHelper 调用

先在_ViewImport.cshtml 中添加

@addTagHelper *, Sample

然后在 View 页面中

```html
<vc:Sample></vc:Sample>
<vc:Sample name="zhangsan"></vc:Sample>
```



