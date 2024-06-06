package com.jike.frontdesk.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.CateringRecordDTO;
import com.jike.frontdesk.domain.dto.NewConsumptionRecordDTO;
import com.jike.frontdesk.domain.po.ConsumptionRecord;
import com.jike.frontdesk.domain.po.Room;
import com.jike.frontdesk.mapper.ConsumptionRecordMapper;
import com.jike.frontdesk.service.IConsumptionRecordService;
import com.jike.frontdesk.service.IRoomService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDateTime;
import java.util.List;

@Api(tags = "消费记录相关接口")
@RestController
@RequestMapping("api/ConsumptionRecord")
@RequiredArgsConstructor
@CrossOrigin
public class ConsumptionRecordController {
    private final IConsumptionRecordService consumptionRecordService;
    private final ConsumptionRecordMapper consumptionRecordMapper;
    private final IRoomService roomService;

    @ApiOperation("新建消费记录信息")
    @PostMapping("Add")
    public ResponseResult<String> addConsumptionRecord(@RequestBody NewConsumptionRecordDTO newConsumptionRecordDTO) {
        ConsumptionRecord consumptionRecord = BeanUtils.copyBean(newConsumptionRecordDTO, ConsumptionRecord.class);
        if (consumptionRecordService.save(consumptionRecord))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("通过房间号查")
    @GetMapping("GetByRoomNum/{roomNum}")
    public ResponseResult<?> getByRoomNum(@PathVariable("roomNum") int roomNum) {
        try {
            List<ConsumptionRecord> records = consumptionRecordMapper.getByRoomNum(roomNum);
            if (records != null && !records.isEmpty()) {
                return ResponseResult.success(records);
            } else {
                return ResponseResult.fail("没有找到对应房间号的消费记录");
            }
        } catch (Exception e) {
            // 异常处理
            return ResponseResult.fail("查询失败: " + e.getMessage());
        }
    }

    @ApiOperation("通过身份证号查")
    @GetMapping("GetByID/{ID}")
    public ResponseResult<?> getByID(@PathVariable("ID") String id) {
        try {
            List<ConsumptionRecord> records = consumptionRecordMapper.getByID(id);
            if (records != null && !records.isEmpty()) {
                return ResponseResult.success(records);
            } else {
                return ResponseResult.fail("没有找到对应身份证号的消费记录");
            }
        } catch (Exception e) {
            // 异常处理
            return ResponseResult.fail("查询失败: " + e.getMessage());
        }
    }

    @ApiOperation("挂账")
    @PostMapping("AddCateringRecord")
    public int addCateringRecord(@RequestParam int roomNumber,@RequestParam double ConsumeAmount) {
        ConsumptionRecord consumptionRecord = new ConsumptionRecord();

        QueryWrapper queryWrapper = new QueryWrapper<>();
        queryWrapper.eq("ROOM_NUMBER",roomNumber);
        Room room = roomService.getOne(queryWrapper);

        consumptionRecord.setRoomId(room.getRoomId());
        consumptionRecord.setConsumeAmount(ConsumeAmount);
        consumptionRecord.setConsumeType("餐饮费");
        consumptionRecord.setConsumeTime(LocalDateTime.now());

        // table 更改为空闲状态
        //cateringRecordDTO.getTableId();

        // myorder中的ConsumptionRecordId也要更新

        consumptionRecordService.save(consumptionRecord);

        return consumptionRecord.getConsumeId();
    }

}
