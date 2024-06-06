const express = require('express');
const { exec } = require('child_process');
const path = require('path');
const fs = require('fs');
const cors = require('cors');
const app = express();
const port = 3000;

app.use(express.json());
app.use(cors());

app.post('/run-tests', (req, res) => {
    console.log('Current working directory:', process.cwd());

    console.log('Received request to run tests');

    const projectPath = path.join(__dirname, '2024STproject/SE_Back_End/canteen-service');

    console.log('projectPath:',projectPath);
    exec('mvn clean test', { cwd: projectPath },(error, stdout, stderr) => {
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

      const allureReportPath = path.join(__dirname, '2024STproject/SE_Back_End/canteen-service', 'target', 'site', 'allure-maven-plugin', 'index.html');

      fs.readFile(allureReportPath, 'utf8', (readError, data) => {
        if (readError) {
          console.error('Error reading report file:', readError);
          return res.status(500).json({ error: 'Error reading report file' });
        }

        res.json({ reportHtml: data });
      });
    });
  });
});

app.listen(port, () => {
  console.log(`Server running at http://localhost:${port}`);
});
