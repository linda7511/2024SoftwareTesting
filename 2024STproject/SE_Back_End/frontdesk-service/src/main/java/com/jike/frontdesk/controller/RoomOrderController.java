package com.jike.frontdesk.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.NewConsumptionRecordDTO;
import com.jike.frontdesk.domain.dto.NewRoomOrderDTO;
import com.jike.frontdesk.domain.dto.RoomOrderDTO;
import com.jike.frontdesk.domain.dto.RoomOrderUpdateDTO;
import com.jike.frontdesk.domain.po.ConsumptionRecord;
import com.jike.frontdesk.domain.po.Room;
import com.jike.frontdesk.domain.po.RoomOrder;
import com.jike.frontdesk.domain.po.RoomType;
import com.jike.frontdesk.service.IRoomOrderService;
import com.jike.frontdesk.service.IRoomTypeService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Api(tags = "房间订单相关接口")
@RestController
@RequestMapping("api/Roomorder")
@RequiredArgsConstructor
@CrossOrigin
public class RoomOrderController {
    private final IRoomOrderService roomOrderService;
    private final IRoomTypeService roomTypeService;
    @ApiOperation("查找所有房间订单")
    @GetMapping("GetAll")
    public ResponseResult<List<RoomOrderDTO>> getAll() {
        List<RoomOrder> roomOrders = roomOrderService.list();
        if (CollUtils.isEmpty(roomOrders)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(roomOrders, RoomOrderDTO.class));
    }

    @ApiOperation("根据顾客id查找房间订单")
    @GetMapping("GetByCustomerId/{customerId}")
    public ResponseResult<List<RoomOrderDTO>> getRoomOrderByCustomerId(@Param("customerId") @PathVariable("customerId") int customerId) {
        QueryWrapper<RoomOrder> queryWrapper = new QueryWrapper<RoomOrder>()
                .eq("CUSTOMER_ID", customerId);
        List<RoomOrder> roomOrders = roomOrderService.list(queryWrapper);
        if (CollUtils.isEmpty(roomOrders)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(roomOrders, RoomOrderDTO.class));
    }

    @ApiOperation("更新房间订单")
    @PutMapping("Update")
    public ResponseResult<String> updateRoomOrder(@RequestBody RoomOrderUpdateDTO roomOrderDTO) {
        RoomOrder roomOrder = roomOrderService.getById(roomOrderDTO.getOrderId());
        roomOrder.setOrderStatus(roomOrderDTO.getOrderStatus());
        if (roomOrderService.updateById(roomOrder))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }
    @ApiOperation("新建订单信息")
    @PostMapping("Add")
    public ResponseResult<?> addRoomOrder(@RequestBody NewRoomOrderDTO newRoomOrderDTO) {
        QueryWrapper<RoomType> queryWrapper = new QueryWrapper<RoomType>()
                .eq("TYPE_NAME", newRoomOrderDTO.getTypeName());
        RoomType roomType = roomTypeService.getOne(queryWrapper,false);
        RoomOrder roomOrder = BeanUtils.copyBean(newRoomOrderDTO, RoomOrder.class);
        roomOrder.setTypeId(roomType.getTypeId());
        if (roomOrderService.save(roomOrder))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }
}
