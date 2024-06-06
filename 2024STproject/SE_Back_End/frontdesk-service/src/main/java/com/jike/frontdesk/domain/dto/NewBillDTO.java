package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "新增账单实体")
public class NewBillDTO {
    @ApiModelProperty("住房开销")
    private double roomCost;
    @ApiModelProperty("食物开销")
    private double foodCost;
    @ApiModelProperty("其他开销")
    private double otherCost;
    @ApiModelProperty("总开销")
    private double sumCost;
    @ApiModelProperty("账单类型")
    private String billType;
    @ApiModelProperty("发票代码")
    private String invoiceNumber;
    @ApiModelProperty("顾客id")
    private int customerId;
}
