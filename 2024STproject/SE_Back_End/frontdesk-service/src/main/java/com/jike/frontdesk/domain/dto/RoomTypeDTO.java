package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "房间类型实体")
public class RoomTypeDTO {
    @ApiModelProperty("id")
    private int typeId;
    @ApiModelProperty("房间类型名称")
    private String typeName;
    @ApiModelProperty("价格")
    private double price;
    @ApiModelProperty("余量")
    private int remaining;
    @ApiModelProperty("备注")
    private String note;
}
