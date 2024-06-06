package com.jike.frontdesk.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.po.Customer;
import com.jike.frontdesk.domain.vo.CustomerVO;

/**
 * <p>
 *  服务类
 * </p>
 *
 * @author 杨昕迪
 * @since 2023-12-26
 */
public interface ICustomerService extends IService<Customer> {
    ResponseResult<?> getCustomerAndTimeInfo(int roomNum);
}
