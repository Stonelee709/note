### 布局容器

1. StackPanel

2. WrapPanel

3. DockPanel

4. Grid

5. Canvas

   ```XML
   		<!--定义两列，第二列是第一列的 4 倍 -->
   		<Grid.ColumnDefinitions>
               <ColumnDefinition Width="100"/>
               <ColumnDefinition Width="4*"/>
           </Grid.ColumnDefinitions>
           <Border Background="Red" Grid.Column="0">
               <Grid Name="grid0" MouseLeftButtonDown="grid1_MouseLeftButtonDown" MouseLeftButtonUp="grid1_MouseLeftButtonUp" MouseMove="grid1_MouseMove" Loaded="grid1_Loaded" Margin="-32,0,0,-1" />
           </Border>
           <Border Background="Green"  Grid.Column="2">
               <Grid Name="grid1" MouseLeftButtonDown="grid1_MouseLeftButtonDown" MouseLeftButtonUp="grid1_MouseLeftButtonUp" MouseMove="grid1_MouseMove" Loaded="grid1_Loaded" Margin="-32,0,0,-1" />
           </Border>	
   ```

   

### 样式

定义全局样式

```xml
    <Window.Resources>
        <Style x:Key="basestyle" TargetType="Button">
            <Setter Property="FontStyle" Value="Italic"></Setter>

        </Style>
        <Style x:Key="defaultstyle" TargetType="Button" BasedOn="{StaticResource basestyle}">
            <Setter Property="FontSize" Value="20"></Setter>
            <Setter Property="Height" Value="40"></Setter>
            <Setter Property="Width" Value="100"></Setter>
        </Style>
    </Window.Resources>

 <Button Style="{StaticResource defaultstyle}" Content="hello"/>
```



### 触发器

#### 属性触发器

```xml
<Style x:Key="defaultstyle" TargetType="Button" BasedOn="{StaticResource basestyle}">
            <Setter Property="FontSize" Value="20"></Setter>
            <Setter Property="Height" Value="40"></Setter>
            <Setter Property="Width" Value="100"></Setter>
            <!--添加触发器，鼠标放上去时，字体发生改变-->
            <Style.Triggers>
                <Trigger Property="IsMouseOver" Value="True">
                    <Setter Property="Foreground" Value="Red"></Setter>
                </Trigger>
            </Style.Triggers>
        </Style>
```

#### 事件触发器

```XML
<Window.Resources>
        <Style x:Key="basestyle" TargetType="Button">
            <Setter Property="FontStyle" Value="Italic"></Setter>

        </Style>
        <Style x:Key="defaultstyle" TargetType="Button" BasedOn="{StaticResource basestyle}">
            <Setter Property="FontSize" Value="20"></Setter>
            <Setter Property="Height" Value="40"></Setter>
            <Setter Property="Width" Value="100"></Setter>
            
            <Style.Triggers>
                <EventTrigger RoutedEvent="Mouse.MouseEnter">
                    <EventTrigger.Actions>
                        <BeginStoryboard>
                            <Storyboard>
                                <DoubleAnimation Duration="0:0:1" 
                                                 Storyboard.TargetProperty="FontSize" To="5">
                                </DoubleAnimation>
                            </Storyboard>
                        </BeginStoryboard>
                    </EventTrigger.Actions>
                </EventTrigger>
            </Style.Triggers>
        </Style>
    </Window.Resources>
```



### 命名空间

xmlns 相当于 C# 中的 Using，将类引入





&#x0a ; 代表回车