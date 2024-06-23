package com.jike.performance.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.performance.domain.dto.AttendenceInformationDTO;
import com.jike.performance.domain.po.AttendenceInformation;
import com.jike.performance.service.IAttendenceInformationService;
import org.junit.jupiter.api.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.test.annotation.Rollback;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Arrays;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
class AttendenceInformationControllerTest {

    @Autowired
    private MockMvc mockMvc;


    private ObjectMapper objectMapper;
    @BeforeEach
    void setUp() {
        objectMapper = new ObjectMapper();
        objectMapper.registerModule(new JavaTimeModule());
        System.out.println("测试开始:");
    }

    @AfterEach
    void tearDown() {
        System.out.println("测试结束!");
    }

    @Test
    @DisplayName("测试AttendenceInformationController中的getAttendanceInformationByEmployeeId方法,该数据存在")
    public void testGetAttendanceInformationByEmployeeId_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/AttendanceInformation/GetByEmpId/1"))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString();
        ResponseResult<List<AttendenceInformationDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<AttendenceInformationDTO>>>() {});

        assertTrue(response.getStatus());
        assertEquals(1, response.getData().get(0).getAttendanceId());
        assertEquals(1, response.getData().get(0).getEmployeeId());
        assertEquals(2023, response.getData().get(0).getYear());
        assertEquals(8, response.getData().get(0).getMonth());
        assertEquals(31, response.getData().get(0).getExpectAttendanceDays());
        assertEquals(31, response.getData().get(0).getActualAttendanceDays());
        assertEquals(0, response.getData().get(0).getPersonalLeaveDays());
        assertEquals(0, response.getData().get(0).getSickLeaveDays());
        assertEquals(0, response.getData().get(0).getLateDays());
        assertEquals(0, response.getData().get(0).getEarlyDepartureDays());
        assertEquals(0, response.getData().get(0).getAbsentDays());
    }

    @Test
    @DisplayName("测试AttendenceInformationController中的getAttendanceInformationByEmployeeId方法,该数据不存在")
    public void testGetAttendanceInformationByEmployeeId_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/AttendanceInformation/GetByEmpId/99"))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString();
        ResponseResult<AttendenceInformationDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<AttendenceInformationDTO>>() {});

        assertFalse(response.getStatus());
    }

}
