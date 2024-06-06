package com.jike.canteen.mapper;

import com.github.jeffreyning.mybatisplus.base.MppBaseMapper;
import com.jike.canteen.domain.dto.OrderDishDTO;
import com.jike.canteen.domain.po.Book;
import com.jike.canteen.domain.po.MyOrder;
import org.apache.ibatis.annotations.Result;
import org.apache.ibatis.annotations.Results;
import org.apache.ibatis.annotations.Select;

import java.util.List;

public interface MyOrderMapper extends MppBaseMapper<MyOrder> {
    @Select("SELECT m.TABLE_ID, m.DISH_ID, m.CONSUMPTION_RECORD_ID, m.ORDER_TIME, m.ORDER_STATUS, " +
            "d.DISH_NAME, d.DISH_PRICE, d.DISH_TASTE " +
            "FROM MYORDER m " +
            "JOIN DISH d ON m.DISH_ID = d.DISH_ID " +
            "WHERE m.TABLE_ID = #{tableId}")
    @Results({
            @Result(property = "tableId", column = "TABLE_ID"),
            @Result(property = "dishId", column = "DISH_ID"),
            @Result(property = "consumptionRecordId", column = "CONSUMPTION_RECORD_ID"),
            @Result(property = "orderTime", column = "ORDER_TIME"),
            @Result(property = "orderStatus", column = "ORDER_STATUS"),
            @Result(property = "dishName", column = "DISH_NAME"),
            @Result(property = "dishPrice", column = "DISH_PRICE"),
            @Result(property = "dishTaste", column = "DISH_TASTE")
    })
    List<OrderDishDTO> selectOrderAndDishInfoByTableId(int tableId);
}
