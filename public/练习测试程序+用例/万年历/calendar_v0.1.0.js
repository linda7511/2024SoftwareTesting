function calendarJudge(year, month, day) {
    const maxDaysInMonth = [0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    // 检查闰年
    if (year % 4 === 0 && (year % 100 !== 0 || year % 400 === 0)) {
        maxDaysInMonth[2] = 29; // 闰年2月有29天
    }

    //检查年份、月份和日期是否合法
    if (year < 1900 || year > 2100 ||
        month < 1 || month > 12 ||
        day < 1 || day > maxDaysInMonth[month]
    ) {
        return '输入日期越界';
    }

    if (day < maxDaysInMonth[month]) {
        return `${year}/${month}/${day + 1}`;
    }
    else if (month < 12) {
        return `${year}/${month+1}/1`;
    }
    else
        return `${year+1}/1/1`

}

function getArgs(row) {
    let args = [Number.parseInt(row.年), Number.parseInt(row.月), Number.parseInt(row.日)];
    return args;
}
