package com.jike.frontdesk.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.CustomerDTO;
import com.jike.frontdesk.domain.dto.RoomDTO;
import com.jike.frontdesk.domain.dto.RoomUpdateDTO;
import com.jike.frontdesk.domain.po.Customer;
import com.jike.frontdesk.domain.po.Room;
import com.jike.frontdesk.service.IRoomService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Api(tags = "房间相关接口")
@RestController
@RequestMapping("api/Room")
@RequiredArgsConstructor
@CrossOrigin
public class RoomController {
    private final IRoomService roomService;

    @ApiOperation("通过主码查")
    @GetMapping("Get/{id}")
    public ResponseResult<RoomDTO> getRoomById(@Param("id") @PathVariable("id") int id) {
        Room room = roomService.getById(id);
        if (room != null) {
            return ResponseResult.success(BeanUtils.copyBean(room, RoomDTO.class));
        }
        return ResponseResult.fail("找不到房间");
    }

    @ApiOperation("通过房间号获取房间id")
    @GetMapping("GetRoomIdByRoomNum/{roomNumber}")
    public ResponseResult<Integer> getRoomIdByRoomNum(@Param("roomNumber") @PathVariable("roomNumber") int roomNumber) {
        QueryWrapper<Room> queryWrapper = new QueryWrapper<Room>()
                .eq("ROOM_NUMBER", roomNumber);
        Room room = roomService.getOne(queryWrapper, false);
        if (room != null) {
            return ResponseResult.success(room.getRoomId());
        }
        return ResponseResult.fail(-1);
    }


    @ApiOperation("更新房间")
    @PutMapping("Update")
    public ResponseResult<String> updateRoom(@RequestBody RoomUpdateDTO roomDTO) {

        Room room = roomService.getById(roomDTO.getRoomId());
        room.setStatus(roomDTO.getStatus());
        if (roomService.updateById(room))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("查找所有空房间")
    @GetMapping("GetEmptyRoomsAll")
    public ResponseResult<List<RoomDTO>> getEmptyRooms() {
        QueryWrapper<Room> queryWrapper = new QueryWrapper<Room>()
                .eq("STATUS", "空闲");
        List<Room> rooms = roomService.list(queryWrapper);
        if (CollUtils.isEmpty(rooms)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(rooms, RoomDTO.class));
    }

    @ApiOperation("根据类型查找空房间")
    @GetMapping("GetEmptyRoomsByType/{typeId}")
    public ResponseResult<List<RoomDTO>> getEmptyRoomsByType(@Param("typeId") @PathVariable("typeId") int typeId) {
        QueryWrapper<Room> queryWrapper = new QueryWrapper<Room>()
                .eq("TYPE_ID", typeId)
                .eq("STATUS", "空闲");
        List<Room> rooms = roomService.list(queryWrapper);
        if (CollUtils.isEmpty(rooms)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(rooms, RoomDTO.class));
    }
}
