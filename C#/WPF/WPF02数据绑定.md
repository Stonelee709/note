# 数据绑定

### （一） 通过 INotifyPropertyChanged 接口

原理：在属性有set 和 get 方法，在set方法中添加通知功能，一旦调用 set 方法将调用通知功能，修改绑定数据

#### 1 ViewModel 类

```c#
public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private String _name = "张三";
        private int _age = 24;
        private String _hobby = "篮球";

        public String Name
        {
            set
            {
                _name = value;
                if (PropertyChanged != null)//有改变
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));//对Name进行监听
                }
            }
            get
            {
                return _name;
            }
        }

        public int Age
        {
            set
            {
                _age = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Age"));//对Age进行监听
                }
            }
            get
            {
                return _age;
            }
        }
        public String Hobby//没有对Hobby进行监听
        {
            get { return _hobby; }
            set { _hobby = value; }
        }

    }
```



#### 2在主窗口类中添加数据源：

```c#
 Person person;
        public MainWindow()
        {
            InitializeComponent();
            person = new Person();
            DataContext = person;
            person.Name = "lee";

        }
```



#### 3窗体 XAML 中绑定

```xaml
 <TextBox Text="{Binding Path=Name}"></TextBox>
```

#### 4 其他窗口要修改时将此对象传过去

```C#
 UW uw = new UW(person);
 uw.Show();
```

