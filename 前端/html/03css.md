background-imagefont-weight:400 表示 Normal 不加粗,700 是bold

font 复合属性

font: font-style font-weight font-size/line-height font-family

去掉链接的下划线: text-decoration:none

首字母缩时: text-indent:2em;  em 表示两个文字大小

<link rel="stylesheet" href=""> 引入外部样式

======================

emmet语法快捷键:

.nav=> div id="nav"

p.nav= p id="nav"

.demo$*5 会生成顺序

```c#
<div class="demo1"></div>
<div class="demo2"></div>
<div class="demo3"></div>
<div class="demo4"></div>
<div class="demo5"></div>
```

div{Hello World} =><div>Hello World</div>

tac =>text-align: center;

ti=>text-indent

w100=> width:100px

h200=> height:100px

SHIFT+ALT+F 格式化代码

===========================

复合选择器：

ol li 后代选择器 ol 下面子代或孙代的 li

ol>li 子代选择器，只选子代

div, p 并行选择器，并且和。

伪类选择器：

链接伪类选择器:  a:link =>未访问的链接  a:visited =>被访问的链接 a:hover=>鼠标悬念 a:active=>鼠标按下未弹起。需要按这个顺序LVHA来写 CSS

焦点选择器：input:focus

#### 块元素

- 一行只有一个
- 可以设置高宽
- 默认宽度是容器的 100%



#### 行内元素特点：

1. 高宽设置无效

2. 默认宽度是本身内容的宽度

3. 行内元素只能容纳文本或其他行内元素

4. a 可以放块级元素，但 a 转换成块级元素最安全

   

#### 行内块元素

- input, image td
- 和相邻元素在一行，但之间会有空白间隙
- 本身宽度是内容元素
- 可以设置宽度与高度





行内元素转成块元素: display: block => a 元素转成块元素，就可以独占一行，可以设置高宽了

块元素转成行内元素: display: inline => div 转成行内元素，就可以不占一行，不可以设置高宽了

行内元素转成行内块元素: display:inline-block => span 转成行内块元素，一行可多个，也可以设置高宽

=============

单行文字垂直居中小技巧：文字行高等于盒子行高

```html
侧边导航栏：    
<style>
        body {
            text-align: center;
        }

        a {
            display: block;
            background-color: gray;
            width: 120px;
            height: 40px;
            text-decoration: none;
            color: white;
            font-size: 12px;
            text-align: left;
            text-indent: 2em;
            /* 垂直居中 */
            line-height: 40px;
        }

        a:hover {
            background-color: orange;
        }
    </style>
</head>

<body>
    <div>
        <a href="#">手机电话卡</a>
        <a href="#">手机电话卡</a>
        <a href="#">手机电话卡</a>
        <a href="#">手机电话卡</a>
        <a href="#">手机电话卡</a>
        <a href="#">手机电话卡</a>
        <a href="#">手机电话卡</a>
    </div>
</body>
```

 

background-image 配合 background-position: x y;方便用于控制图片位置

background-position: center top/background-position: 20px top

**background  :**[ **background-color**  ](c_backgroundcolor.html)||[ **background-image**  ](c_backgroundimage.html)||[ **background-repeat**  ](c_backgroundrepeat.html)||[ **background-attachment**  ](c_backgroundattachment.html)||[ **background-position**](c_backgroundposition.html) 



**background-attachment :** **scroll** | **fixed**  

参数： 

**scroll :**  背景图像是随对象内容滚动
**fixed :** 　背景图像固定 

background-color: rgb**a**(0,0,0,.3) 背景透明 a 表示 alpha

border-bottom: 4px solid #FF7486

box-shadow: 0 2px 4px 0 rgb(0 0 0 / 8%);

选择器权重：!important>行内样式>id>class>tag>继承   =>background-color: red !important;



================

布局：盒子、浮动、定位

盒子边框: border-top: 1px solid red;

board-collapse: collapse; 合并相临的边框

**让 Div 居中显示: 设置宽度，然后margin: 0 auto;**

**让行内元素居中显示，在其父元素加  text-align: center**

