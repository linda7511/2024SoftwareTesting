package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "顾客实体")
public class CustomerDTO {
    @ApiModelProperty("id")
    private int customerId;
    @ApiModelProperty("身份证号")
    private String ID;
    @ApiModelProperty("性别")
    private String gender;
    @ApiModelProperty("电话")
    private String phone;
    @ApiModelProperty("信用等级")
    private String creditGrade;
    @ApiModelProperty("会员等级")
    private String memberGrade;
    @ApiModelProperty("姓名")
    private String name;
}