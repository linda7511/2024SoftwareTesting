package com.jike.personnel.domain.vo;

import com.jike.personnel.domain.dto.EmployeeDTO;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "登录成功返回账号")
public class LoginVO {
    @ApiModelProperty("token")
    private String token;
    @ApiModelProperty("状态码")
    private int statusCode;
    @ApiModelProperty("message")
    private String message;
    @ApiModelProperty("员工")
    private EmployeeDTO employee;
    @ApiModelProperty("部门")
    private String departmentName;
}
