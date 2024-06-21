function tele_toll_system(t, n) {
    if(t<0||n<0)
        return "数值越界";
    if(t>=0&&t<60){
        return n<=1?25+0.15*t*0.01:25+0.15*t;
    }
    if(t>=60&&t<=120){
        return n<=2?25+0.15*t*0.015:25+0.15*t;
    }
    if(t>120&&t<180){
        return n<=3?25+0.15*t*0.02:25+0.15*t;
    }
    if(t>=180&&t<=300){
        return n<=3?25+0.15*t*0.025:25+0.15*t;
    }
    if(t>300){
        return n<=6?25+0.15*t*0.03:25+0.15*t;
    }
}

function getArgs(row) {
    let args = [Number.parseInt(row.本月通话分钟数), Number.parseInt(row.本年度未按时缴费次数)];
    return args;
}
