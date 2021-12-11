### Bean 创建的生命周期

class->实例化 -> 对象->属性填充（此类中要用到其他类的对象初始化）-->初始化 afterPropertiesSet() (如果实现接口InitializingBean)===>AOP===>代理对象==> Bean对象

代理对象的创建：

```JAVA
public class UserServiceProxy extends UserService {

​			override test() {

​	}

}
```

