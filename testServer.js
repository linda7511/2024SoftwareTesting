const express = require('express');
const { exec } = require('child_process');
const path = require('path');
const fs = require('fs');
const cors = require('cors');
const app = express();
const { Builder } = require('selenium-webdriver');
const port = 12345;//3000

app.use(express.json());
app.use(cors());

app.post('/run-unitTests', (req, res) => {
    console.log('Current working directory:', process.cwd());

    console.log('Received request to run unit tests');

    const projectPath = path.join(__dirname, '2024STproject/SE_Back_End/canteen-service');

    // const allureResultsPath = path.join(projectPath, 'allure-results');
  
    // // 运行 shell 脚本来管理历史数据
    // exec('path/to/manage_allure_history.sh', (error, stdout, stderr) => {
    //   if (error) {
    //     console.error('Error managing Allure history:', error);
    //     reject(error);
    //     return;
    //   }
    //   console.log('stdout:', stdout);
    //   console.error('stderr:', stderr);
    //   resolve();
    // });

    console.log('projectPath:',projectPath);
    exec('mvn test -Dtest=com.jike.canteen.service.impl.**', { cwd: projectPath },(error, stdout, stderr) => {
        if (error) {
            console.error('Error during mvn clean test:', error);
            return res.status(500).json({ error: 'Error running tests' });
        }
    
        console.log('mvn clean test stdout:', stdout);
        console.error('mvn clean test stderr:', stderr);

        exec('mvn allure:report', { cwd: projectPath }, (error, stdout, stderr) => {
          if (error) {
            console.error('Error during mvn allure:report:', error);
            return res.status(500).json({ error: 'Error generating report' });
          }

          console.log('mvn allure:report stdout:', stdout);
          console.error('mvn allure:report stderr:', stderr);

          const allureReportPath = path.join(__dirname, '2024STproject/SE_Back_End/canteen-service', 'target', 'allure-report', 'index.html');

          fs.readFile(allureReportPath, 'utf8', (readError, data) => {
            if (readError) {
              console.error('Error reading report file:', readError);
              return res.status(500).json({ error: 'Error reading report file' });
            }

            console.log('reportHtml:', data);
            res.json({ reportHtml: data });
          });
        });
      });
});

