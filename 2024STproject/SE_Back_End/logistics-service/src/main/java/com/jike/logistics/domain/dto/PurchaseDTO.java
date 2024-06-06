package com.jike.logistics.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.util.Date;

@Data
@ApiModel(description = "采购实体")
public class PurchaseDTO {
    @ApiModelProperty("采购id")
    private int purchaseId;
    @ApiModelProperty("物资id")
    private int goodsId;
    @ApiModelProperty("员工id")
    private int employeeId;
    @ApiModelProperty("购买日期")
    private Date purchaseDate;
    @ApiModelProperty("采购数量")
    private int purchaseQuantity;
    @ApiModelProperty("单价")
    private double unitPrice;
}
