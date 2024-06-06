package com.jike.logistics.mapper;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.jike.logistics.domain.po.Purchase;
import org.apache.ibatis.annotations.Update;

/**
 * <p>
 *  Mapper 接口
 * </p>
 *
 * @author 杨昕迪
 * @since 2023-01-02
 */
public interface PurchaseMapper extends BaseMapper<Purchase> {

    @Update("UPDATE GOODS SET INVENTORY = INVENTORY + #{purchaseQuantity} WHERE GOODS_ID = #{goodsId}")
    int updateGoodsInventory(int goodsId, int purchaseQuantity);
}
