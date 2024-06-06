package com.jike.personnel.service.impl;


import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.personnel.domain.po.Application;
import com.jike.personnel.domain.po.Department;
import com.jike.personnel.mapper.ApplicationMapper;
import com.jike.personnel.mapper.DepartmentMapper;
import com.jike.personnel.service.IApplicationService;
import com.jike.personnel.service.IDepartmentService;
import org.springframework.stereotype.Service;

@Service
public class ApplicationServiceImpl extends ServiceImpl<ApplicationMapper, Application> implements IApplicationService {


}
