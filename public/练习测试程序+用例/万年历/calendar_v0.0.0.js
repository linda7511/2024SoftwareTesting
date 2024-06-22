function calendarProblem(year, month, day) {
    //自己写的
    if (year < 1900 || year > 2100 || month < 1 || month > 12 || day < 1)
    return "输入日期越界";

    let daysInMonth;
    if (month == 2) {
        if ((year % 4 == 0 || year % 100 != 0) || (year % 400 == 0)) {
            daysInMonth = 29;
        } else {
            daysInMonth = 28;
        }
    } else if (month == 4 || month == 6 || month == 9 || month == 11) {
        daysInMonth = 30;
    } else {
        daysInMonth = 31;
    }

    if (day > daysInMonth)
        return "输入日期越界";

    let nextYear = year;
    let nextMonth = month;
    let nextDay = day + 1;

    if (nextDay > daysInMonth) {
        nextDay = 1;
        nextMonth++;
        if (nextMonth > 12) {
            nextMonth = 1;
            nextYear++;
        }
    }

    return nextYear + "/" + nextMonth + "/" + nextDay;
}

function getArgs(row) {
    let args = [Number.parseInt(row.年), Number.parseInt(row.月), Number.parseInt(row.日)]
    return args
}
