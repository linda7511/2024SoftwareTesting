package com.jike.performance.domain.dto;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;
@Data
@ApiModel(description = "考勤信息实体")
public class AttendenceInformationDTO {
    @ApiModelProperty("id")
    private int attendanceId;
    @ApiModelProperty("员工id")
    private int employeeId;
    @ApiModelProperty("年份")
    private int year;
    @ApiModelProperty("月份")
    private int month;
    @ApiModelProperty("预计上班天数")
    private int expectAttendanceDays;
    @ApiModelProperty("实际上班天数")
    private int actualAttendanceDays;
    @ApiModelProperty("个人请假天数")
    private int personalLeaveDays;
    @ApiModelProperty("病假天数")
    private int sickLeaveDays;
    @ApiModelProperty("迟到天数")
    private int lateDays;
    @ApiModelProperty("早离天数")
    private int earlyDepartureDays;
    @ApiModelProperty("未到天数")
    private int absentDays;
}
