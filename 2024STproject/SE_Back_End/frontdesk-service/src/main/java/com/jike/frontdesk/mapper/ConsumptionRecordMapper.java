package com.jike.frontdesk.mapper;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.jike.frontdesk.domain.po.ConsumptionRecord;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;

import java.util.List;

/**
 * <p>
 *  Mapper 接口
 * </p>
 *
 * @author 杨昕迪
 * @since 2023-12-26
 */
public interface ConsumptionRecordMapper extends BaseMapper<ConsumptionRecord> {
    @Select("SELECT * FROM CONSUMPTIONRECORDS WHERE ROOM_ID = (SELECT ROOM_ID FROM ROOM WHERE ROOM_NUMBER = #{roomNum})")
    List<ConsumptionRecord> getByRoomNum(int roomNum);
    @Select("SELECT cr.* FROM CONSUMPTIONRECORDS cr " +
            "JOIN CHECKIN ch ON cr.ROOM_ID = ch.ROOM_ID " +
            "JOIN CUSTOMER c ON ch.CUSTOMER_ID = c.CUSTOMER_ID " +
            "WHERE c.ID = #{id}")
    List<ConsumptionRecord> getByID(String id);

}
