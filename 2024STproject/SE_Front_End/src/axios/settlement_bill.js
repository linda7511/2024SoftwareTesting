import { get } from './axiosConfig.js'

export async function getConsumptionRecord(select,searchItem) {
    try {
        console.log('select:'+select);
        // 在调用函数之前将 searchItem 转换为数字
        const searchItemAsNumber = parseInt(searchItem); 
        if(select=='房间号'){
            const response = await get('/api/ConsumptionRecord/GetByRoomNum/' + searchItemAsNumber, {});
            console.log(response.data);
            return response.data;
        }
        else if(select == '身份证'){
            const response = await get('/api/ConsumptionRecord/GetByID/' + searchItem, {});
            console.log(response).data;
            return response.data;
        }
    } catch (error) {
        console.error(error);
    }
}

export async function Statistics(){
    //获取所有支付数据
    const payData = await get('/api/Pay/GetAll',{});
    console.log('payData:',payData.data);
    if (!Array.isArray(payData.data)) {
      throw new Error("Invalid data format");
  }
    // 获取当前日期
    const currentDate = new Date();
    // 将当前日期的时间部分设为0点
    currentDate.setHours(0, 0, 0, 0);

    // 获取昨天的日期
    const yesterday = new Date(currentDate);
    yesterday.setDate(currentDate.getDate() - 1);
    console.log("今天："+currentDate);
    console.log("昨天："+yesterday);

    // 获取上个月的日期
    const lastMonth = new Date(currentDate);
    lastMonth.setMonth(currentDate.getMonth()-1);
    lastMonth.setDate(1);
    lastMonth.setHours(0, 0, 0, 0);
    console.log("上月："+lastMonth);

    const yesterdayTimestamp = yesterday.getTime();
    const currentDateTimestamp = currentDate.getTime();
    // 过滤出昨天和今天的支付记录
    const yesterdayPayments = payData.data.filter(payment => {
        const payTime = new Date(payment.PayTime).getTime();
        return (
        payTime >= yesterdayTimestamp  && payTime < currentDateTimestamp
        );
    });
    console.log("yesterdayPayments",yesterdayPayments);
  
    const tomorrow = new Date(currentDate);
    tomorrow.setDate(currentDate.getDate() + 1);
    const tomorrowTimestamp = tomorrow.getTime();
    const todayPayments = payData.data.filter(payment => {
        const payTime = new Date(payment.PayTime).getTime();
        return (
        payTime >= currentDateTimestamp && payTime < tomorrowTimestamp
        );
    });
    console.log(todayPayments);


    // 过滤出上个月和当月的支付记录
    const lastMonthPayments = payData.data.filter(payment => {
      const payTime = new Date(payment.PayTime);
      return (
        payTime >= lastMonth && payTime < currentDate
      );
    });
    console.log(lastMonthPayments);
    
    const currentMonthPayments = payData.data.filter(payment => {
      const payTime = new Date(payment.PayTime);
      const firstDayOfMonth = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1);
      const newCurrentDate = new Date();
      return (
          payTime >= firstDayOfMonth && payTime < new Date(newCurrentDate)
      );
    });
    console.log(currentMonthPayments);

    // 计算昨天和今天的收银额
    const yesterdayRevenue = yesterdayPayments.reduce((total, payment) => total + payment.PayCount, 0);
    const todayRevenue = todayPayments.reduce((total, payment) => total + payment.PayCount, 0);
    var dayRevenueRate;
    if(yesterdayRevenue)
      dayRevenueRate = (todayRevenue - yesterdayRevenue)/yesterdayRevenue*100;
    else
      dayRevenueRate = (todayRevenue - yesterdayRevenue)/1*100;

    // 计算上个月和当月的收银额
    const lastMonthRevenue = lastMonthPayments.reduce((total, payment) => total + payment.PayCount, 0);
    const currentMonthRevenue = currentMonthPayments.reduce((total, payment) => total + payment.PayCount, 0);
    var monthRevenueRate;
    if(lastMonthRevenue)
      monthRevenueRate = (currentMonthRevenue - lastMonthRevenue)/lastMonthRevenue*100;
    else
    monthRevenueRate = (currentMonthRevenue - lastMonthRevenue)/1*100;

    // 计算昨天和今天的收银单量
    const yesterdayPaymentCount = yesterdayPayments.length;
    const todayPaymentCount = todayPayments.length;
    var dayPaymentRate;
    if(yesterdayPaymentCount)
      dayPaymentRate = (todayPaymentCount - yesterdayPaymentCount)/yesterdayPaymentCount*100;
    else
    dayPaymentRate = (todayPaymentCount - yesterdayPaymentCount)/1*100;


    return [dayRevenueRate,todayRevenue,monthRevenueRate,currentMonthRevenue,dayPaymentRate,todayPaymentCount];

}
