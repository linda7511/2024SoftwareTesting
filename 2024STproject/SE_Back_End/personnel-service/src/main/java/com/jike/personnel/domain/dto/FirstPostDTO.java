package com.jike.personnel.domain.dto;


import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "新岗位实体")
public class FirstPostDTO {
    @ApiModelProperty("岗位id")
    private int postId;

    @ApiModelProperty("岗位工资")
    private int positionSalary;
}
