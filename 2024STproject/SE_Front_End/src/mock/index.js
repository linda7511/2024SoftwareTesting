//仅用于测试接口
const Mock = require("mockjs");
let attendanceInfo = Mock.mock({
    "data|6": [ //生成6条数据 数组
        {
            "attendanceId|+1": 1,//生成商品id，自增1
            "empId|1-5": 1,//生成商品id，自增1
            "year|2000-2023": 2003, 
            "month|1-12": 11,
            "expectAttendanceDays|1-30": 11,
            "actualAttendanceDays|1-30": 11,
            "personalLeaveDays|1-30": 11,
            "sickLeaveDays|1-30": 11,
            "lateDays|1-30": 11,
            "earlyDepartureDays|1-30": 11,
            "absentDays|1-30": 11,
        }
    ]
})
let employees = Mock.mock({
    "data|5": [ //生成6条数据 数组
        {
            "employeeId|+1": 1,//生成商品id，自增1
            "postId|1-4": 1,//生成商品id，自增1
            "departmentId|1-3": 1, //随机生成商品价格 在30-1000之间
            "name": "@cname",//生成商品名 ， 都是中国人的名字
            "departmentName|+1": ['人事部','后勤部','餐饮部'],
            "postName|+1": ['经理','主管','实习生'],
        }
    ]
})
let posts = Mock.mock({
    "data|5": [ //生成6条数据 数组
        {
            "postId|+1": 1,//生成商品id，自增1
            "postName": "@cname",//生成商品名 ， 都是中国人的名字
            "departmentId|+1": 1,
            "authority": "@cname",
            "positionLevel": "@cname",
            "positionSalary|3000-50000": 3000,
            "paymentType": "@cname",
            "paymentBase": "@cname",
            "insurance": "@cname",
            "accumulationFunds|3000-50000": 3000,
        }
    ]
})

let departments = Mock.mock({
    "data|3": [ //生成6条数据 数组
        {
            "departmentId|+1": 1,
            "departmentName|+1": ['人事部','后勤部','餐饮部'],
            "numberOfPeople|5-100": 5,
        }
    ]
})
let salarys = Mock.mock({
    "data|6": [ //生成6条数据 数组
    {
        "assetsId|+1": 1,
        "empId|1-4": 1,
        "year|2000-2023": 2003, 
        "month|1-12": 11,
        "bonus|100-10000": 100,
        "holidayAllowance|50-2000": 50,
        "otherAllowance|50-2000": 50,
        "commission|50-2000": 50,
        "yearEndBonus|100-10000": 100,
        "overtimePay|50-1000": 50,
        "rewardType": "string",
        "rewardAmount|50-2000": 50,
        "lateDeduction|50-2000": 50,
        "earlyDepartureDeduction|50-2000": 50,
        "incomeTax|50-2000": 50,
        "socialInsurance|50-2000": 50,
        "grossSalary|3000-50000": 3000,
        "netSalary|3000-50000": 3000,
      }
    ]
})

let ConsumptionRecords = Mock.mock({
    "data|6": [ //生成6条数据 数组
        {
            "consumeId|+1": 1,
            "roomId|+1": 1,
            "billId|+1": 1,
            "employeeId|+1": 1,
            "consumeType|+1": ['娱乐','餐饮','住房'],
            "consumeTime": "@datetime",
            "consumeAmount|10-10000": 10,
        }
    ]
})

let pays = Mock.mock({
    "data|5": [ //生成6条数据 数组
        {
            "customerId|+1": 1,
            "billId|+1": 1,
            "payWay|+1": ['微信','支付宝','现金'],
            "payTime": '@time',
            "status|+1": ['已支付','未支付'],
        }
    ]
})

let bills = Mock.mock({
    "data|5": [ //生成6条数据 数组
        {
            "billId|+1": 1,
            "roomCost|10-10000": 10,
            "custofoodCostmerId|10-10000": 10,
            "otherCost|10-10000": 10,
            "sumCost|10-10000": 10,
            "billType|+1": ['微信','支付宝','现金'],
            "invoiceNumber|+1": ['已支付','未支付'],
        }
    ]
})

Mock.mock(/\/api\/AttendanceInformation\/GetByEmpId\/\d+/, 'get', (options) => {
    console.log('get');
    const url = options.url;
    console.log(url);
    const empId = url.match(/\/api\/AttendanceInformation\/GetByEmpId\/(\d+)/)[1];
    console.log(empId);
    const filteredData = attendanceInfo.data.filter(item => item.empId == empId);
    console.log(filteredData);
    return filteredData;
});

