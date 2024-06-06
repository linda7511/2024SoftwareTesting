# hotel-management-system-front

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles for vue-router4.0
```
npm install vue-router@4
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```

### 使用Element-plus
```
npm install element-plus --save
npm i unplugin-element-plus -D
```

### 使用Element-plus的Icon
```
npm install @element-plus/icons-vue
```

### 自动导入
```
npm install -D unplugin-vue-components unplugin-auto-import
```

### 引入pinia
```
npm install pinia
```



### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).


### 问题
#### 物资管理
- 增加和修改都应有表单独记录，而不是直接修改
- 有待增加的清单，勾选了就自动增加库存，有增加的记录
- 消耗也要有表记录，记录是谁的消耗的
#### 网络订单处理和宾客入住
- 功能连续，应该在同一个页面方便操作
#### 数据分页
- 不能一个el-table展示完几万条数据
- 数据也应该分页请求得到
#### 配色
