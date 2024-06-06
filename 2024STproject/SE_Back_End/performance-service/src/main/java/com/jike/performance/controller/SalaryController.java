package com.jike.performance.controller;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.performance.domain.dto.AttendenceInformationDTO;
import com.jike.performance.domain.dto.NewSalaryDTO;
import com.jike.performance.domain.dto.SalaryDTO;
import com.jike.performance.domain.po.AttendenceInformation;
import com.jike.performance.domain.po.Salary;
import com.jike.performance.service.IAttendenceInformationService;
import com.jike.performance.service.ISalaryService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@Api(tags = "工资相关接口")
@RestController
@RequestMapping("api/Salary")
@RequiredArgsConstructor
@CrossOrigin
public class SalaryController {
    private final ISalaryService salaryService;

    @ApiOperation("通过工资id查")
    @GetMapping("GetByEmployeeId/{empId}")
    public ResponseResult<?> getSalaryByEmployeeId (@Param("employee_id") @PathVariable("empId") int emp_Id) {
        QueryWrapper<Salary> queryWrapper = new QueryWrapper<Salary>()
                .eq("EMPLOYEE_ID", emp_Id);
        List<Salary> salaries = salaryService.list(queryWrapper);
        if (salaries != null && !salaries.isEmpty()) {
            List<SalaryDTO> dtoList = salaries.stream()
                    .map(info -> BeanUtils.copyBean(info, SalaryDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(dtoList);
        }
        return ResponseResult.fail("找不到工资信息");
    }


    @ApiOperation("通过工资id删除工资记录")
    @DeleteMapping("/Delete/{assetsId}")
    public ResponseResult<?> deleteSalaryById(@PathVariable("assetsId") int salaryId) {
        boolean isDeleted = salaryService.removeById(salaryId);
        if (isDeleted) {
            return ResponseResult.success("成功删除");

        }
        return ResponseResult.fail("删除工资记录失败");
    }

    @ApiOperation("更新工资信息")
    @PutMapping("Update")
    public ResponseResult<?> updateSalary(@RequestBody SalaryDTO salaryDTO) {
        Salary salary = BeanUtils.copyBean(salaryDTO, Salary.class);
        if (salaryService.updateById(salary))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("新建工资信息")
    @PostMapping("Add")
    public ResponseResult<?> addSalary(@RequestBody NewSalaryDTO newSalaryDTO) {
        Salary salary = BeanUtils.copyBean(newSalaryDTO, Salary.class);
        if (salaryService.save(salary))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }


}
