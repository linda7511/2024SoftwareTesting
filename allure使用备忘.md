### 集成测试

现在点击run test没有执行测试，只是mvn allure:report，要执行测试每次只能先在命令行SE_Back_End目录下手动执行mvn (clean) test。

每次如果新生成allure-results，要将各子模块的allure-results复制到父模块的target/allure-results目录下。

现在的集成测试结果：![image-20240618161631827](D:\Desktop\学习\大三下\软件测试\集成测试结果1.png)

![image-20240618161703513](D:\Desktop\学习\大三下\软件测试\集成测试结果1.2.png)

### 关于生成历史

![92642917ec50a4f98e389bae5bde6dd](D:\Desktop\学习\大三下\软件测试\allure-history.png)

每次点击run-test前，记得将allure-report/history复制到allure-results下，现在没执行mvn clean test，每次run test，allure-report会被清空，但allure-results好像不会。