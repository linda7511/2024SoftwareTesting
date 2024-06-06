package com.jike.logistics.service.impl;


import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.logistics.domain.po.Consume;
import com.jike.logistics.mapper.ConsumeMapper;
import com.jike.logistics.service.IConsumeService;
import org.springframework.stereotype.Service;

@Service
public class ConsumeServiceImpl extends ServiceImpl<ConsumeMapper, Consume> implements IConsumeService {
}
