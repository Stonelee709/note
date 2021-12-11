以开发婴儿车为例，婴儿车为主体程序，它可以外接各种动物叫声的插件，供第三方开发。

主体程序定义一个类库叫Aminal.SDK, 类库中有 IAnimal 这个接口，下面有一个 Voice 方法。其它厂商根据这个接口，来实现接口并生成 SHEEP、CAT、DOG 等类，这些类生成类库放进 U盘，可插入主体程序。

主体程序会扫描 U 盘下面的所有 DLL 类库，找到所有实现了 IAnimal 接口的类将放到 List<Type> 中，通过反射创建实例，然后调用。

```c#
 class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Environment.CurrentDirectory + "\\Animal");
            var files = new DirectoryInfo(path).GetFiles("*.dll");
            var animalTypes = new List<Type>();
            foreach (var file in files)
            {
                var assemble = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                var types = assemble.GetTypes();
                foreach (var t in types)
                {
                    if (t.GetInterfaces().Contains(typeof(IAnimal)) )
                    {
                        var isUnfinish = t.GetCustomAttributes(false).Any(a => a.GetType() == typeof(UnfinishAttribute));
                        if (isUnfinish) continue;
                        animalTypes.Add(t);
                    }
                }
            }

            while (true)
            {
                for (int i = 0; i < animalTypes.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {animalTypes[i].Name}");
                }
                Console.WriteLine("=======================");
                Console.WriteLine("Please input the animal index:");
                int index = int.Parse(Console.ReadLine());
                if (index < 0 || index > animalTypes.Count)
                {
                    Console.WriteLine("Wrong Input");
                    continue;
                }
                Console.WriteLine("Please input the times:");
                int times = int.Parse(Console.ReadLine());
                var animal = animalTypes[index - 1];
                var o = Activator.CreateInstance(animal);
                var a = o as IAnimal;
                a.Voice(times);
            }
        }
    }
```

