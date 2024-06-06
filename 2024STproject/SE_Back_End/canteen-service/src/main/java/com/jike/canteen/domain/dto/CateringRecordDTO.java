package com.jike.canteen.domain.dto;

import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.util.List;

@Data
@ApiModel(description = "新建CateringRecord信息实体")
public class CateringRecordDTO {

//    RoomNum: roomNum,
//    SumCost: this.sumCost,
//    tableId: this.selectedTable.tableId,
//    EmployeeId: userInfo.EmployeeId,
//    orderMessages: dishPK,
    @ApiModelProperty("room number")
    private int roomNumber;

    @ApiModelProperty("总消费")
    private double consumeAmount;

    @ApiModelProperty("桌子id")
    private int tableId;

    @ApiModelProperty("message list")
    private List<TempOrderDTO> orderMessages;
}
