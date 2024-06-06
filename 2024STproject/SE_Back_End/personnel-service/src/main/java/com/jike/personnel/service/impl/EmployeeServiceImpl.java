package com.jike.personnel.service.impl;


import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.personnel.domain.po.Department;
import com.jike.personnel.domain.po.Employee;
import com.jike.personnel.mapper.DepartmentMapper;
import com.jike.personnel.mapper.EmployeeMapper;
import com.jike.personnel.service.IDepartmentService;
import com.jike.personnel.service.IEmployeeService;
import org.springframework.stereotype.Service;

@Service
public class EmployeeServiceImpl extends ServiceImpl<EmployeeMapper, Employee> implements IEmployeeService {

}
