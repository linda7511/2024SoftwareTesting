package com.jike.frontdesk.service.impl;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.frontdesk.domain.po.ConsumptionRecord;
import com.jike.frontdesk.mapper.ConsumptionRecordMapper;
import com.jike.frontdesk.service.IConsumptionRecordService;
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
public class ConsumptionRecordServiceImpl extends ServiceImpl<ConsumptionRecordMapper, ConsumptionRecord> implements IConsumptionRecordService {
}
