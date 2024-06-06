package com.jike.performance.controller;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.performance.domain.dto.AttendenceInformationDTO;
import com.jike.performance.domain.dto.NewAttendenceInformationDTO;
import com.jike.performance.domain.po.AttendenceInformation;
import com.jike.performance.service.IAttendenceInformationService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@Api(tags = "考勤信息相关接口")
@RestController
@RequestMapping("api/AttendanceInformation")
@RequiredArgsConstructor
@CrossOrigin
public class AttendanceInformationController {
    private final IAttendenceInformationService attendenceInformationService;


    @ApiOperation("通过员工id查")
    @GetMapping("GetByEmpId/{empId}")
    public ResponseResult<?> getAttendanceInformationByEmployeeId
            (@Param("employee_id") @PathVariable("empId") int emp_Id) {
        QueryWrapper<AttendenceInformation> queryWrapper = new QueryWrapper<AttendenceInformation>()
                .eq("EMPLOYEE_ID", emp_Id);
        List<AttendenceInformation> attendenceInformations = attendenceInformationService.list(queryWrapper);
        if (attendenceInformations != null && !attendenceInformations.isEmpty()) {
            List<AttendenceInformationDTO> dtoList = attendenceInformations.stream()
                    .map(info -> BeanUtils.copyBean(info, AttendenceInformationDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(dtoList);
        }
        return ResponseResult.fail("找不到考勤信息");
    }
    @ApiOperation("新建考勤信息")
    @PostMapping("Add")
    public ResponseResult<?> addAttendanceInformation(@RequestBody NewAttendenceInformationDTO newAttendenceInformationDTO) {
        AttendenceInformation attendenceInformation = BeanUtils.copyBean(newAttendenceInformationDTO, AttendenceInformation.class);
        if (attendenceInformationService.save(attendenceInformation))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("更新考勤信息")
    @PutMapping("Update")
    public ResponseResult<?> updateAttendanceInformation(@RequestBody AttendenceInformationDTO attendenceInformationDTO) {
        AttendenceInformation attendenceInformation = BeanUtils.copyBean(attendenceInformationDTO, AttendenceInformation.class);
        if (attendenceInformationService.updateById(attendenceInformation))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("通过考勤id删除考勤信息")
    @DeleteMapping("/Delete/{AttendanceId}")
    public ResponseResult<?> deleteAttendanceInformationById(@PathVariable("AttendanceId") int attendanceId) {
        boolean isDeleted = attendenceInformationService.removeById(attendanceId);
        if (isDeleted) {
            return ResponseResult.success("成功删除");

        }
        return ResponseResult.fail("删除考勤失败");
    }
}
