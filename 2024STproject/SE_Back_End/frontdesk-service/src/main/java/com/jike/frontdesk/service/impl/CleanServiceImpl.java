package com.jike.frontdesk.service.impl;

import com.github.jeffreyning.mybatisplus.service.MppServiceImpl;
import com.jike.frontdesk.domain.po.Clean;
import com.jike.frontdesk.mapper.CleanMapper;
import com.jike.frontdesk.service.ICleanService;
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
public class CleanServiceImpl extends MppServiceImpl<CleanMapper, Clean> implements ICleanService {
}
