```c#
		Person person;
        public MainWindow()
        {
            InitializeComponent();
            person = new Person();
            DataContext = person;
            person.Name = "lee";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            person.Name = "stone";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UW uw = new UW(person);
            uw.Show();
        }
```

```xaml
		<StackPanel>
        <TextBox Text="{Binding Path=Name}"></TextBox>
        <Button Click="Button_Click" Width="20" Height="20">click</Button>
        <Button Click="Button_Click_1" Width="20" Height="20">show</Button>
            
        </StackPanel>
```

