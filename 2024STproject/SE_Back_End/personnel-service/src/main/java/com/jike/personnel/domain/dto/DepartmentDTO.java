package com.jike.personnel.domain.dto;


import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "部门实体")
public class DepartmentDTO {

    @ApiModelProperty("部门id")
    private int departmentId;
    @ApiModelProperty("部门名称")
    private String departmentName;
    @ApiModelProperty("部门人数")
    private int numberOfPeople;
}
