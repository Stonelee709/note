#### 1、从 GIT HUB 下载项目时，导出到 VS 中依赖包显示黄色惊叹号。

开程序包管理控制台
vs->工具->NuGet包管理器->程序包管理控制台
Update-Package –reinstall



#### 2.取 appsettings.json 的值

在 startup 类中

```c#
 public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }
```

然后通过以下方式取得：

var jsonsetting = _configuration["CosmosDb:Account"];

#### 3ViewComponents

viewcomponent 是一个独立的组件，可方便复用。

#### 4安装前端库