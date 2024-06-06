package com.jike.frontdesk.service.impl;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.frontdesk.domain.po.RoomOrder;
import com.jike.frontdesk.mapper.RoomOrderMapper;
import com.jike.frontdesk.service.IRoomOrderService;
import org.springframework.stereotype.Service;

/**
 * <p>
 *  服务实现类
 * </p>
 *
 * @author 杨昕迪
 * @since 2023-12-28
 */
@Service
public class RoomOrderServiceImpl extends ServiceImpl<RoomOrderMapper, RoomOrder> implements IRoomOrderService {
}
