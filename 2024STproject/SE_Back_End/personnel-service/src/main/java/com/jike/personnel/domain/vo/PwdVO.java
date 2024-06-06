package com.jike.personnel.domain.vo;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.util.Date;

@Data
@ApiModel(description = "修改密码VO")
public class PwdVO {
    @ApiModelProperty("员工id")
    private int employeeId;
    @ApiModelProperty("密码")
    private String password;
}