如果子元素要设置 margin-top，那么会出现**块元素塌陷**，解决方案: 1 为父元素设置 board-top 2为父元素设置 padding 3 为父元素添加 overflow:hidden。注：浮动的例子没有塌陷问题。

**如果没有指定 width 或 height 则 padding 不会改变盒子大小**

清除内外边距

```html
* {
            padding: 0px;
            margin: 0px;
        }

```

去掉 li 的小圆点 list-style:none

圆角边框: board-radius:50%

盒子阴影: box-shadow: 5px 5px rgba(0, 0, 0, .6);

============================

布局三种方式：标准流、浮动、定位

#### 浮动特性：

- 脱离标准流控制，只影响后面的标准流，不影响前面的标准流
- 不再保留原来的位置

float: left;

如果使用 inline-block 会有一个不确定的间隙。

任何元素都可以浮动，浮动之后都具有行内块相似特性元素。

**清除浮动：如果父元素不确定有多少子元素，就不方便给高度，但如果子元素都是浮动，那么父元素高度为0，这会影响父元素兄弟元素的布局**。解决办法：

1）额外标签法。在最后一个子元素后面增加一个标签 <div style="clear:both"/>，必须是块级元素。

2）父元素添加 overflow。overflow:hidden;

3) 这父元素添加:after伪元素。

```css
 .clearfix:after {
            content: "";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }

        .clearfix {
            *zoom: 1;
        }
```

3) 双伪元素清除浮动。

```css
.clearfix:before, 
.clearfix:after {
            content: "";
            display: table;
           
        }
.clearfix:after {
    		clear:both;
}
        .clearfix {
            *zoom: 1;
        }
```



=========================

PNG 可以透明背景

=======================

css 属性书写顺序

1布局定位属性：display/poistion/float/clear/visibility/overflow

2自身属性:width/height/margin/padding/board/background

3文本属性：color/font/text-decoration/text-align/verticial-align/white-space/break-word

4其它属性: content/cursor/border-radius/box-shadow/text-shadow/background:linear-gradient...

==========================

定位:

定位=定位模式+边偏移

poistion: 

static(少用) 

relative(以自己为原点做偏移，虽然走了，但之前的位置保留) 

absolute: 1)没有父元素或者父元素没有定位，根据浏览器；2)如果父/爷元素有定位，就以最近有定位的父/爷元素为坐标偏移 3) 不占有原先的位置

**子绝父相**

fixed：固定在浏览器**可视区域**的固定位置。与父元素没关系，不随滚动条滚动。不占有原来的位置。

如何固定在版心右侧：left 50%+ margin 50%版心宽度

sticky:相对定位和固定定位的混合。以浏览器可视区域为参照。占有原先位置。必须添加 top, bottom, left, right 其中一个属性 

叠放顺序： z-index: 数值越大，盒子越靠上（只有定位的盒子有）

绝对定位的盒子不能通过 margin:0 auto 来水平居中。 left:50%, margin：-盒子宽度/2

行内元素加了绝对或固定定位，可以设置高宽。块级元素加了绝对或固定定位，不给宽度或高度就是内容大小 



float 不会压住内容（可做文字环绕效果），position 会压住内容

=========

display: none 元素隐藏，位置也无。

visibiliy: hidden元素隐藏，位置还在

overflow: auto 超出才会显示滚动条

======

hover 操作子元素CSS：

.father:hover .son{}

==============

精灵图：

一张大图有多个小图，减少调用数量

background-position: 负值 负值

===========

字体图标

https://icomoon.io/app/#/select

引入外部字体

```css
 font-family: 'HansHandItalic';
    src: url('fonts/hanshand-webfont.eot');
    src: url('fonts/hanshand-webfont.eot?#iefix') format('embedded-opentype'),
         url('s/hanshand-webfont.woff') format('woff'),
         url('fonts/hanshand-webfont.ttf') format('truetype'),
         url('fonts/hanshand-webfont.svg#webfont34M5alKg') format('svg');
    font-weight: normal;
    font-style: normal;
	font-display: block;
```

=====

CSS 画三角形

