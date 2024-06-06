package com.jike.personnel.controller;


import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.personnel.domain.dto.*;
import com.jike.personnel.domain.po.Application;
import com.jike.personnel.domain.po.Department;
import com.jike.personnel.domain.po.Employee;
import com.jike.personnel.service.IApplicationService;
import com.jike.personnel.service.IDepartmentService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.web.bind.annotation.*;

import java.util.Date;
import java.util.List;
import java.util.stream.Collectors;

@Api(tags = "申请相关接口")
@RestController
@RequestMapping("api/Application")
@RequiredArgsConstructor
@CrossOrigin

public class ApplicationController {

    private final IApplicationService applicationService;

    @ApiOperation("通过员工id查")
    @GetMapping("GetByEmpId/{empId}")
    public ResponseResult<?> getApplicationByEmpId(@Param("empId") @PathVariable("empId") int id) {
        QueryWrapper<Application> queryWrapper = new QueryWrapper<Application>()
                .eq("EMPLOYEE_ID", id);
        List<Application> applications = applicationService.list(queryWrapper);
        if (applications != null && !applications.isEmpty()) {
            List<ApplicationDTO> applicationDTOs = applications.stream()
                    .map(application -> BeanUtils.copyBean(application, ApplicationDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(applicationDTOs);
        }
        return ResponseResult.fail("找不到申请");
    }

    @ApiOperation("查找所有申请")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<Application> applications = applicationService.list();
        if (CollUtils.isEmpty(applications)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(applications, ApplicationDTO.class));
    }

    @ApiOperation("通过申请类型查")
    @GetMapping("GetByType/{appType}")
    public ResponseResult<?> getApplicationByType(@Param("申请类型") @PathVariable("appType") String type) {
        QueryWrapper<Application> queryWrapper = new QueryWrapper<Application>()
                .eq("APPLICATION_TYPE", type);
        List<Application> applications = applicationService.list(queryWrapper);
        if (applications != null && !applications.isEmpty()) {
            List<ApplicationDTO> applicationDTOs = applications.stream()
                    .map(application -> BeanUtils.copyBean(application, ApplicationDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(applicationDTOs);
        }
        return ResponseResult.fail("找不到申请");
    }

    @ApiOperation("通过申请时间查")
    @GetMapping("GetByApplicationTimeOfDay/{appTime}")
    public ResponseResult<?> getApplicationByTime(@Param("申请时间") @PathVariable("appTime")
                                                                      @DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME) Date time) {
        QueryWrapper<Application> queryWrapper = new QueryWrapper<Application>()
                .eq("APPLICATION_TIME", time);
        List<Application> applications = applicationService.list(queryWrapper);
        if (applications != null && !applications.isEmpty()) {
            List<ApplicationDTO> applicationDTOs = applications.stream()
                    .map(application -> BeanUtils.copyBean(application, ApplicationDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(applicationDTOs);
        }
        return ResponseResult.fail("找不到申请");
    }


    @ApiOperation("新建申请")
    @PostMapping("Add")
    public ResponseResult<?> addApplication(@RequestBody NewApplicationDTO newApplicationDTO) {
        Application application = BeanUtils.copyBean(newApplicationDTO, Application.class);
        if (applicationService.save(application))
            return ResponseResult.success(BeanUtils.copyBean(application, ApplicationDTO.class));
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("更新申请信息")
    @PutMapping("Update")
    public ResponseResult<?> updateApplication(@RequestBody ApplicationDTO applicationDTO) {
        Application application = BeanUtils.copyBean(applicationDTO, Application.class);
        if (applicationService.updateById(application))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }


    @ApiOperation("更新申请信息(申请是否通过)")
    @PutMapping("Update1")
    public ResponseResult<?> update1Application(@RequestBody IfApplicationDTO applicationDTO) {
        QueryWrapper<Application> queryWrapper = new QueryWrapper<Application>()
                .eq("APPLICATION_ID", applicationDTO.getApplicationId());
        Application application = applicationService.getOne(queryWrapper, false);
        application.setIfApprove(applicationDTO.getIfApprove());
        application.setApplicationContent(applicationDTO.getApplicationContent());
        application.setApplicationTime(applicationDTO.getApplicationTime());
        application.setApplicationType(applicationDTO.getApplicationType());
        application.setEmployeeId(applicationDTO.getEmployeeId());
        if (applicationService.updateById(application))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("通过申请id删除申请")
    @DeleteMapping("/Delete/{applicationId}")
    public ResponseResult<Void> deleteApplicationById(@PathVariable("applicationId") int applicationId) {
        boolean isDeleted = applicationService.removeById(applicationId);
        if (isDeleted) {
            return ResponseResult.success("成功删除");

        }
        return ResponseResult.fail("删除申请失败");
    }

}
