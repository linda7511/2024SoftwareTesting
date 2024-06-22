package com.jike.canteen.service.impl;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.github.jeffreyning.mybatisplus.service.MppServiceImpl;
import com.jike.canteen.domain.dto.CateringRecordDTO;
import com.jike.canteen.domain.dto.MyOrderDTO;
import com.jike.canteen.domain.dto.OrderDishDTO;
import com.jike.canteen.domain.dto.TempOrderDTO;
import com.jike.canteen.domain.po.MyOrder;
import com.jike.canteen.domain.po.MyTable;
import com.jike.canteen.mapper.DishMapper;
import com.jike.canteen.mapper.MyOrderMapper;
import com.jike.canteen.mapper.MyTableMapper;

import com.jike.canteen.service.FrontdeskClient;
import com.jike.canteen.service.IMyOrderService;
import com.jike.canteen.service.IMyTableService;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Service
public class MyOrderServiceImpl extends MppServiceImpl<MyOrderMapper, MyOrder> implements IMyOrderService {

    private final MyTableMapper myTableService;
    private final DishMapper myDishMapper;
    private final MyOrderMapper myOrderMapper;
    private final FrontdeskClient frontdeskClient;

    public MyOrderServiceImpl(MyTableMapper myTableService, DishMapper myDishMapper, MyOrderMapper myOrderMapper, FrontdeskClient frontdeskClient) {
        this.myTableService = myTableService;
        this.myDishMapper = myDishMapper;
        this.myOrderMapper = myOrderMapper;
        this.frontdeskClient = frontdeskClient;
    }

    private boolean isValidOrderStatus(String orderStatus) {
        return "点单成功".equals(orderStatus) || "点单失败".equals(orderStatus);
    }


    public ResponseResult<String> addMyOrder(@RequestBody MyOrderDTO myOrderDTO) {
        if (myOrderDTO.getTableId() == 0 || myOrderDTO.getDishId() == 0 || myOrderDTO.getOrderTime() == null || !isValidOrderStatus(myOrderDTO.getOrderStatus())) {
            return ResponseResult.fail("新增失败");
        }


        // Check if dishId exists
        if (myDishMapper.selectById(myOrderDTO.getDishId()) == null ||
                myTableService.selectById(myOrderDTO.getTableId()) == null) {
            return ResponseResult.fail("新增失败：外键不存在");
        }

        MyOrder myOrder = BeanUtils.copyBean(myOrderDTO, MyOrder.class);
        if (myOrderMapper.insert(myOrder) > 0) {
            return ResponseResult.success("新增成功");
        }
        return ResponseResult.fail("新增失败");
    }

    public ResponseResult<String> updateMyOrder(@RequestBody MyOrderDTO myOrderDTO) {

        if (myOrderDTO.getTableId() == 0 || myOrderDTO.getDishId() == 0 || myOrderDTO.getOrderTime() == null || !isValidOrderStatus(myOrderDTO.getOrderStatus())) {
            return ResponseResult.fail("修改失败");
        }

        MyOrder myOrder = BeanUtils.copyBean(myOrderDTO, MyOrder.class);
        if (myOrderMapper.updateByMultiId(myOrder) > 0) {
            return ResponseResult.success("修改成功");
        }
        return ResponseResult.fail("修改失败");
    }

    public ResponseResult<?> getOrderAndDishInfo(@PathVariable("tableId") int tableId) {
        List<OrderDishDTO> orderDishInfos = myOrderMapper.selectOrderAndDishInfoByTableId(tableId);
        if (orderDishInfos != null && !orderDishInfos.isEmpty()) {
            return ResponseResult.success(orderDishInfos);
        } else {
            return ResponseResult.fail("没有找到对应桌位的订单和菜品信息");
        }
    }

    public ResponseResult<?> updateConsumpId(@PathVariable("consumptionRecordId") int consumptionRecordId, @RequestBody String orderMessages) throws JsonProcessingException {
        ObjectMapper objectMapper = new ObjectMapper();
        JsonNode jsonNode = objectMapper.readTree(orderMessages);
        List<TempOrderDTO> tempOrderDTOList = new ArrayList<>();

        for (JsonNode node : jsonNode) {
            int tableId = node.get("tableId").asInt();
            int dishId = node.get("dishId").asInt();
            String orderTime = node.get("orderTime").asText();
            TempOrderDTO dto = new TempOrderDTO();
            dto.setDishId(dishId);
            dto.setOrderTime(LocalDateTime.parse(orderTime));
            dto.setTableId(tableId);
            tempOrderDTOList.add(dto);
        }

        for (TempOrderDTO tempOrderDTO : tempOrderDTOList) {
            QueryWrapper<MyOrder> queryWrapper = new QueryWrapper<>();
            queryWrapper.eq("DISH_ID", tempOrderDTO.getDishId())
                    .eq("TABLE_ID", tempOrderDTO.getTableId())
                    .ge("ORDER_TIME", tempOrderDTO.getOrderTime())
                    .le("ORDER_TIME", tempOrderDTO.getOrderTime().plusSeconds(1));
            MyOrder myOrder = myOrderMapper.selectOne(queryWrapper);
            myOrder.setConsumptionRecordId(consumptionRecordId);
            if (myOrderMapper.updateByMultiId(myOrder) <= 0)
                return ResponseResult.fail("修改失败");
        }

        return ResponseResult.success("修改成功");
    }

    public ResponseResult<String> addCateringRecord(@RequestBody CateringRecordDTO cateringRecordDTO) {
        int tableId = cateringRecordDTO.getTableId();
        MyTable myTable = myTableService.selectById(tableId);
        myTable.setTableStatus("空闲");
        myTableService.updateById(myTable);
        int consumptionRecordId = frontdeskClient.addCateringRecord(cateringRecordDTO.getRoomNumber(), cateringRecordDTO.getConsumeAmount());
        for (TempOrderDTO tempOrderDTO : cateringRecordDTO.getOrderMessages()) {
            QueryWrapper<MyOrder> queryWrapper = new QueryWrapper<>();
            queryWrapper.eq("DISH_ID", tempOrderDTO.getDishId())
                    .eq("TABLE_ID", tempOrderDTO.getTableId())
                    .ge("ORDER_TIME", tempOrderDTO.getOrderTime())
                    .le("ORDER_TIME", tempOrderDTO.getOrderTime().plusSeconds(1));
            MyOrder myOrder = myOrderMapper.selectOne(queryWrapper);
            myOrder.setConsumptionRecordId(consumptionRecordId);
            if (myOrderMapper.updateByMultiId(myOrder) <= 0)
                return ResponseResult.fail("修改失败");
        }
        return ResponseResult.success("新增成功");
    }
}

