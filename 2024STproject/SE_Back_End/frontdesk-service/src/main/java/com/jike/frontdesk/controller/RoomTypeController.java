package com.jike.frontdesk.controller;

import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.RoomTypeDTO;
import com.jike.frontdesk.domain.po.RoomType;
import com.jike.frontdesk.service.IRoomTypeService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

@Api(tags = "房间类型相关接口")
@RestController
@RequestMapping("api/RoomType")
@RequiredArgsConstructor
@CrossOrigin
public class RoomTypeController {
    private final IRoomTypeService roomTypeService;

    @ApiOperation("通过主码查")
    @GetMapping("Get/{id}")
    public ResponseResult<RoomTypeDTO> getRoomTypeById(@Param("id") @PathVariable("id") int id) {
        RoomType roomType = roomTypeService.getById(id);
        if (roomType != null) {
            return ResponseResult.success(BeanUtils.copyBean(roomType, RoomTypeDTO.class));
        }
        return ResponseResult.fail("找不到房间类型");
    }
}
