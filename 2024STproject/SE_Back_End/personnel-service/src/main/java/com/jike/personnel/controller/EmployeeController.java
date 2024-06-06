package com.jike.personnel.controller;


import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.Md5Util;
import com.jike.common.utils.ResponseResult;
import com.jike.personnel.config.JwtProperties;
import com.jike.personnel.domain.dto.EmployeeDTO;
import com.jike.personnel.domain.dto.EmployeeDetailsDTO;
import com.jike.personnel.domain.dto.NewEmployeeDTO;
import com.jike.personnel.domain.po.Employee;
import com.jike.personnel.domain.vo.LoginVO;
import com.jike.personnel.domain.vo.PwdVO;
import com.jike.personnel.mapper.EmployeeMapper;
import com.jike.personnel.service.IEmployeeService;
import com.jike.personnel.util.JwtTool;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;

@Api(tags = "员工相关接口")
@RestController
@RequestMapping("api/Employee")
@RequiredArgsConstructor
@CrossOrigin
public class EmployeeController {

    private final IEmployeeService employeeService;
    private final JwtTool jwtTool;
    private final JwtProperties jwtProperties;
    private final EmployeeMapper employeeMapper;

    @ApiOperation("查找所有员工")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<EmployeeDetailsDTO> employeeDetailsDTOS = employeeMapper.selectAllEmployeeDetails();
        if (employeeDetailsDTOS != null && !employeeDetailsDTOS.isEmpty()) {
            return ResponseResult.success(employeeDetailsDTOS);
        } else {
            return ResponseResult.fail("请求出错");
        }
    }

    @ApiOperation("新建员工")
    @PostMapping("Add")
    public ResponseResult<?> addEmployee(@RequestBody NewEmployeeDTO newEmployeeDTO) {
        Employee employee = BeanUtils.copyBean(newEmployeeDTO, Employee.class);
        System.out.println(employee.getPassword());
        employee.setPassword(Md5Util.getMD5String(employee.getPassword()));
        if (employeeService.save(employee)) {
            employeeMapper.updateDepartmentPeopleCount(newEmployeeDTO.getPostId());
            return ResponseResult.success("新增成功");
        }
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("更新员工信息")
    @PutMapping("Update")
    public ResponseResult<?> updateEmployee(@RequestBody EmployeeDTO employeeDTO) {
        Employee employee = BeanUtils.copyBean(employeeDTO, Employee.class);
        if (employeeService.updateById(employee))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("通过主码查")
    @GetMapping("GetByEmpId/{EmployeeId}")
    public ResponseResult<?> getEmployeeById(@Param("EmployeeId") @PathVariable("EmployeeId") int id) {
        Employee employee = employeeService.getById(id);
        if (employee != null) {
            return ResponseResult.success(BeanUtils.copyBean(employee, EmployeeDTO.class));
        }
        return ResponseResult.fail("找不到员工");
    }

    @ApiOperation("登录")
    @PostMapping("/Login/{account}/{password}")
    public ResponseResult<?> register(@PathVariable String account, @PathVariable String password) {

        // user
        QueryWrapper queryWrapper = new QueryWrapper<>();
        queryWrapper.eq("ACCOUNT", account);
        LoginVO vo = new LoginVO();
        Employee employee = employeeService.getOne(queryWrapper, false);
        if (employee == null) {
            vo.setStatusCode(-1);
            vo.setMessage("账号输入错误，无对应员工信息");
            return ResponseResult.fail(vo);
        } else if (!Md5Util.checkPassword(password, employee.getPassword())) {
            vo.setStatusCode(1);
            vo.setMessage("密码错误");
            return ResponseResult.fail(vo);
        } else if (Md5Util.checkPassword(password, employee.getPassword())) {
            EmployeeDTO dto = BeanUtils.copyBean(employee, EmployeeDTO.class);
            dto.setPassword("");
            String token = jwtTool.createToken(employee.getEmployeeId(), jwtProperties.getTokenTTL());
            vo.setToken(token);
            vo.setEmployee(dto);
            vo.setMessage("成功登录");
            vo.setDepartmentName(employeeMapper.selectDepartment(dto.getEmployeeId()));
            return ResponseResult.success(vo);
        }
        return ResponseResult.fail("密码错误");

    }

    @ApiOperation("修改密码")
    @PutMapping("UpdatePassword")
    public ResponseResult<?> changePassword(@RequestBody PwdVO pwdVO) {
        Employee employee = employeeService.getById(pwdVO.getEmployeeId());
        employee.setPassword(Md5Util.getMD5String(pwdVO.getPassword()));
        if (employeeService.updateById(employee))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }
}