先画一个高宽为 0 的盒子，Border: 50px Solid transparent; 然后指定其中一个 BOARD 有颜色 board-left-corlor: pink;

===================

textarea 取消拖动：resize:none;

取消输入框蓝色边框：outline:none

图片与文本对齐:  vertical-align: middle/baseline/bottomline，默认是基线，这会导致图片与 boarder 有一个空隙，可改成 mddile/top/bottom

超过文本显示成省略号：white-space:no-wrap; over-flow:hidden; text-overflow:ellipsis;



=========================

两个盒子boarder1+1=2时，可以通过 margin-left:-1px; 来合并

CSS 初始化

```css
<style>
        * {
            margin: 0;
            padding: 0
        }

        em,
        i {
            font-style: normal
        }

        li {
            list-style: none
        }

        img {
            border: 0;
            vertical-align: middle
        }

        button {
            cursor: pointer
        }

        a {
            color: #666;
            text-decoration: none
        }

        a:hover {
            color: #c81623
        }

        button,
        input {
            font-family: Microsoft YaHei, Heiti SC, tahoma, arial, Hiragino Sans GB, "\5B8B\4F53", sans-serif
        }

        body {
            -webkit-font-smoothing: antialiased;
            background-color: #fff;
            font: 12px/1.5 Microsoft YaHei, Heiti SC, tahoma, arial, Hiragino Sans GB, "\5B8B\4F53", sans-serif;
            color: #666
        }

        .hide,
        .none {
            display: none
        }

        .clearfix:after {
            visibility: hidden;
            clear: both;
            display: block;
            content: ".";
            height: 0
        }

        .clearfix {
            *zoom: 1
        }
```

======================

属性选择器：

```css
div[class=boxfather] {
            background-color: pink;
        }

.boxfather ul li:nth-child(2n) {

   color: pink;

    }
p:nth-of-type(2)
{
background:#ff0000;
}
```

伪元素选择器：必须有content:'text'

div::before 在 div 内部开头添加一个元素; 

div::after 在 div 内部最后添加一个元素;

div:hover::before{}

伪元素清除浮动:

```css
div::after{

	content:'';

	display:block;

	height:0;

 	clear:both;

	visibility: hidden;

}
```



=============

**盒子大小默认是内容+board+padding，如果设置 box-sizing:border-box 则board, padding 不会影响大小。**

图片模糊处理： filter:blur(5px);

calc 计算: width:calc(100%-80px);

===================

过渡:

transition: 属性 花费时间(0.5s) 运动曲线(可省略) 何时开始(0s)

```css
		div {
            width: 100px;
            height: 100px;
            background-color: black;
            transition: width 0.5s;
        }

        div:hover {
            width: 200px;
            
        }
 div {
            width: 100px;
            height: 100px;
            background-color: black;
            transition: all 1s;
        }

        div:hover {
            width: 200px;
            height: 200px;
            background-color: pink;
        }
```

==================

正式项目开发：

base.css: JD 的清除CSS

common.css

网页标签添加图标：

```html
<link rel="shortcut icon" href="/favicon.ico" />
```

=========

2D 转换

transform: translate(100px,100px)移动位置，也可以使用 50%(盒子自身)。特点：不影响其它元素的位置；对行内元素无效。

transform: roate(45deg)。通过旋转可以画三角：只保留两条边，再旋转 45 度。

transform-origin: x y; 可以是 left, bottom

transform: scale(x,y); x,y 是数字没有单位就是位数。不影响其它盒子。

transform: translate()  scale(x,y) 可以联合使用，先位移，再旋转

===

动画：

```css
<style>
        @keyframes move {
            0% {
                transform: translateX(0px);
            }

            100% {
                transform: translateX(1000px);
            }
        }

       div {
            width: 100px;
            height: 100px;
            background-color: pink;
            animation-name: move;
            animation-duration: 2s;
            animation-iteration-count: infinite;
            /* 逆向播放 */
            animation-direction: alternate;
        }
 div:hover {
            animation-play-state: paused;
        }
    </style>
```

absolute 定位时要居中对齐: top:50%; left:50%; transform:translate(-50%,-50%);