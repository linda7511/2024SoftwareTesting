package com.jike.performance.service.impl;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.performance.domain.po.AttendenceInformation;
import com.jike.performance.mapper.AttendenceInformationMapper;
import com.jike.performance.service.IAttendenceInformationService;
import org.springframework.stereotype.Service;


@Service
public class AttendenceInformationServiceImpl extends ServiceImpl<AttendenceInformationMapper,AttendenceInformation>
        implements IAttendenceInformationService
{

}
