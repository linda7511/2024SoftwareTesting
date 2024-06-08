package com.jike.canteen.service.impl;

import com.jike.canteen.controller.CombinedController;
import com.jike.canteen.domain.dto.*;
import com.jike.canteen.service.IBookService;
import com.jike.canteen.service.IMyOrderService;
import com.jike.canteen.service.IMyTableService;
import com.jike.common.utils.ResponseResult;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.Rollback;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;

import com.jike.canteen.service.IDishService;

import static org.junit.jupiter.api.Assertions.assertEquals;

import io.qameta.allure.Step;

@Transactional
@Rollback
@SpringBootTest
public class UnitTest {

    @Autowired
    private IDishService dishService;
    @Autowired
    private IBookService bookService;
    @Autowired
    private CombinedController combinedController;
    @Autowired
    private IMyOrderService myOrderService;
    @Autowired
    private IMyTableService myTableService;


    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyOrder/MyOrderAddSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试添加订单成功")
    public void addMyOrder_Success(int tableId,int dishId,int consumption_recordId,
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
    public void addMyOrder_F1(Integer tableId,Integer dishId,Integer consumption_recordId,
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
    public void addMyOrder_F2(Integer tableId,Integer dishId,Integer consumption_recordId,
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
    public void addMyOrder_F3(Integer tableId,Integer dishId,Integer consumption_recordId,
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
    public void updateMyOrderInfo_f1(Integer tableId,Integer dishId,Integer consumption_recordId,
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
    public void updateMyOrderInfo_f2(Integer tableId,Integer dishId,Integer consumption_recordId,
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
    public void updateMyOrderInfo_f3(Integer tableId,Integer dishId,Integer consumption_recordId,
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
    public void updateMyOrderInfo_success(Integer tableId,Integer dishId,Integer consumption_recordId,
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





    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableAddSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试添加桌位成功")
    public void addMyTable_Success(int capacity,String tabletype,String tablelocation,
                                   String tableStatus,String note,int bookable, int available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增成功", response.getMessage());
    }


    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableAddF1.csv", numLinesToSkip = 1)
    @DisplayName("测试添加桌位失败，参数为空")
    public void addMyTable_F1(Integer capacity,String tabletype,String tablelocation,
                              String tableStatus,String note,Integer bookable, Integer available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增失败", response.getMessage());
    }


    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableAddF2.csv", numLinesToSkip = 1)
    @DisplayName("测试添加桌位信息失败，tablestatus非法")
    public void addMyTable_F2(Integer capacity,String tabletype,String tablelocation,
                              String tableStatus,String note,Integer bookable, Integer available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableAddF3.csv", numLinesToSkip = 1)
    @DisplayName("测试添加桌位信息失败，tabletype非法")
    public void addMyTable_F3(Integer capacity,String tabletype,String tablelocation,
                              String tableStatus,String note,Integer bookable, Integer available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增失败", response.getMessage());
    }




    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateF1.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位失败，主键冲突")
    public void updateMyTableInfo_f1(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                     String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateF2.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位失败，tablestatus非法")
    public void updateMyTableInfo_f2(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                     String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateF3.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位失败，tabletype非法")
    public void updateMyTableInfo_f3(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                     String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateF4.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位失败，参数为空")
    public void updateMyTableInfo_f4(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                     String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位信息成功")
    public void updateMyTableInfo_success(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                          String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改成功", updateResponse.getMessage());
    }

    // @Step("测试删除存在的桌位 [{tableId}]")
    // @ParameterizedTest
    // @CsvFileSource(resources = "/TableDeleteSuccess.csv", numLinesToSkip = 1)
    // @DisplayName("测试删除存在的桌位")
    // public void deleteTableById_Success(int tableId) {
    //     ResponseResult<Void> response = myTableService.deleteTableById(tableId);
    //     assertEquals("成功删除", response.getMessage());
    // }

    @Step("测试删除不存在的桌位 [{TableId}]")
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/TableDeleteF1.csv", numLinesToSkip = 1)
    @DisplayName("删除桌位失败，桌位不存在")
    public void deleteTableById_F1(int tableId) {
        ResponseResult<Void> response = combinedController.deleteTableById(tableId);
        assertEquals("删除桌位失败", response.getMessage());
    }

    @Step("测试删除存在外键约束的桌位 [{dishId}]")
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/TableDeleteF2.csv", numLinesToSkip = 1)
    @DisplayName("删除桌位失败，存在外键约束")
    public void deleteTableById_F2(int tableId) {
        ResponseResult<Void> response = combinedController.deleteTableById(tableId);
        assertEquals("删除失败：该桌位存在关联订单，无法删除", response.getMessage());
    }

}
