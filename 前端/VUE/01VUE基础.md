第一个 VUE 程序：

```php+HTML
<html>
    <head>
        <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
    </head>
    <title>HELLOE VUE</title>
<body>
    <div id="app" class="app">
        {{message}}<br/>
        {{school.name}}<br/>
        {{school.mobile}}<br/>
        {{ campus[0] }}<br/>
    </div>
</body>
<script>
    var app= new Vue(
        {
            // el 表示 element，挂载点，建议使用 id 选择器
            el:'#app',//不能挂载到 html 和 body 上
             data:{
                message:'HELLO VUE3',
                school:{
                    name:'nanchang University',
                    mobile:'15312240729'
                },
                campus:["Nanchange","Henan","Hubei"]
            }
        }
    )
</script>
</html>
```

注：VS CODE 可以安装 Live Server 插件时时刷新页面

### v-text：挂载普通文本

```html
<html>
    <head>
        <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
    </head>
    <body>
            <div id="app">
                //字符拼接
                <h2 v-text="message+' 你好'"></h2>
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                        message:'hello vue'
                    }
                    }
                )
            </script>
    </body>
</html>
```

### v-html 挂载html文本

```html
<html>
    <head>
        <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
    </head>
    <body>
            <div id="app">
                <h2 v-html="content+' 你好'"></h2>
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                        content:'<i>黑马</i>'
                    }
                    }
                )
            </script>
    </body>
</html>
```

### v-on 绑定事件

1. v-on:click //v-on 可以替换为@，即 @click

2. v-on:mouseenter

3. v-on:dbclick

   ```html
   <html>
       <head>
           <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
       </head>
       <body>
               <div id="app">
                   <h2 v-html="content+' 你好'" @click="changecontent"></h2>
               </div>
   
               <script>
                   var app= new Vue({
                       el:"#app",
                       data: {
                           content:'<i>黑马</i>'
                       },
                       methods: {
                           changecontent: function() {
                               //通过 this 来访问数据
                               this.content='白马';
                               alert('hello');
                           }
                       }
                       }
                   )
               </script>
       </body>
   </html>
   ```

   

### v-show 通过样式控制是否显示

```html
<html>
    <head>
        <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
    </head>
    <body>
            <div id="app">
                <h2 v-show="isdispaly">你好</h2>
                <h2 v-show="age>18">你好2</h2>
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                        isdispaly:false,
                        age:19
                    },                  
                    }
                )
            </script>
    </body>
</html>
```

### v-if 通过 Dom 来控制是否显示，性能消耗大些

```html
<html>
    <head>
        <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
    </head>
    <body>
            <div id="app">
                <h2 v-if="isdispaly">你好</h2>
                <h2 v-if="age>18">你好2</h2>
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                        isdispaly:true,
                        age:19
                    },                  
                    }
                )
            </script>
    </body>
</html>
```

v-on 补充

```html
<body>
            <div id="app">
                //限定为 enter 键
              <input @keyup.enter="sayhi"/>
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                       isActive:true,
                       arr:[1,2,3,4,5]
                    }, 
                    methods:{
                        sayhi:function(){
                            alert("hi");
                        }
                    }                 
                    }
                )
            </script>
```





### v-bind 设置元素属性

v-bind:src/class 也可以简写成 :src/class

```html
<html>
    <head>
        <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
        <style type="text/css">
        .active {
            color:red
        }
        </style>
    </head>
    <body>
            <div id="app">
                
                <h1 v-bind:class="isActive?'active':''">黑马</h1>
                <h1 :class="isActive?'active':''">黑马</h1>
                <h1 v-bind:class="{active:isActive}">黑马</h1>
                //绑定动态属性
                <div v-bind:[back]="color">1234</div>
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                       isActive:true,
                         back:"style",
        				color:"color:red"
                    },                  
                    }
                )
            </script>
    </body>
</html>
```

### v-for 循环

```html
<html>
    <head>
        <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
       </head>
    <body>
            <div id="app">
                <li v-for="item in arr">
                    {{item}} 
                </li>
                <li v-for="(item,index) in arr">
                    {{index}} 
                </li>
               
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                       isActive:true,
                       arr:[1,2,3,4,5]
                    },                  
                    }
                )
            </script>
    </body>
</html>
```

### v-mode 双向数据绑定

```html
<html>
    <head>
        <script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
       </head>
    <body>
            <div id="app">
              <input v-model="message"/>
              <h2>{{message}}</h2>
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                       message:'黑马'
                    },                       
                    }
                )
            </script>
    </body>
</html>
```



### 计算属性

```html
<body>
            <div id="app">
                <p>{{fullname}}</p>
            </div>
            
            </div>

            <script>
                var app= new Vue({
                    el:"#app",
                    data: {
                       firstname:'mike',
                       lastname:'Jodan'
                    },
                    //只会调用一次并将结果缓存
                    //这个简写方式，可以写 set 和 get，只有 get 就是只读属性
                    computed: {
                        fullname:function() {
                            return this.firstname+" "+ this.lastname;
                        }
                    }
                    })
                
            </script>
    </body>
```



var 没用作用域，现在都用 let