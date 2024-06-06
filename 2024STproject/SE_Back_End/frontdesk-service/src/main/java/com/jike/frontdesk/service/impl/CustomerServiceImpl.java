package com.jike.frontdesk.service.impl;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.po.Customer;
import com.jike.frontdesk.domain.vo.CustomerVO;
import com.jike.frontdesk.mapper.CustomerMapper;
import com.jike.frontdesk.service.ICustomerService;
import org.springframework.stereotype.Service;

/**
 * <p>
 * 服务实现类
 * </p>
 *
 * @author 杨昕迪
 * @since 2023-12-26
 */
@Service
public class CustomerServiceImpl extends ServiceImpl<CustomerMapper, Customer> implements ICustomerService {
    @Override
    public ResponseResult<?> getCustomerAndTimeInfo(int roomNum) {
        CustomerVO vo = baseMapper.selectCusAndTimeByRoomNum(roomNum);
        if (vo != null)
            return ResponseResult.success(vo);
        return ResponseResult.fail("找不到顾客");
    }
}
