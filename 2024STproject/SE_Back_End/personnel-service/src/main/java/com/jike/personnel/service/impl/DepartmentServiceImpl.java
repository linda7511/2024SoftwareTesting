package com.jike.personnel.service.impl;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.personnel.domain.po.Department;
import com.jike.personnel.mapper.DepartmentMapper;
import com.jike.personnel.service.IDepartmentService;
import org.springframework.stereotype.Service;


@Service
public class DepartmentServiceImpl extends ServiceImpl<DepartmentMapper, Department> implements IDepartmentService{

}
