jQuery 对象是DOM对象的包装，是一个伪数组

**Dom 对象与jQuery对象的相互转换：**

```javascript
		$('div')[0].style.backgroundColor = 'red';//jquery 转 dom
		$('div').get(0).style.backgroundColor = 'red';//jquery 转 dom
        
var div = document.querySelector("div");
        $(div);//dom 转 jquery
```

#### 选择器

```javascript
$('div').css("background", "pink");//将所有的 DIV 都改了 CSS，这个过程就是隐式迭代，遍历内部 DOM 元素的过程就叫隐匿迭代。
$('ul li').css("color","red");
$('ul li:first').css("color", "red");
$('ul li:eq(2)').css("color", "red");//第三个
$('ul li:even').css("color", "red");//奇数
$('ul li:odd').css("color", "red");//偶数
$(".son").parent();//最近一级父元素
$("nav").children("p");//亲儿子
$("nav").find("p");//所有子代
$(".first").siblings("li")//查找兄弟节点，不包括自己
$(".first").nextAll("li")//查找当前元素之后的所有同辈
$(".first").preAll("li")//查找当前元素之前的所有同辈
$(".div").hasClass("protected")//检查当前元素是否含有特定类
$("li").eq(2)//相当于$('ul li:eq(2)')


```



```javascript
JQUERY 的排他思想
$("button").click(function () {
    		//this表示当前自己
            $(this).css("background", "pink");
    //隐式迭代
            $(this).siblings("button").css("background", "");
    //或者使用链式编程
    $(this).css("background", "pink").siblings("button").css("background", "");
        });
//修改多个样式
$(this).css({ "color": "pink", "font-size": "20px" });
//添加类
$(".div").addClass("current")
$(".div").removeClass("current")
$(".div").toggleClass("current")//在有和无之间切换
```

https://jquery.cuishifeng.cn/    JQUERY API

show(),hide()

slideup(), slidedown(),slidetoggle()

hover() 里面放一个函数时，就是经过和退出都会执行，放两个时，一个是经过，一个是退出



```javascript
//下拉菜单
<style>
        * {
            margin: 0px;
            padding: 0px;
        }

        .menu>li {
            float: left;
            list-style: none;
            margin-right: 80px;
            width: 100px;
        }

        .menu li a {
            float: none;
            display: block;
        }

        .menu li ul {
            float: none;
            display: none;
            margin: 0px;
        }

        .menu li ul li {
            float: none;
            margin: 0px;
            list-style: none;

        }

        .test li {
            list-style: none;
        }
    </style>
    <script>
        $(
            function () {
                $(".menu li").hover(function () {
                    $(this).children("ul").stop().slideToggle();
                })
            }
        )
    </script>
</head>


<body>
    <div class="menu">
        <li>
            <a>File</a>
            <ul>
                <li>Open</li>
                <li>Edit</li>
                <li>Cut</li>
                <li>Copy</li>
                <li>Quit</li>
            </ul>
        </li>
        <li>
            <a>menu1</a>
            <ul>
                <li>1</li>
                <li>2</li>
                <li>3</li>
                <li>5</li>
                <li>5</li>
            </ul>
        </li>
        <li>
            <a>menu1</a>
            <ul>
                <li>1</li>
                <li>2</li>
                <li>3</li>
                <li>4</li>
                <li>5</li>
            </ul>
        </li>

    </div>
```

淡入淡出

fadeIn(1000)

fadeOut()

fadeTo(1000,0.5)

自定义动画

animate(属性, 速度, easing, fn)

```javascript
 $(
            function () {
                $("button").click(function () {
                    $(".menu").animate({
                        left: 500,
                        top: 300,
                        opacity: 0.4,
                        width: 200
                    }, 500)
                });

            }
        )
```

#### jquery 属性操作

```javascript
  $(
            function () {
				//固有属性
                console.log($('a').prop("href"));
                console.log($('input').prop("checked"));
                $('input').prop("checked",true);
                //自定义属性
                console.log($('input').attr("index"));
                //设置属性
                console.log($('input').attr("index", 4));
                //数据缓存 data()
                $("input").data("uname", "andy");
                console.log($("input").data("uname"));
            }
        )
```

$("div").html()

$("div").text()

$("input").val()

$("input[name=List-1radio1]").click(function () {
                var sex = $(this).val();
                alert(sex);
            });



#### 元素操作

遍历

```javascript
$(
            function () {

                $("div").each(function (index, docelement) {
                    $(docelement).css("color", "blue");
                })
            }
        )
$.each 可遍历元素、数组、对象
```

##### 创建元素

```javascript
$(
            function () {
                //内部添加，父子关系
                var p = $("<p>add</p>");
                $("div").append(p);
                $("div").prepend(p);
                //外部添加，兄弟关系
                $("div").before(p);
                $("div").after(p);
                //删除
                //$("div").remove();//自杀
                $("div").empty();//清空
                $("div").html("");//清空
            }
        )
```

##### 事件处理on绑定一个或者多个事件(推荐使用 on)

可以给动态创建的元素添加绑定

```javascript
$(
            function () {
                $('div').on(
                    {
                        mouseenter: function () {
                            $(this).css("background", "blue")
                        }
                    },
                    {
                        click: function () {
                            $(this).css("background", "red")
                        }
                    }
                )
                $('div').on(

                    {
                        click: function () {
                            $(this).css("background", "red")
                        }
                    }
                )
                 $("ul").on("click", "li", function () {
                    alert(11);
                })
            }
        )

```

解除事件 off

触发一次的事件用 one()

#### 自动触发

element.click()   element.trigger()     

element.triggerHandler()//不会触发元素的默认行为



#### 对象拷贝

$.extend(targetobj, obj)

浅拷贝简单值类型复制，内部对象复制地址

深拷贝简单值类型复制，对象复制值 $.extend(true,targetobj, obj)



Jquery 插件

https://www.jq22.com/

http://www.htmleaf.com/ （推荐）

懒加载

全屏滚动插件 fullpage.js

echarts.js: 五个步骤：1引入 js 2准备具备大小的窗口 3 初始化 echarts 实例对象 4 指定配置项和数据 5 将配置项设置级 echarts 实例对象

flexible.js  检测浏览器宽度，修改 html 大小

rem 配置 cssrem 插件

flex 页面布局

#### Jquery 扩展方法

```javascript
$.fn.highlight1 = function () {
    // this已绑定为当前jQuery对象:
    this.css('backgroundColor', '#fffceb').css('color', '#d85030');
    return this;
}
```



```javascript
$.fn.highlight = function (options) {
    // 合并默认值和用户设定值:
    var opts = $.extend({}, $.fn.highlight.defaults, options);
    this.css('backgroundColor', opts.backgroundColor).css('color', opts.color);
    return this;
}

// 设定默认值:
$.fn.highlight.defaults = {
    color: '#d85030',
    backgroundColor: '#fff8de'
}
```

