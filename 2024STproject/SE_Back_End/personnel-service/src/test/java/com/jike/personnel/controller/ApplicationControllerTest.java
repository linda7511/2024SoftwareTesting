package com.jike.personnel.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.personnel.domain.dto.ApplicationDTO;
import com.jike.personnel.domain.po.Application;
import com.jike.personnel.service.IApplicationService;
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
class ApplicationControllerTest {

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
    @DisplayName("测试ApplicationController中的getApplicationByEmpId方法,该数据存在")
    public void testGetApplicationByEmpIdByEmployeeId_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Application/GetByEmpId/1")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<ApplicationDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<ApplicationDTO>>>() {});
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
        assertTrue(response.getStatus());
        assertEquals(10, response.getData().size());
        assertEquals(1, response.getData().get(0).getApplicationId());
        String formattedDate = formatter.format(response.getData().get(0).getApplicationTime());
        assertEquals("2023-07-25", formattedDate);
        assertEquals("请假一天", response.getData().get(0).getApplicationType());
        assertEquals("因家庭原因需要请假一天", response.getData().get(0).getApplicationContent());
        assertEquals("是", response.getData().get(0).getIfApprove());
        assertEquals(1, response.getData().get(0).getEmployeeId());
        assertEquals(1, response.getData().get(0).getDepartmentId());



    }

    @Test
    @DisplayName("测试ApplicationController中的getApplicationByEmpId方法,该数据不存在")
    public void testGetApplicationByEmpId_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Application/GetByEmpId/2")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<ApplicationDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<ApplicationDTO>>>() {});

        assertFalse(response.getStatus());
    }

}
