package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "房间更新实体")
public class RoomUpdateDTO {
    @ApiModelProperty("id")
    private int roomId;
    @ApiModelProperty("状态")
    private String status;
}
