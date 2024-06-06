package com.jike.frontdesk.service.impl;

import com.github.jeffreyning.mybatisplus.service.MppServiceImpl;
import com.jike.frontdesk.domain.po.Pay;
import com.jike.frontdesk.mapper.PayMapper;
import com.jike.frontdesk.service.IPayService;
import org.springframework.stereotype.Service;

/**
 * <p>
 *  服务实现类
 * </p>
 *
 * @author 杨昕迪
 * @since 2023-12-26
 */
@Service
public class PayServiceImpl extends MppServiceImpl<PayMapper, Pay> implements IPayService {
}