app.post('/run-integrationTests', (req, res) => {
  console.log('Current working directory:', process.cwd());

  console.log('Received request to run integration tests');

  // 定义模块路径数组
  const modules = ['canteen-service', 'frontdesk-service', 'performance-service', 'personnel-service'];
  const projectPath = path.join(__dirname, '2024STproject/SE_Back_End');
  //const allureResultsPath = path.join(projectPath, 'allure-results');

  console.log('projectPath:',projectPath);
  exec('mvn allure:report', { cwd: projectPath }, (error, stdout, stderr) => {
    if (error) {
      console.error('Error during mvn allure:report:', error);
      return res.status(500).json({ error: 'Error generating report' });
    }

    console.log('mvn allure:report stdout:', stdout);
    console.error('mvn allure:report stderr:', stderr);

    const allureReportPath = path.join(__dirname, '2024STproject/SE_Back_End', 'target', 'allure-report', 'index.html');

    fs.readFile(allureReportPath, 'utf8', (readError, data) => {
      if (readError) {
        console.error('Error reading report file:', readError);
        return res.status(500).json({ error: 'Error reading report file' });
      }

      console.log('reportHtml:', data);
      res.json({ reportHtml: data });
    });
  });
    // exec('mvn clean test', { cwd: projectPath },(error, stdout, stderr) => {
    //     if (error) {
    //         console.error('Error during mvn clean test:', error);
    //         return res.status(500).json({ error: 'Error running tests' });
    //     }
    
    //     console.log('mvn clean test stdout:', stdout);
    //     console.error('mvn clean test stderr:', stderr);

    //     exec('mvn allure:report', { cwd: projectPath }, (error, stdout, stderr) => {
    //       if (error) {
    //         console.error('Error during mvn allure:report:', error);
    //         return res.status(500).json({ error: 'Error generating report' });
    //       }

    //       console.log('mvn allure:report stdout:', stdout);
    //       console.error('mvn allure:report stderr:', stderr);

    //       const allureReportPath = path.join(__dirname, '2024STproject/SE_Back_End', 'target', 'allure-report', 'index.html');

    //       fs.readFile(allureReportPath, 'utf8', (readError, data) => {
    //         if (readError) {
    //           console.error('Error reading report file:', readError);
    //           return res.status(500).json({ error: 'Error reading report file' });
    //         }

    //         console.log('reportHtml:', data);
    //         res.json({ reportHtml: data });
    //       });
    //     });
    //   });

  // 确保allure-results目录存在
  // fs.mkdir(allureResultsPath, { recursive: true }, (err) => {
  //   if (err) {
  //     console.error('Error creating allure-results directory:', err);
  //     return res.status(500).json({ error: 'Error creating results directory' });
  //   }

  //   // 运行每个模块的测试并收集结果
  //   let testsRunning = true;
  //   let finishedModules = 0;

  //   // modules.forEach(moduleName => {
  //   //   const modulePath = path.join(projectPath, moduleName);
  //   //   console.log(`Running tests for module: ${moduleName}`);

  //   //   exec('mvn test', { cwd: modulePath }, (error, stdout, stderr) => {
  //   //     if (error) {
  //   //       console.error(`Error during mvn clean test for ${moduleName}:`, error);
  //   //       console.log(`mvn clean test stdout for ${moduleName}:`, stdout);
  //   //       console.error(`mvn clean test stderr for ${moduleName}:`, stderr);
  //   //       testsRunning = false;
  //   //       return;
  //   //     }

  //   //     console.log(`mvn clean test stdout for ${moduleName}:`, stdout);
  //   //     console.error(`mvn clean test stderr for ${moduleName}:`, stderr);

  //   //     // 收集Allure结果到allure-results目录
  //   //     const allureResultsModulePath = path.join(modulePath, 'target', 'allure-report');
  //   //     const allureResultsDestPath = path.join(allureResultsPath, moduleName);

  //   //     // 确保模块的allure结果目录存在
  //   //     fs.mkdir(allureResultsDestPath, { recursive: true }, (err) => {
  //   //       if (err) {
  //   //         console.error('Error creating module allure-results directory:', err);
  //   //         return;
  //   //       }

  //   //       // 复制Allure结果到allure-results目录
  //   //       fs.copy(allureResultsModulePath, allureResultsDestPath, (err) => {
  //   //         if (err) {
  //   //           console.error('Error copying allure results:', err);
  //   //           return;
  //   //         }
  //   //         console.log(`Allure results copied for module: ${moduleName}`);
  //   //         finishedModules++;

  //   //         // 检查是否所有模块的测试都已完成
  //   //         if (finishedModules === modules.length) {
  //   //           generateAllureReport();
  //   //         }
  //   //       });
  //   //     });
  //   //   });
  //   // });

  //   // 生成最终的Allure报告
    
  //   function generateAllureReport() {
  //     if (!testsRunning) {
  //       return res.status(500).json({ error: 'Some tests failed' });
  //     }

  //     console.log('Generating Allure report...');
  //     exec('mvn allure:serve -Dallure.results.directory=allure-results', { cwd: projectPath }, (error, stdout, stderr) => {
  //       if (error) {
  //         console.error('Error during mvn allure:serve:', error);
  //         return res.status(500).json({ error: 'Error generating report' });
  //       }

  //       console.log('mvn allure:serve stdout:', stdout);
  //       console.error('mvn allure:serve stderr:', stderr);

  //       const allureReportPath = path.join(projectPath, 'allure-report', 'index.html');
  //       fs.readFile(allureReportPath, 'utf8', (readError, data) => {
  //         if (readError) {
  //           console.error('Error reading report file:', readError);
  //           return res.status(500).json({ error: 'Error reading report file' });
  //         }

  //         console.log('reportHtml:', data);
  //         res.json({ reportHtml: data });
  //       });
  //     });
  //   }
  // });
});

app.post('/run-systemTest', async (req, res) => {
  const { test } = req.body;
  const scriptPath = `./test/${test}.js`;

  console.log(`Received request to run system test: ${test}`);

  try {
    // 使用 Mocha 命令行工具执行测试脚本
    exec(`npx mocha ${scriptPath}`, (error, stdout, stderr) => {
      if (error) {
        console.error(`Error running system test: ${error}`);
        return res.status(500).json({ success: false, error: error.message });
      }
      if (stderr) {
        console.error(`Standard Error: ${stderr}`);
        return res.status(500).json({ success: false, error: stderr });
      }
      console.log(stdout); // 打印测试输出
      res.json({ success: true, message: 'Test executed successfully' });
    });
  } catch (error) {
    console.error('Error running system test:', error);
    res.status(500).json({ success: false, error: error.message });
  }
});

app.listen(port, () => {
  //console.log(`Server running at http://localhost:${port}`);
  console.log(`Server running at http://127.0.0.1:${port}`);
});
