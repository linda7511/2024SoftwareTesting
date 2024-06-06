package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "房间实体")
public class RoomDTO {
    @ApiModelProperty("id")
    private int roomId;
    @ApiModelProperty("房间类型id")
    private int typeId;
    @ApiModelProperty("房间号")
    private int roomNumber;
    @ApiModelProperty("房间电话号码")
    private String roomPhone;
    @ApiModelProperty("状态")
    private String status;
}
