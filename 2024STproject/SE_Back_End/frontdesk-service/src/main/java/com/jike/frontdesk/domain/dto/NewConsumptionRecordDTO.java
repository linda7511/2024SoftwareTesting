package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "新增消费记录实体")
public class NewConsumptionRecordDTO {
    @ApiModelProperty("房间id")
    private int roomId;
    @ApiModelProperty("账单id")
    private int billId;
    @ApiModelProperty("消费类型")
    private String consumeType;
    @ApiModelProperty("消费时间")
    private LocalDateTime consumeTime;
    @ApiModelProperty("消费金额")
    private double consumeAmount;

}
