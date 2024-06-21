function computerSelling(host, monitor, peripheral){
    if (host <= 0 || monitor <= 0 || peripheral <= 0 || host > 70 || monitor > 80 || peripheral > 90) {
        return "数值越界"
    }

    let totalSales = host * 25 + monitor * 30 + peripheral * 45;
    let commission
    if (totalSales <= 1000) {
        commission = totalSales * 0.1;
    } else if (totalSales <= 1800) {
        commission = totalSales * 0.15;
    } else {
        commission = totalSales * 0.2;
    }

    return String(commission)

}

function getArgs(row) {
    let args = [Number.parseInt(row.主机数), Number.parseInt(row.显示器数), Number.parseInt(row.外设数)]
    return args
}