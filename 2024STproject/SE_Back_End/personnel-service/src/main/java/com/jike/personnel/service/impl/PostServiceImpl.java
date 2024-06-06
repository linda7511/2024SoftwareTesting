package com.jike.personnel.service.impl;


import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.personnel.domain.po.Employee;
import com.jike.personnel.domain.po.Post;
import com.jike.personnel.mapper.EmployeeMapper;
import com.jike.personnel.mapper.PostMapper;
import com.jike.personnel.service.IEmployeeService;
import com.jike.personnel.service.IPostService;
import org.springframework.stereotype.Service;

@Service
public class PostServiceImpl extends ServiceImpl<PostMapper, Post> implements IPostService {

}
