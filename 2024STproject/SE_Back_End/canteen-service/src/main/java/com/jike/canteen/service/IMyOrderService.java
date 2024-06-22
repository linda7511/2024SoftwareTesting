package com.jike.canteen.service;


import com.baomidou.mybatisplus.extension.service.IService;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.github.jeffreyning.mybatisplus.service.IMppService;
import com.jike.canteen.domain.dto.CateringRecordDTO;
import com.jike.canteen.domain.dto.MyOrderDTO;
import com.jike.canteen.domain.po.MyOrder;
import com.jike.common.utils.ResponseResult;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;


public interface IMyOrderService extends IMppService<MyOrder> {

    // 添加新订单
    public ResponseResult<String> addMyOrder(@RequestBody MyOrderDTO myOrderDTO);

    // 更新订单
    public ResponseResult<String> updateMyOrder(@RequestBody MyOrderDTO myOrderDTO);

    // 根据桌号获取订单和菜品信息
    public ResponseResult<?> getOrderAndDishInfo(@PathVariable("tableId") int tableId);

    // 更新消费记录ID及订单信息
    public ResponseResult<?> updateConsumpId(@PathVariable("consumptionRecordId") int consumptionRecordId, @RequestBody String orderMessages) throws JsonProcessingException;

    // 添加餐饮记录
    public ResponseResult<String> addCateringRecord(@RequestBody CateringRecordDTO cateringRecordDTO);
}
