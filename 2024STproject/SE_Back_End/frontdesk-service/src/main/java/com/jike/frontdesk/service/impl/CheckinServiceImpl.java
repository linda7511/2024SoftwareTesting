package com.jike.frontdesk.service.impl;


import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.github.jeffreyning.mybatisplus.service.MppServiceImpl;
import com.jike.frontdesk.domain.po.Checkin;
import com.jike.frontdesk.mapper.CheckinMapper;
import com.jike.frontdesk.service.ICheckinService;
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
public class CheckinServiceImpl extends MppServiceImpl<CheckinMapper, Checkin> implements ICheckinService {

}
