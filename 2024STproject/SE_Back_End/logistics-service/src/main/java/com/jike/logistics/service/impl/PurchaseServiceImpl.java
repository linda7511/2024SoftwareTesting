package com.jike.logistics.service.impl;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.logistics.domain.po.Purchase;
import com.jike.logistics.mapper.PurchaseMapper;
import com.jike.logistics.service.IPurchaseService;
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
public class PurchaseServiceImpl extends ServiceImpl<PurchaseMapper, Purchase> implements IPurchaseService {
}
