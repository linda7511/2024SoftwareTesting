package com.jike.frontdesk.service.impl;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.frontdesk.domain.po.RoomType;
import com.jike.frontdesk.mapper.RoomTypeMapper;
import com.jike.frontdesk.service.IRoomTypeService;
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
public class RoomTypeServiceImpl extends ServiceImpl<RoomTypeMapper,RoomType> implements IRoomTypeService {
}
