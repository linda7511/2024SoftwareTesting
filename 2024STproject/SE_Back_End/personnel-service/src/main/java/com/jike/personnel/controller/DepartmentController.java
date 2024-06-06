package com.jike.personnel.controller;


import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.personnel.domain.dto.DepartmentDTO;
import com.jike.personnel.domain.dto.EmployeeDTO;
import com.jike.personnel.domain.po.Department;
import com.jike.personnel.domain.po.Employee;
import com.jike.personnel.service.IDepartmentService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Api(tags = "部门相关接口")
@RestController
@RequestMapping("api/Department")
@RequiredArgsConstructor
@CrossOrigin
public class DepartmentController {

    private final IDepartmentService departmentService;

    @ApiOperation("通过主码查")
    @GetMapping("Get/{DepartmentId}")
    public ResponseResult<?> getDepartmentById(@Param("DepartmentId") @PathVariable("DepartmentId") int id) {
        Department department = departmentService.getById(id);
        if (department != null) {
            return ResponseResult.success(BeanUtils.copyBean(department, DepartmentDTO.class));
        }
        return ResponseResult.fail("找不到部门");
    }
    @ApiOperation("查找所有部门")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<Department> departments = departmentService.list();
        if (CollUtils.isEmpty(departments)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(departments, DepartmentDTO.class));
    }

    @ApiOperation("通过部门名称查")
    @GetMapping("GetByName/{DepartmentName}")
    public ResponseResult<?> getApplicationByName(@Param("部门名称") @PathVariable("DepartmentName") String name) {
        QueryWrapper<Department> queryWrapper = new QueryWrapper<Department>()
                .eq("DEPARTMENT_NAME", name);
        Department department = departmentService.getOne(queryWrapper, false);
        if (department != null) {
            return ResponseResult.success(BeanUtils.copyBean(department, DepartmentDTO.class));
        }
        return ResponseResult.fail("找不到部门");
    }

}
