# AutoFac

AutoFac 是一个 IOC 框架，与Microsoft.Extensions.DependencyInjection一样。

```C#
interface IAService
    {
        void call();
    }
    interface IBService
    {
        void call();
    }
    class AService : IAService
    {
        public void call()
        {
            Console.WriteLine("A Service is calling");
        }
    }

    class BService : IBService
    {
        private IAService _IAService;
        public BService(IAService IAService)
        {
            _IAService = IAService;
        }
        public void call()
        {
            _IAService.call();
            Console.WriteLine("Bserivce is calling after A");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var containerbuilder = new ContainerBuilder();
            containerbuilder.RegisterType<AService>().As<IAService>();
            containerbuilder.RegisterType<BService>().As<IBService>();
            var container = containerbuilder.Build();
            using (var scope=container.BeginLifetimeScope())
            {
                var iBservice = scope.Resolve<IBService>();
                iBservice.call();
            }
            Console.ReadLine();
        }
    }
```

