package com.jike.logistics.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDate;

@Data
@ApiModel(description = "消耗实体")
public class ConsumeDTO {
    @ApiModelProperty("消耗id")
    private int consumeId;
    @ApiModelProperty("部门id")
    private int departmentId;
    @ApiModelProperty("物资id")
    private int goodsId;
    @ApiModelProperty("消耗数量")
    private int consumeAmount;
}
