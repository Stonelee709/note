 [SOLID](https://zh.wikipedia.org/wiki/SOLID_(面向对象设计)) 指 Single responsibility， Open-closed， Liskov substitution， Interface segregation and Dependency inversion， 即单一功能、开闭原则、里氏替换、接口隔离以及依赖反转)。



MVVM 理解：https://blog.csdn.net/changyang208/article/details/71545457

View UI 层和 Model 并不能一一对应，这个时候就需要用 ViewModel 层来转换，将 UI 层与 ViewModel 对应起来。同时UI的中的事件，也要通过 ViewModel 中的方法来实现，这个时候就需要使用 Command，Command 中有一个私有 ViewModel 对象：

```C#
public class ButtonCommand : ICommand
    {
        private CustomerViewModel obj; // Point 1
        public ButtonCommand(CustomerViewModel _obj) // Point 2
        {
            obj = _obj;
        }
        public bool CanExecute(object parameter)
        {
            return true; // Point 3
        }
        public void Execute(object parameter)
        {
            obj.Calculate(); // Point 4
        }
    }
```

 ```c#
public class CustomerViewModel 
{
     private Customer obj = new Customer();
 
        public string TxtCustomerName
        {
            get { return obj.CustomerName; }
            set { obj.CustomerName = value; }
        }        
 
        public string TxtAmount
        {
            get { return Convert.ToString(obj.Amount) ; }
            set { obj.Amount = Convert.ToDouble(value); }
        }
 
 
        public string LblAmountColor
        {
            get 
            {
                if (obj.Amount > 2000)
                {
                    return "Blue";
                }
                else if (obj.Amount > 1500)
                {
                    return "Red";
                }
                return "Yellow";
            }
        }
 
        public bool IsMarried
        {
            get
            {
                if (obj.Married == "Married")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
 
        }

        public void Calculate()
        {
            obj.CalculateTax();
        }
}
 ```



三者之间的关系：View对应一个ViewModel，ViewModel可以聚合N个Model，ViewModel可以对应多个View。这个很类似 VUE 的数据绑定，一个页面对应了 data, methods，Data 对应 Model, Methods 对应 Command。Command 要操作数据需要在自己的类中添加 ViewModel 对象做为属性，VIEWMODEL 与 UI 绑定。

===============================

对 ButtonCommand类进行通用性改造

```c#
 public class ButtonCommand : ICommand
    {
        private Action WhattoExecute;
        private Func<bool> WhentoExecute;
        public ButtonCommand(Action What, Func<bool> When) // Point 1
        {
            WhattoExecute = What;
            WhentoExecute = When;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return WhentoExecute(); // Point 2
        }
        public void Execute(object parameter)
        {
            WhattoExecute(); // Point 3
        }
    }
```

 Model AND ViewModel

```c#
public class Customer
    {
        public string Name { get; set; } = "lee";
        public int Age { get; set; } = 39;
        public float Amount { get; set; } = 0f;
    }

public class CustomerViewModel : INotifyPropertyChanged
    {
        Customer customer = new Customer();
        public event PropertyChangedEventHandler PropertyChanged;
        public string NameText {
            get { return customer.Name; }

            set { customer.Name = value; PropertyChanged(this, new PropertyChangedEventArgs("NameText")); } 
        }
        public int AgeText
        {
            get { return customer.Age; }

            set { customer.Age = value; PropertyChanged(this, new PropertyChangedEventArgs("AgeText")); }
        }

        public float DiscountText
        {
            get {
                if (customer.Age > 69)
                {
                    return 0.6f;
                }
                else
                {
                    return 0.8f;
                }
            }

           
        }

        public void calculateTotal()
        {
            customer.Name = "ironman";
            customer.Age = 70;
            customer.Amount= 100 * this.DiscountText;
            if (PropertyChanged != null) // Point 2
            {
                PropertyChanged(this, new PropertyChangedEventArgs("NameText"));
                PropertyChanged(this, new PropertyChangedEventArgs("AgeText"));
               
                // Point 3
            }
        }
		//命令
        private ButtonCommand buttonCommand;
        public CustomerViewModel()
        {
            //通过委托传递
            buttonCommand = new ButtonCommand(this.calculateTotal,this.valid);
        }

        public bool valid()
        {
            return true;
        }
        public ICommand btnClick
        {
            get
            {
                return buttonCommand;
            }
        }
    }
```

=======

PRISM 框架实现：

在 ViewModel层进行如下修改：

```c#
private DelegateCommand  objCommand;
public CustomerViewModel()
        {
objCommand = new DelegateCommand(obj.CalculateTax,
                                        obj.IsValid);
        }
```

