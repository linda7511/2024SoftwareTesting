package com.jike.canteen.controller;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.jike.canteen.domain.dto.*;
import com.jike.canteen.service.*;
import com.jike.common.utils.ResponseResult;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Api(tags = "综合接口")
@RestController
@RequestMapping("api")
@RequiredArgsConstructor
@CrossOrigin
public class CombinedController {

    private final IDishService dishService;
    private final IBookService bookService;
    private final IMyOrderService myOrderService;
    private final IMyTableService myTableService;

    // 菜品相关接口
    @ApiOperation("通过菜品id删除菜品")
    @DeleteMapping("/Dish/Delete/{DishId}")
    public ResponseResult<Void> deleteDishById(@PathVariable("DishId") int dishId) {
        return dishService.deleteDishById(dishId);
    }

    @ApiOperation("查找所有菜品")
    @GetMapping("/Dish/GetAllDishes")
    public ResponseResult<?> getAllDishes() {
        return dishService.getAll();
    }

    @ApiOperation("更新菜品")
    @PutMapping("/Dish/Update")
    public ResponseResult<String> updateDish(@RequestBody DishDTO DishDTO) {
        return dishService.updateDish(DishDTO);
    }

    @ApiOperation("新建菜品")
    @PostMapping("/Dish/Add")
    public ResponseResult<String> addDish(@RequestBody NewDishDTO newDishDTO) {
        return dishService.addDish(newDishDTO);
    }

    // 预定相关接口
    @ApiOperation("新建预定信息")
    @PostMapping("/Book/Add")
    public ResponseResult<?> addBookInfo(@RequestBody BookDTO BookDTO) {
        return bookService.addBookInfo(BookDTO);
    }

    @ApiOperation("更新预定信息")
    @PutMapping("/Book/Update")
    public ResponseResult<?> updateBookInfo(@RequestBody BookDTO BookDTO) {
        return bookService.updateBookInfo(BookDTO);
    }

    @ApiOperation("通过顾客id和桌位id删除预定信息")
    @DeleteMapping("/Book/Delete/{TableId}/{CustomerId}")
    public ResponseResult<?> deleteBookInfo(@Param("桌位id") @PathVariable("TableId") int tableId,
                                            @Param("顾客id") @PathVariable("CustomerId") int customerId) {
        return bookService.deleteBookInfo(tableId, customerId);
    }

    @ApiOperation("查找所有预定信息")
    @GetMapping("/Book/GetAll")
    public ResponseResult<?> getAllBookInfo() {
        return bookService.getAll();
    }

    // 订单相关接口
    @ApiOperation("新建订单")
    @PostMapping("/MyOrder/Add")
    public ResponseResult<?> addMyOrder(@RequestBody MyOrderDTO myOrderDTO) {
        return myOrderService.addMyOrder(myOrderDTO);
    }

    @ApiOperation("更新订单")
    @PutMapping("/MyOrder/Update")
    public ResponseResult<?> updateMyOrder(@RequestBody MyOrderDTO myOrderDTO) {
        return myOrderService.updateMyOrder(myOrderDTO);
    }

    @ApiOperation("获取订单和菜品信息")
    @GetMapping("/MyOrder/GetOrderAndDishInfo/{TableId}")
    public ResponseResult<?> getOrderAndDishInfo(@PathVariable("TableId") int tableId) {
        return myOrderService.getOrderAndDishInfo(tableId);
    }

    @ApiOperation("更新消费记录ID")
    @GetMapping("/MyOrder/updateConsumpId")
    public ResponseResult<?> updateId(@RequestParam("ConsumptionRecordId") int consumptionRecordId,
                                      @RequestParam("OrderMessages") String orderMessages) throws JsonProcessingException {
        return myOrderService.updateConsumpId(consumptionRecordId, orderMessages);
    }

    @ApiOperation("新建餐饮记录")
    @PostMapping("/MyOrder/AddCateringRecord")
    public ResponseResult<String> addCateringRecord(@RequestBody CateringRecordDTO cateringRecordDTO) {
        return myOrderService.addCateringRecord(cateringRecordDTO);
    }

    // 桌位相关接口
    @ApiOperation("通过桌位id删除桌位")
    @DeleteMapping("/MyTable/Delete/{TableId}")
    public ResponseResult<Void> deleteTableById(@PathVariable("TableId") int tableId) {
        return myTableService.deleteTableById(tableId);
    }

    @ApiOperation("查找所有桌位")
    @GetMapping("/MyTable/GetAll")
    public ResponseResult<List<MyTableDTO>> getAllTables() {
        return myTableService.getAllTables();
    }

    @ApiOperation("更新桌位")
    @PutMapping("/MyTable/Update")
    public ResponseResult<String> updateTable(@RequestBody MyTableDTO myTableDTO) {
        return myTableService.updateTable(myTableDTO);
    }

    @ApiOperation("新建桌位")
    @PostMapping("/MyTable/Add")
    public ResponseResult<String> addTable(@RequestBody NewMyTableDTO newMyTableDTO) {
        return myTableService.addTable(newMyTableDTO);
    }

    @ApiOperation("更新桌位信息")
    @GetMapping("/MyTable/Free")
    public ResponseResult<String> freeTable(@RequestParam("TableId") int tableId) {
        return myTableService.freeTable(tableId);
    }
}
