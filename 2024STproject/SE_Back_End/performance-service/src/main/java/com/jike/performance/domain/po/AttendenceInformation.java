package com.jike.performance.domain.po;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;

import java.io.Serializable;

@Data
@EqualsAndHashCode(callSuper = false)
@Accessors(chain = true)
@TableName("ATTENDENCEINFORMATION")
public class AttendenceInformation implements Serializable {
    private static final long serialVersionUID = 1L;
    /**
     * id
     */
    @TableId(value = "ATTENDANCE_ID", type = IdType.AUTO)
    private int attendanceId;

    /**
     * 员工ID
     */
    @TableField("EMPLOYEE_ID")
    private int employeeId;

    /**
     * 年份
     */
    @TableField("YEAR")
    private int year;

    /**
     * 月份
     */
    @TableField("MONTH")
    private int month;

    /**
     * 预计上班天数
     */
    @TableField("EXPECT_ATTENDANCE_DAYS")
    private int expectAttendanceDays;
    /**
     * 实际上班天数
     */
    @TableField("ACTUAL_ATTENDANCE_DAYS")
    private int actualAttendanceDays;
    /**
     * 个人请假天数
     */
    @TableField("PERSONAL_LEAVE_DAYS")
    private int personalLeaveDays;
    /**
     * 病假天数
     */
    @TableField("SICK_LEAVE_DAYS")
    private int sickLeaveDays;
    /**
     * 迟到天数
     */
    @TableField("LATE_DAYS")
    private int lateDays;
    /**
     * 早离天数
     */
    @TableField("EARLY_DEPARTURE_DAYS")
    private int earlyDepartureDays;
    /**
     * 未到天数
     */
    @TableField("ABSENT_DAYS")
    private int absentDays;


}
