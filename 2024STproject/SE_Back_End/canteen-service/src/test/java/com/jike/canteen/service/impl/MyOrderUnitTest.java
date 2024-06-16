package com.jike.canteen.service.impl;

import com.jike.canteen.domain.dto.*;
import com.jike.common.utils.ResponseResult;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.Rollback;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.assertEquals;

@Transactional
@Rollback
@SpringBootTest
public class MyOrderUnitTest {

    @Autowired
    private IMyOrderService myOrderService;

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderAddSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试添加订单成功")
    public void createNewOrder(int tableId,int dishId,int consumption_recordId,
                                   LocalDateTime orderTime,String orderStatus) {
        MyOrderDTO myOrderDTO = new MyOrderDTO();
        myOrderDTO.setTableId(tableId);
        myOrderDTO.setDishId(dishId);
        myOrderDTO.setOrderTime(orderTime);
        myOrderDTO.setConsumptionRecordId(consumption_recordId);
        myOrderDTO.setOrderStatus(orderStatus);

        ResponseResult<?> response = myOrderService.addMyOrder(myOrderDTO);
        assertEquals("新增成功", response.getMessage());
    }


    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderAddF1.csv", numLinesToSkip = 1)
    @DisplayName("测试添加订单失败，参数为空")
    public void createOrderWithEmptyParameters(Integer tableId,Integer dishId,Integer consumption_recordId,
                              LocalDateTime orderTime,String orderStatus) {
        MyOrderDTO myOrderDTO = new MyOrderDTO();
        myOrderDTO.setTableId(tableId != null ? tableId : 0);
        myOrderDTO.setDishId(dishId != null ? dishId : 0);
        myOrderDTO.setOrderTime(orderTime);
        myOrderDTO.setConsumptionRecordId(consumption_recordId);
        myOrderDTO.setOrderStatus(orderStatus);

        ResponseResult<?> response = myOrderService.addMyOrder(myOrderDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderAddF2.csv", numLinesToSkip = 1)
    @DisplayName("测试添加订单失败，外键不存在")
    public void registerNewOrder_ForeignKeyMissing(Integer tableId,Integer dishId,Integer consumption_recordId,
                              LocalDateTime orderTime,String orderStatus) {
        MyOrderDTO myOrderDTO = new MyOrderDTO();
        myOrderDTO.setTableId(tableId);
        myOrderDTO.setDishId(dishId);
        myOrderDTO.setOrderTime(orderTime);
        myOrderDTO.setConsumptionRecordId(consumption_recordId);
        myOrderDTO.setOrderStatus(orderStatus);

        ResponseResult<?> response = myOrderService.addMyOrder(myOrderDTO);
        assertEquals("新增失败：外键不存在", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderAddF3.csv", numLinesToSkip = 1)
    @DisplayName("测试添加预订信息失败，orderstatus非法")
    public void registerNewOrder_InvalidStatus(Integer tableId,Integer dishId,Integer consumption_recordId,
                              LocalDateTime orderTime,String orderStatus) {
        MyOrderDTO myOrderDTO = new MyOrderDTO();
        myOrderDTO.setTableId(tableId);
        myOrderDTO.setDishId(dishId);
        myOrderDTO.setOrderTime(orderTime);
        myOrderDTO.setConsumptionRecordId(consumption_recordId);
        myOrderDTO.setOrderStatus(orderStatus);

        ResponseResult<?> response = myOrderService.addMyOrder(myOrderDTO);
        assertEquals("新增失败", response.getMessage());
    }




    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderUpdateF1.csv", numLinesToSkip = 1)
    @DisplayName("测试修改订单失败，主键冲突")
    public void amendOrderInfo_DuplicateKey(Integer tableId,Integer dishId,Integer consumption_recordId,
                                     LocalDateTime orderTime,String orderStatus) {
        MyOrderDTO myOrderDTO = new MyOrderDTO();
        myOrderDTO.setTableId(tableId != null ? tableId : 0);
        myOrderDTO.setDishId(dishId != null ? dishId : 0);
        myOrderDTO.setOrderTime(orderTime);
        myOrderDTO.setConsumptionRecordId(consumption_recordId);
        myOrderDTO.setOrderStatus(orderStatus);

        ResponseResult<?> updateResponse = myOrderService.updateMyOrder(myOrderDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderUpdateF2.csv", numLinesToSkip = 1)
    @DisplayName("测试修改订单失败，orderStatus非法")
    public void amendOrderInfo_InvalidStatus(Integer tableId,Integer dishId,Integer consumption_recordId,
                                     LocalDateTime orderTime,String orderStatus) {
        MyOrderDTO myOrderDTO = new MyOrderDTO();
        myOrderDTO.setTableId(tableId != null ? tableId : 0);
        myOrderDTO.setDishId(dishId != null ? dishId : 0);
        myOrderDTO.setOrderTime(orderTime);
        myOrderDTO.setConsumptionRecordId(consumption_recordId);
        myOrderDTO.setOrderStatus(orderStatus);

        ResponseResult<?> updateResponse = myOrderService.updateMyOrder(myOrderDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderUpdateF3.csv", numLinesToSkip = 1)
    @DisplayName("测试修改订单失败，参数为空")
    public void amendOrderInfo_EmptyParameters(Integer tableId,Integer dishId,Integer consumption_recordId,
                                     LocalDateTime orderTime,String orderStatus) {
        MyOrderDTO myOrderDTO = new MyOrderDTO();
        myOrderDTO.setTableId(tableId != null ? tableId : 0);
        myOrderDTO.setDishId(dishId != null ? dishId : 0);
        myOrderDTO.setOrderTime(orderTime);
        myOrderDTO.setConsumptionRecordId(consumption_recordId);
        myOrderDTO.setOrderStatus(orderStatus);

        ResponseResult<?> updateResponse = myOrderService.updateMyOrder(myOrderDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderUpdateSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试修改预订信息成功")
    public void amendOrderInfo_Success(Integer tableId,Integer dishId,Integer consumption_recordId,
                                          LocalDateTime orderTime,String orderStatus) {
        MyOrderDTO myOrderDTO = new MyOrderDTO();
        myOrderDTO.setTableId(tableId != null ? tableId : 0);
        myOrderDTO.setDishId(dishId != null ? dishId : 0);
        myOrderDTO.setOrderTime(orderTime);
        myOrderDTO.setConsumptionRecordId(consumption_recordId);
        myOrderDTO.setOrderStatus(orderStatus);

        ResponseResult<?> updateResponse = myOrderService.updateMyOrder(myOrderDTO);

        assertEquals("修改成功", updateResponse.getMessage());
    }


}
