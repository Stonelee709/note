**获取元素**：

```javascript
 var ele = document.getElementById('id');
        var tags = document.getElementsByTagName('li');
        for (let index = 0; index < tags.length; index++) {
            console.log(tags[index]);

        }
		var classele = document.getElementsByClassName("container");
        var query = document.querySelector(".box");//获取第一个
        var query = document.querySelector("#nav");//获取第一个
		var querylist = document.querySelectorAll(".box");
		var body = document.body;
        var html = document.documentElement;
```

**通过节点层级关系查找**

节点nodetype, nodename, nodevalue

元素节点 nodetype 为 1; 属性节点 nodetype 为 2; 文本节点 nodetype 为3

```javascript
        var first = document.querySelector(".first");
        var ul = document.querySelector("ul");
        console.log(first.parentNode);
        console.log(ul.childNodes);//会获取文本节点和元素节点
        //如果只要获取元素节点
        console.log(ul.children);
        //获取的第一个子节点，可能是文本节点
        console.log(ul.firstChild);
        //获取的第一个子元素
        console.log(ul.firstElementChild);
        console.log(ul.children[0]);
        console.log(ul.children[ul.children.length - 1]);
        console.log(ul.nextSibling);//包含文本节点
        console.log(ul.nextElementSibling);//包含文本节点
```

**创建节点**

```javascript
var li = document.createElement('li');
        var li2 = document.createElement('li');
        var first = document.querySelector(".first");
        first.appendChild(li);
        first.insertBefore(li2, first.children[0]);
```

**删除节点**

```javascript
first.removeChild(ul.children[0]);
first.removeChild(this.parentNode);
```

阻止链接跳转需要添加 href="javascript:void(0)";

**复制节点**

node.cloneNode(true); true 表示深拷贝

**事件基础**：

```javascript
    <div class="test"></div>
    <img src="../img/bingwall.jpg" alt="">
    <button class="btn">click</button>
    <script>
        var ele = document.querySelector(".test");
        var btn = document.querySelector(".btn");
        var img = document.querySelector("img");
        btn.onclick = function () {
            ele.innerHTML = "<p>hello</p>";
            img.src = "../img/bgc.png";
            input.value = "单击";
            btn.disabled = true;
            this.disabled = true;//this 指向函数调用者
            input.type="password";
            ele.style.backgroundcolor='purple';
            ele.className = "newcss";
            
        }
    </script>
```

innerText 不识别 html 标签，不保留空行。innerHTML 识别并保留。

表单 input 不能通过 innerText 来修改，需要通过 value.

**获取属性值的两种方法：**

element.属性

element.getAttribute('属性')//获取自定义属性

element.dataset.index // dataset 是定义了所有自定义属性的集合。获取 data 开头的属性，即 data-index

**设置属性值**：H5 规定所有自定义属性都以 data 开头

element.属性=‘属性’

element.setAttribute('属性', 值）

**删除属性**

element.removeAttribute('属性')



**创建元素三种方式**

document.write() 整个页面全部重绘，只剩下（）中的内容了。

innerHTML: 效率更加，但要用数组形式(push 进数组后再 Join)，不要拼接字符串

document.CreateElement()：效率低些



#### 事件高级

##### 注册事件：

1传统方式（只能注册一个） onClick=function...

2事件监听注册方式。addEventListener(type, listener,[usecapture])，可以添加多个监听器。（推荐）

```javascript
 var btn = document.querySelector("button");
        btn.addEventListener("click", function () {
            alert("hello");
        });
 	 btn.addEventListener("click", function () {
            alert("world");
        });
```

##### 删除事件

1 传统方式 eventTarget.onclick=null;

2 removeEventListener

```javascript
 var btn = document.querySelector("button");
        btn.addEventListener("click", fn);
        function fn() {
            alert("hello");
            btn.removeEventListener("click", fn);
        }
```

##### DOM事件流

当你点击按钮时，先是 Document 接收到了点击事件，先处理，然后再往下传递是 html ->body->div->button，这是捕获。反向就是冒泡。

onclick 只能得到冒泡。

addEventListener 第三个参数为 true 就是捕获, false 是冒泡。实际开发很少用捕获。

有些事件是没有冒泡的：onblur, onfocus, onmouseenter, onmouseleave



##### 事件对象

div.onclick=function(event){} event 就是事件对象，是事件一系统事件的相关数据的集合。鼠标事件对象、键盘事件对象

```javascript
 var btn = document.querySelector("button");
        btn.addEventListener("click", fn);
        function fn(event) {
            console.log(event);
        }
```

event.target 和 this 的区别: event.target 是触发事件的对象, this 是绑定事件的对象。例如 ul 下面的 li, ul 绑定了事件 this 就是  ul,但是通过单击 li 触发的，触发的对象是 li

event.type: 类型

event.preventDefault: 阻止默认形为 也可以直接使用 return false;

```javascript
<div class="box">

        <a href="www.baidu.com">click</a>
    </div>
    <script>
        var btn = document.querySelector("a");
        btn.addEventListener("click", function (event) {
            event.preventDefault();//推荐
            return false;// return 后面代码不执行，同时只能用于传统注册事件方式
        });

    </script>
```

event.stopPropogation() 阻止冒泡

##### 事件委托

不要给每个子节点单独设置事件侦听，而在其父节点上设置，然后通过冒泡原理通过 e.target 来找到当前的子元素。这样可以操作一次 DOM，提高程序性能。

```javascript
<ul>
        <li>1</li>
        <li>2</li>
        <li>3</li>
        <li>5</li>
        <li>5</li>
    </ul>
    <script>
        var ul = document.querySelector("ul");
        ul.addEventListener("click", function (event) {
            event.target.style.backgroundColor = 'pink';
        });

    </script>
```

##### 常用鼠标事件

1 禁用鼠标右键菜单

```javascript
var ul = document.querySelector("ul");
        ul.addEventListener("contextmenu", function (event) {
            event.preventDefault();
        });
```

2 禁用选中文字

```javascript
var ul = document.querySelector("ul");
        ul.addEventListener("selectstart", function (event) {
            event.preventDefault();
        });
```

3 MouseEvent

```javascript
document.addEventListener("click", function (e) {
           console.log(e.clientX);//可视区域
            console.log(e.clientY);

            console.log(e.pageX);//页面坐标（常用）
            console.log(e.pageY);
                console.log(e.screenX);//屏幕
            console.log(e.screenY);
        })
```

4键盘事件 keyup, keydown, keypress(keypress不能识别功能键如 ctrl, shift)，先执行 down -> press ->keyup

keydown, keypress 字还没有显示就会触发。

```javascript
document.addEventListener("keyup", function (e) {

            console.log(e.keyCode);//keyup, keydown不区分打小写,keypress区分
        })
```

window.load 事件与 window.pageshow 事件：pageshow 解决了 firefox 缓存页面不刷新的问题

mouseover 事件（经过自身与子元素都会触发）与 mouseenter 事件（自身盒子才会触发，因为它不会冒泡）：

