package com.jike.logistics.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "维修实体")
public class MaintainDTO {
    @ApiModelProperty("房间id")
    private int roomId;
    @ApiModelProperty("员工id")
    private int employeeId;
    @ApiModelProperty("维修时间")
    private LocalDateTime maintainingTime;
    @ApiModelProperty("维修项")
    private String maintainingItem;
    @ApiModelProperty("维修结果")
    private String maintainingResult;
}
