package com.jike.frontdesk.mapper;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.jike.frontdesk.domain.po.Customer;
import com.jike.frontdesk.domain.vo.CustomerVO;
import org.apache.ibatis.annotations.Result;
import org.apache.ibatis.annotations.Results;
import org.apache.ibatis.annotations.Select;

/**
 * <p>
 *  Mapper 接口
 * </p>
 *
 * @author 杨昕迪
 * @since 2023-12-26
 */
public interface CustomerMapper extends BaseMapper<Customer>  {

    @Select("SELECT c.CUSTOMER_ID, c.ID, c.GENDER, c.PHONE, c.CREDIT_GRADE, c.MEMBER_GRADE, c.NAME, ci.CHECKIN_TIME, ci.CHECKOUT_TIME " +
            "FROM ROOM r " +
            "JOIN CHECKIN ci ON r.ROOM_ID = ci.ROOM_ID " +
            "JOIN CUSTOMER c ON ci.CUSTOMER_ID = c.CUSTOMER_ID " +
            "WHERE r.ROOM_NUMBER = #{roomNumber}")
    @Results({
            @Result(property = "customerId", column = "CUSTOMER_ID"),
            @Result(property = "ID", column = "ID"),
            @Result(property = "gender", column = "GENDER"),
            @Result(property = "phone", column = "PHONE"),
            @Result(property = "creditGrade", column = "CREDIT_GRADE"),
            @Result(property = "memberGrade", column = "MEMBER_GRADE"),
            @Result(property = "checkinTime", column = "CHECKIN_TIME"),
            @Result(property = "checkoutTime", column = "CHECKOUT_TIME")
    })
    CustomerVO selectCusAndTimeByRoomNum(int roomNumber);
}
