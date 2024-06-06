package com.jike.frontdesk.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.CheckinDTO;
import com.jike.frontdesk.domain.dto.CleanDTO;
import com.jike.frontdesk.domain.dto.NewCleanDTO;
import com.jike.frontdesk.domain.po.Checkin;
import com.jike.frontdesk.domain.po.Clean;
import com.jike.frontdesk.domain.po.Room;
import com.jike.frontdesk.service.ICheckinService;
import com.jike.frontdesk.service.ICleanService;
import com.jike.frontdesk.service.IRoomService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

@Api(tags = "清洁相关接口")
@RestController
@RequestMapping("api/Cleaning")
@RequiredArgsConstructor
@CrossOrigin
public class CleanController {
    private final ICleanService cleanService;
    private final IRoomService roomService;

    @ApiOperation("新建清洁信息")
    @PostMapping("Add")
    public ResponseResult<String> addCleaningInfo(@RequestBody NewCleanDTO cleanDTO) {

        Clean clean = BeanUtils.copyBean(cleanDTO, Clean.class);
        QueryWrapper<Room> queryWrapper = new QueryWrapper<Room>()
                .eq("ROOM_NUMBER", cleanDTO.getRoomNumber());
        Room room = roomService.getOne(queryWrapper, false);

        clean.setRoomId(room.getRoomId());
        if (cleanService.save(clean))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("通过房间id和员工id删除")
    @GetMapping("Delete/{RoomId}/{EmpId}")
    public ResponseResult<?> deleteCleaningInfo(@Param("房间id") @PathVariable("RoomId") int roomId,
                                        @Param("员工id") @PathVariable("EmpId") int employeeId) {
        QueryWrapper<Clean> queryWrapper = new QueryWrapper<Clean>()
                .eq("ROOM_ID", roomId)
                .eq("EMPLOYEE_ID", employeeId);
        boolean isRemoved = cleanService.remove(queryWrapper);
        if (isRemoved) {
            return ResponseResult.success("清理信息删除成功");
        }
        return ResponseResult.fail("找不到对应的清理信息，删除失败");
    }

    @ApiOperation("通过房间id和员工id查找清理信息")
    @GetMapping("Find/{RoomId}/{EmpId}")
    public ResponseResult<?> findCleaningInfo(@PathVariable("RoomId") int roomId,
                                          @PathVariable("EmpId") int employeeId) {
        QueryWrapper<Clean> queryWrapper = new QueryWrapper<Clean>()
                .eq("ROOM_ID", roomId)
                .eq("EMPLOYEE_ID", employeeId);
        Clean clean = cleanService.getOne(queryWrapper, false);
        if (clean != null) {
            return ResponseResult.success(clean);
        }
        return ResponseResult.fail("找不到对应的清理信息");
    }

}
