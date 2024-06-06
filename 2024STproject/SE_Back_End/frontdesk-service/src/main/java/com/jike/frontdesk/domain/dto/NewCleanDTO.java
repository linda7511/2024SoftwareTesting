package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "新建清洁信息实体")
public class NewCleanDTO {
    @ApiModelProperty("员工id")
    private int employeeId;
    @ApiModelProperty("房间号")
    private int roomNumber;
    @ApiModelProperty("清洁时间")
    private LocalDateTime cleaningTime;
}
