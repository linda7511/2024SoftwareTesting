package com.jike.frontdesk.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "支付实体")
public class PayDTO {
    @ApiModelProperty("顾客id")
    private int customerId;
    @ApiModelProperty("账单id")
    private int billId;
    @ApiModelProperty("支付方式")
    private String payMethod;
    @ApiModelProperty("支付时间")
    private LocalDateTime payTime;
    @ApiModelProperty("支付金额")
    private double payAmount;
    @ApiModelProperty("支付状态")
    private String payStatus;
}
