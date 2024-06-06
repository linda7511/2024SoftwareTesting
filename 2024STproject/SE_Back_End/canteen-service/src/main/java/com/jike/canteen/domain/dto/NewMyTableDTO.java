package com.jike.canteen.domain.dto;


import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "新增桌位实体")
public class NewMyTableDTO {
    @ApiModelProperty("容纳人数")
    private int capacity;
    @ApiModelProperty("桌位类型")
    private String tableType;
    @ApiModelProperty("桌位位置")
    private String tableLocation;
    @ApiModelProperty("桌位状态")
    private String tableStatus;
    @ApiModelProperty("桌位备注")
    private String note;
    @ApiModelProperty("是否可预订")
    private int bookable;
    @ApiModelProperty("是否可用")
    private int available;
}
