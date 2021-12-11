### 安装

npm install @vue.cli

vue create 项目名



### v-for

```vue
<li v-for="(item,index) in mylist" v-bind:key="index" v-show="isDisplay" >
    {{item}}---{{index}}
  </li>
```

