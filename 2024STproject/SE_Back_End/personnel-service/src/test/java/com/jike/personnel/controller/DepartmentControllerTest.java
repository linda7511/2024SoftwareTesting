package com.jike.personnel.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.personnel.domain.dto.DepartmentDTO;
import com.jike.personnel.domain.po.Department;
import com.jike.personnel.service.IDepartmentService;
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
class DepartmentControllerTest {

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
    @DisplayName("测试DepartmentController中的getDepartmentById方法,该数据存在")
    public void testGetDepartmentById_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Department/Get/1")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<DepartmentDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<DepartmentDTO>>() {});

        assertTrue(response.getStatus());
        assertEquals(1, response.getData().getDepartmentId());
        assertEquals("总经理办公室", response.getData().getDepartmentName());
        assertEquals(4, response.getData().getNumberOfPeople());
    }

    @Test
    @DisplayName("测试DepartmentController中的getDepartmentById方法,该数据不存在")
    public void testGetDepartmentById_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Department/Get/10")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<DepartmentDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<DepartmentDTO>>() {});

        assertFalse(response.getStatus());
    }

    @Test
    @DisplayName("测试DepartmentController中的getApplicationByName方法,该数据存在")
    public void testGetApplicationByName_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Department/GetByName/总经理办公室")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<DepartmentDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<DepartmentDTO>>() {});

        assertTrue(response.getStatus());
        assertEquals(1, response.getData().getDepartmentId());
        assertEquals("总经理办公室", response.getData().getDepartmentName());
        assertEquals(4, response.getData().getNumberOfPeople());
    }

    @Test
    @DisplayName("测试DepartmentController中的getApplicationByName方法,该数据不存在")
    public void testGetApplicationByName_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Department/GetByName/abc")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<DepartmentDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<DepartmentDTO>>() {});

        assertFalse(response.getStatus());
    }

}
