# BOM

核心是 window，包含 documnt, location, navigation, screen, history。

window.onload

window.addEventListner("load",func)

window.addEventListner("DomcontentLoaded",func)//这个更快，而且避免图片过多，要等很久

onsize:可用于做响应式布局

#### 计时器

setTimeout(func, 2000) 延时2秒执行

setInterval 会一直执行

定时器的 this 指向 window。

#### 同步异步

setTimeout 是同步任务，但里面的 func 是异步任务。

JS 执行机制：先执行同步任务，把异步任务放入任务队列，再在查看队列中的异步任务，如果有就执行。主线程会不断检查异步任务队列，这叫事件循环。

#### Location 对象

location:href='www.baidu.com'  和 location.assign()跳转页面

location.search 返回的是 ?uname=lee

location.reload() 刷新，加了 true 参数是强制刷新

#### navigator 对象

navigator.useragent 可以得到浏览器信息，从而实现手机访问手机页面。

navigator.useragent.match(/(phone|pad|iPhone|iPod|ios|iPad|Android|Mobile|))

#### history 对象

back()后退

forward() 前进