package com.jike.performance.service.impl;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.performance.domain.po.Salary;
import com.jike.performance.mapper.SalaryMapper;
import com.jike.performance.service.ISalaryService;
import org.springframework.stereotype.Service;


@Service
public class SalaryServiceImpl extends ServiceImpl<SalaryMapper,Salary> implements ISalaryService{
}
