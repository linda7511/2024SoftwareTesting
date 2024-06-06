package com.jike.logistics.controller;

import cn.hutool.core.collection.CollUtil;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.logistics.domain.dto.ConsumeDTO;
import com.jike.logistics.domain.dto.MaintainDTO;
import com.jike.logistics.domain.dto.PurchaseDTO;
import com.jike.logistics.domain.po.Consume;
import com.jike.logistics.domain.po.Maintain;
import com.jike.logistics.domain.po.Purchase;
import com.jike.logistics.service.IMaintainService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Api(tags = "维护相关接口")
@RestController
@RequestMapping("api/Maintain")
@RequiredArgsConstructor
@CrossOrigin
public class MaintainController {
    private final IMaintainService maintainService;

    @ApiOperation("返回所有维修信息（若干条）")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<Maintain> maintainList = maintainService.list();
        if (CollUtil.isEmpty(maintainList)) {
            return ResponseResult.success();
        }
        return ResponseResult.success(BeanUtils.copyList(maintainList, MaintainDTO.class));
    }

    @ApiOperation("通过维护物体的名字查询")
    @GetMapping("GetByObject/{ObjectName}")
    public ResponseResult<?> getMaintainInfoByObjectName(@Param("ObjectName") @PathVariable("ObjectName") String itemName) {
        QueryWrapper<Maintain> queryWrapper = new QueryWrapper<Maintain>()
                .eq("MAINTAINING_ITEM", itemName);
        List<Maintain> maintainList = maintainService.list(queryWrapper);
        if (maintainList != null && !maintainList.isEmpty()) {
            List<MaintainDTO> maintainDTOList = maintainList.stream()
                    .map(maintain -> BeanUtils.copyBean(maintain, MaintainDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(maintainDTOList);
        }
        return ResponseResult.fail("找不到维护记录");
    }


    @ApiOperation("通过房间id查询")
    @GetMapping("GetByRoom/{RoomId}")
    public ResponseResult<?> getMaintainInfoByRoomId(@Param("RoomId") @PathVariable("RoomId") int id) {
        QueryWrapper<Maintain> queryWrapper = new QueryWrapper<Maintain>()
                .eq("ROOM_ID", id);
        List<Maintain> maintainList = maintainService.list(queryWrapper);
        if (maintainList != null && !maintainList.isEmpty()) {
            List<MaintainDTO> maintainDTOList = maintainList.stream()
                    .map(maintain -> BeanUtils.copyBean(maintain, MaintainDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(maintainDTOList);
        }
        return ResponseResult.fail("找不到维护记录");
    }


    @ApiOperation("新建维护记录信息")
    @PostMapping("Add")
    public ResponseResult<?> addMaintainInfo(@RequestBody MaintainDTO maintainDTO) {
        Maintain maintain = BeanUtils.copyBean(maintainDTO, Maintain.class);
        if (maintainService.save(maintain))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("更新维护信息")
    @PutMapping("Update")
    public ResponseResult<?> updateMaintainInfo(@RequestBody MaintainDTO maintainDTO) {
        Maintain maintain = BeanUtils.copyBean(maintainDTO, Maintain.class);
        if (maintainService.updateByMultiId(maintain))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("通过房间id和员工id和维护时间删除")
    @DeleteMapping("Delete/{RoomId}/{EmpId}/{MaintainTime}")
    public ResponseResult<?> deleteMaintainInfo(@Param("房间id") @PathVariable("RoomId") int roomId,
                                            @Param("员工id") @PathVariable("EmpId") int empId,
                                            @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) @Param("维修时间") @PathVariable("MaintainTime") LocalDateTime maintainTime) {
        QueryWrapper<Maintain> queryWrapper = new QueryWrapper<Maintain>()
                .eq("ROOM_ID", roomId)
                .eq("EMPLOYEE_ID", empId)
                .eq("MAINTAINING_TIME",maintainTime);
        boolean isRemoved = maintainService.remove(queryWrapper);
        if (isRemoved) {
            return ResponseResult.success("维护信息删除成功");
        }
        return ResponseResult.fail("找不到对应的维护信息，删除失败");
    }
}
