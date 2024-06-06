package com.jike.personnel.domain.dto;


import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.util.Date;

@Data
@ApiModel(description = "申请实体")
public class IfApplicationDTO {


    /**
     * 申请号
     */
    @ApiModelProperty("申请号")
    private int applicationId;


    @ApiModelProperty("申请时间")
    private Date applicationTime;

    /**
     * 申请类型
     */
    @ApiModelProperty("申请类型")
    private String applicationType;
    /**
     * 申请内容
     */
    @ApiModelProperty("申请内容")
    private String applicationContent;
    /**
     * 是否批准
     */
    @ApiModelProperty("是否批准")
    private String ifApprove;
    /**
     * 员工id
     */
    @ApiModelProperty("员工id")
    private int employeeId;
}