Mock.mock('/api/AttendanceInformation/Add', 'post', (options) => {
    const newData = JSON.parse(options.body);
    // 计算新的attendanceId
    const lastAttendanceId = attendanceInfo.data[attendanceInfo.data.length - 1]?.attendanceId || 0;
    newData.attendanceId = lastAttendanceId + 1;

    // 将新数据添加到数组中
    attendanceInfo.data.push(newData);

    return newData;
});

Mock.mock('/api/Employee/GetAll', 'get', () => {
    return employees.data;
});
Mock.mock('/api/Post/GetAll', 'get', () => {
    return posts.data;
});
Mock.mock('/api/Employee/Update', 'put', (options) => {
    const newData = JSON.parse(options.body);
    // 计算新的attendanceId
    const updatedData  = employees.data.find(item => item.employeeId === newData.employeeId);

    if (updatedData) {
        updatedData.postId = newData.postId;
    }

    return updatedData;
});

Mock.mock(/\/api\/Department\/Get\/\d+/, 'get', (options) => {
    console.log('get');
    const url = options.url;
    console.log(url);
    const departmentId = url.match(/\/api\/Department\/Get\/(\d+)/)[1];
    console.log(departmentId);
    const filteredData = departments.data.filter(item => item.departmentId == departmentId);
    console.log(filteredData);
    return filteredData;
});

Mock.mock(/\/api\/Salary\/GetByEmployeeId\/\d+/, 'get', (options) => {
    console.log('get');
    const url = options.url;
    console.log(url);
    const empId = url.match(/\/api\/Salary\/GetByEmployeeId\/(\d+)/)[1];
    console.log(empId);
    const filteredData = salarys.data.filter(item => item.empId == empId);
    console.log(filteredData);
    return filteredData;
});

Mock.mock(/\/api\/Salary\/Delete\/\d+/, 'delete', (options) => {
    console.log('del');
    const url = options.url;
    console.log(url);
    const assetsId = url.match(/\/api\/Salary\/Delete\/(\d+)/)[1];
    console.log(assetsId);
    const filteredDataIndex = salarys.data.findIndex(item => item.assetsId == assetsId);
    console.log(filteredDataIndex);

    if (filteredDataIndex !== -1) {
        const removedData = salarys.data.splice(filteredDataIndex, 1);
        console.log(salarys);
        return removedData;
    } else {
        console.log("Data not found");
        return null;
    }
});

Mock.mock('/api/Salary/Add/', 'post', (options) => {
    const newData = JSON.parse(options.body);
    // 计算新的lastAssetsId
    const lastAssetsId = salarys.data[salarys.data.length - 1]?.assetsId || 0;
    newData.assetsId = lastAssetsId + 1;

    // 将新数据添加到数组中
    salarys.data.push(newData);

    return newData;
});

Mock.mock('/api/Post/Update', 'put', (options) => {
    const newData = JSON.parse(options.body);
    // 计算新的attendanceId
    const updatedData  = posts.data.find(item => item.postId === newData.postId);

    if (updatedData) {
        updatedData.positionSalary = newData.positionSalary;
    }
    console.log(posts.data);

    return updatedData;
});

Mock.mock(/\/api\/ConsumptionRecord\/GetByRoomNum\/\d+/, 'get', (options) => {
    console.log('get');
    const url = options.url;
    console.log(url);
    const roomId = url.match(/\/api\/ConsumptionRecord\/GetByRoomNum\/(\d+)/)[1];
    console.log(roomId);
    const filteredData = ConsumptionRecords.data.filter(item => item.roomId == roomId);
    console.log(filteredData);
    return filteredData;
});

Mock.mock(/\/api\/ConsumptionRecord\/GetByID\/\d+/, 'get', (options) => {
    console.log('get');
    const url = options.url;
    console.log(url);
    const employeeId = url.match(/\/api\/ConsumptionRecord\/GetByID\/(\d+)/)[1];
    console.log(employeeId);
    const filteredData = ConsumptionRecords.data.filter(item => item.employeeId == employeeId);
    console.log(filteredData);
    return filteredData;
});

Mock.mock('/api/Pay/Add', 'post', (options) => {
    const newData = JSON.parse(options.body);
    // 计算新的attendanceId
    // const lastAttendanceId = attendanceInfo.data[attendanceInfo.data.length - 1]?.attendanceId || 0;
    // newData.attendanceId = lastAttendanceId + 1;

    // 将新数据添加到数组中
    pays.data.push(newData);

    return newData;
});

Mock.mock('/api/Bill/Add', 'post', (options) => {
    const newData = JSON.parse(options.body);
    // 计算新的attendanceId
    const lastBillId = bills.data[bills.data.length - 1]?.billId || 0;
    newData.billId = lastBillId + 1;

    // 将新数据添加到数组中
    bills.data.push(newData);

    return newData.billId;
});