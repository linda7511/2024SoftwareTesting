package com.jike.logistics.service.impl;

import com.github.jeffreyning.mybatisplus.service.MppServiceImpl;
import com.jike.logistics.domain.po.Maintain;
import com.jike.logistics.mapper.MaintainMapper;
import com.jike.logistics.service.IMaintainService;
import org.springframework.stereotype.Service;

/**
 * <p>
 *  服务实现类
 * </p>
 *
 * @author 杨昕迪
 * @since 2023-01-02
 */
@Service
public class MaintainServiceImpl extends MppServiceImpl<MaintainMapper, Maintain> implements IMaintainService {
}
