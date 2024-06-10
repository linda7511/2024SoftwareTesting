package com.jike.performance.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.performance.domain.dto.AttendenceInformationDTO;
import com.jike.performance.domain.dto.SalaryDTO;
import com.jike.performance.domain.po.Salary;
import com.jike.performance.service.ISalaryService;
import org.junit.jupiter.api.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.annotation.Rollback;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.transaction.annotation.Transactional;

import java.nio.charset.StandardCharsets;
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
class SalaryControllerTest {

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
    @DisplayName("测试SalaryController中的getSalaryByEmployeeId方法,该数据存在")
    public void testGetSalaryByEmployeeId_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Salary/GetByEmployeeId/1")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<SalaryDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<SalaryDTO>>>() {});

        assertTrue(response.getStatus());
        assertEquals(1, response.getData().get(0).getSalaryId());
        assertEquals(1, response.getData().get(0).getEmployeeId());
        assertEquals(2023, response.getData().get(0).getYear());
        assertEquals(8, response.getData().get(0).getMonth());
        assertEquals(1200, response.getData().get(0).getBonus());
        assertEquals(600, response.getData().get(0).getHolidayAllowance());
        assertEquals(250, response.getData().get(0).getOtherAllowance());
        assertEquals(0, response.getData().get(0).getCommission());
        assertEquals(1800, response.getData().get(0).getYearEndBonus());
        assertEquals(350, response.getData().get(0).getOvertimePay());
        assertEquals("年终奖", response.getData().get(0).getRewardType());
        assertEquals(900, response.getData().get(0).getRewardAmount());
        assertEquals(250, response.getData().get(0).getLateDeduction());
        assertEquals(120, response.getData().get(0).getEarlyDepartureDeduction());
        assertEquals(550, response.getData().get(0).getIncomeTax());
        assertEquals(2500, response.getData().get(0).getSocialInsurance());
        assertEquals(10000, response.getData().get(0).getGrossSalary());
        assertEquals(10000, response.getData().get(0).getNetSalary());
    }

    @Test
    @DisplayName("测试SalaryController中的getSalaryByEmployeeId方法,该数据不存在")
    public void testGetSalaryByEmployeeId_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Salary/GetByEmployeeId/10")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString();
        ResponseResult<SalaryDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<SalaryDTO>>() {});

        assertFalse(response.getStatus());
    }

}
