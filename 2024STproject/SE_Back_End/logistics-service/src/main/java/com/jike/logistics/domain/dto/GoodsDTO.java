package com.jike.logistics.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "物资实体")
public class GoodsDTO {
    @ApiModelProperty("物资id")
    private int goodsId;
    @ApiModelProperty("种类")
    private String category;
    @ApiModelProperty("物资名称")
    private String goodsName;
    @ApiModelProperty("库存")
    private int inventory;
}
