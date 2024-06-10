package com.jike.personnel.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.personnel.domain.dto.DepartmentDTO;
import com.jike.personnel.domain.dto.EmployeeDTO;
import com.jike.personnel.domain.po.Employee;
import com.jike.personnel.service.IEmployeeService;
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
import java.text.SimpleDateFormat;
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
class EmployeeControllerTest {

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
    @DisplayName("测试EmployeeController中的getEmployeeById方法,该数据存在")
    public void testGetEmployeeById_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Employee/GetByEmpId/1")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<EmployeeDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<EmployeeDTO>>() {});
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
        assertTrue(response.getStatus());
        assertEquals(1, response.getData().getEmployeeId());
        assertEquals("章北海", response.getData().getName());
        assertEquals("男", response.getData().getSex());
        assertEquals(42, response.getData().getAge());
        assertEquals(1, response.getData().getPostId());
        String formattedDate = formatter.format(response.getData().getEntryTime());
        assertEquals("2023-07-22", formattedDate);
        assertEquals("13269664877", response.getData().getPhoneNumber());
        assertEquals(0, response.getData().getBasePay());
        assertEquals("e10adc3949ba59abbe56e057f20f88", response.getData().getPassword());
        assertEquals("中国农业银行", response.getData().getBankName());
        assertEquals("6217009670700268381", response.getData().getCreditCardNumber());
        assertEquals("zbh", response.getData().getAccount());


    }

    @Test
    @DisplayName("测试EmployeeController中的getEmployeeById方法,该数据不存在")
    public void testGetEmployeeById_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Employee/GetByEmpId/20")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<EmployeeDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<EmployeeDTO>>() {});

        assertFalse(response.getStatus());
    }

}
