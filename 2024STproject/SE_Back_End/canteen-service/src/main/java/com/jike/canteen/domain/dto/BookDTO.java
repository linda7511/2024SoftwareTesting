package com.jike.canteen.domain.dto;


import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "预定联系集")
public class BookDTO {
    @ApiModelProperty("桌位id")
    private int tableId;
    @ApiModelProperty("顾客id")
    private int customerId;
    @ApiModelProperty("预定时间")
    private LocalDateTime bookTime;
    @ApiModelProperty("预定人数")
    private int bookNumber;
    @ApiModelProperty("预定状态")
    private String bookStatus;
    @ApiModelProperty("特殊需求")
    private String bookRequement;
    @ApiModelProperty("预定备注")
    private String bookNote;
}
