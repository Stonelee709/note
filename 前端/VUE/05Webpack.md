### 模块导入导出

#### 1 ES6 导入导出

```javascript
var number='hello js';

function sayNumber() {
    console.log(number);
}

export {
    number,
    sayNumber
}
```

```javascript
import * as aaa from "./a.js"

aaa.sayNumber()
```

#### 2 CommonJS

```javascript
//导出
function add(num1, num2){
    return num1+num2;
}
function mul(num1,num2){
    return num1*num2;
}

module.exports={
    add,
    mul
}

//导入
const {add,mul}=require('./mathutil.js')

console.log(add(100,200))
console.log(mul(2,3))
```



### Webpack

Webpack: 将前端文件按模块化打包。它依赖于 Node 环境。

Node 会自动安装一个包管理工具 npm：node package manager。

不管你的 js 用 ES 自带的模块化功能或使用 CommonJs 的模块化功能, webpack 都能帮你处理好。

``` powershell
webpack ./src/main.js -o ./dist  打包命令
```



### css 打包

```javascript
//在 main.js 中添加
1require(./css/normal.css)
2安装 css-loader 和 style-loader
3 配置
```

https://webpack.docschina.org/concepts/loaders/#using-loaders