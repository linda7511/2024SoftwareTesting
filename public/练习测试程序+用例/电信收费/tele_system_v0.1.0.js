function tele_toll_system(t, n) {
    if(t<0||n<0||t>44640||n>11)
        return "数值越界";
    if(t>=0&&t<=60){
        var result= n<=1?25+0.15*t*(1-0.01):25+0.15*t;
        return result.toFixed(2);
    }
    if(t>60&&t<=120){
        var result= n<=2?25+0.15*t*(1-0.015):25+0.15*t;
        return result.toFixed(2);
    }
    if(t>120&&t<=180){
        var result= n<=3?25+0.15*t*(1-0.02):25+0.15*t;
        return result.toFixed(2);
    }
    if(t>180&&t<=300){
        var result= n<=3?25+0.15*t*(1-0.025):25+0.15*t;
        return result.toFixed(2);
    }
    if(t>300){
        var result= n<=6?25+0.15*t*(1-0.03):25+0.15*t;
        return result.toFixed(2);
    }
}

function getArgs(row) {
    let args = [Number.parseInt(row.本月通话分钟数), Number.parseInt(row.本年度未按时缴费次数)];
    return args;
}
