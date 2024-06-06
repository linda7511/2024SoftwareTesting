package com.jike.frontdesk.domain.vo;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "顾客实体")
public class CustomerVO {
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
    @ApiModelProperty("入住时间")
    private LocalDateTime checkinTime;
    @ApiModelProperty("退房时间")
    private LocalDateTime checkoutTime;
}
