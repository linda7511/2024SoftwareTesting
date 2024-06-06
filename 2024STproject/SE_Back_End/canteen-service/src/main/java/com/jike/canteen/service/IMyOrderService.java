package com.jike.canteen.service;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.github.jeffreyning.mybatisplus.service.IMppService;
import com.jike.canteen.domain.dto.CateringRecordDTO;
import com.jike.canteen.domain.dto.MyOrderDTO;
import com.jike.canteen.domain.po.MyOrder;
import com.jike.common.utils.ResponseResult;

public interface IMyOrderService extends IMppService<MyOrder> {

    public ResponseResult<?> addMyOrder(MyOrderDTO myOrderDTO);

    public ResponseResult<?> updateMyOrder(MyOrderDTO myOrderDTO);

    public ResponseResult<?> getOrderAndDishInfo(int tableId);

    public ResponseResult<?> updateConsumpId(int consumptionRecordId, String orderMessages) throws JsonProcessingException;

    public ResponseResult<String> addCateringRecord(CateringRecordDTO cateringRecordDTO);
}
