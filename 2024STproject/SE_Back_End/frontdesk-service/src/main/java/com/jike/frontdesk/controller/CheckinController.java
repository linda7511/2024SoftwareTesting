package com.jike.frontdesk.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.CustomerDTO;
import com.jike.frontdesk.domain.dto.CheckinDTO;
import com.jike.frontdesk.domain.po.Checkin;
import com.jike.frontdesk.domain.po.Customer;
import com.jike.frontdesk.domain.po.Room;
import com.jike.frontdesk.domain.vo.CustomerVO;
import com.jike.frontdesk.service.ICheckinService;
import com.jike.frontdesk.service.IRoomService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.List;

@Api(tags = "入住相关接口")
@RestController
@RequestMapping("api/Checkin")
@RequiredArgsConstructor
@CrossOrigin
public class CheckinController {
    private final ICheckinService checkinService;
    private final IRoomService roomService;

    @ApiOperation("新建入住信息")
    @PostMapping("Add")
    public ResponseResult<String> addCheckinInfo(@RequestBody CheckinDTO CheckinDTO) {
        Checkin checkin = BeanUtils.copyBean(CheckinDTO, Checkin.class);
        if (checkinService.save(checkin))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("更新入住信息")
    @PutMapping("Update")
    public ResponseResult<String> updateCheckinInfo(@RequestBody CheckinDTO CheckinDTO) {
        Checkin checkin = BeanUtils.copyBean(CheckinDTO, Checkin.class);
        if (checkinService.updateByMultiId(checkin))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("通过顾客id和入住时间查")
    @GetMapping("GetByCustomerIdAndInTime/{customerId}/{inTime}")
    public ResponseResult<?> getCheckinInfoByCustomerIdAndInTime(@Param("顾客id") @PathVariable("customerId") int customerId,
                                                      @Param("入住时间") @PathVariable("inTime") String inTime) {
        QueryWrapper<Checkin> queryWrapper = new QueryWrapper<Checkin>()
                .eq("CUSTOMER_ID", customerId)
                .eq("CHECKIN_TIME", inTime);
        Checkin checkin = checkinService.getOne(queryWrapper, false);
        if (checkin != null) {
            return ResponseResult.success(BeanUtils.copyBean(checkin, CheckinDTO.class));
        }
        return ResponseResult.fail("找不到入住信息");
    }

    @ApiOperation("通过房间号查（若干条）")
    @GetMapping("GetByRoomNum/{roomNumber}")
    public ResponseResult<?> getCheckinInfoByRoomNum(@Param("房间号") @PathVariable("roomNumber") int roomNumber) {
        QueryWrapper<Room> queryWrapper = new QueryWrapper<Room>()
                .eq("ROOM_NUMBER", roomNumber);
        Room room = roomService.getOne(queryWrapper, false);
        if (room != null) {
            QueryWrapper<Checkin> queryWrapper2 = new QueryWrapper<Checkin>()
                    .eq("ROOM_ID", room.getRoomId());
            List<Checkin> checkinList = checkinService.list(queryWrapper2);
            return ResponseResult.success(BeanUtils.copyList(checkinList, CheckinDTO.class));
        }
        return ResponseResult.fail("找不到入住信息");
    }
}
