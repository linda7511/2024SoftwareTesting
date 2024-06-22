package com.jike.frontdesk.controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.po.ConsumptionRecord;
import com.jike.frontdesk.service.IConsumptionRecordService;
import com.jike.frontdesk.service.IRoomService;
import com.jike.frontdesk.mapper.ConsumptionRecordMapper;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import java.nio.charset.StandardCharsets;
import java.time.format.DateTimeFormatter;
import java.util.Collections;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
class ConsumptionRecordControllerTest {

    @Autowired
    private MockMvc mockMvc;

    private ObjectMapper objectMapper;

    @BeforeEach
    void setUp() {
        objectMapper = new ObjectMapper();
        objectMapper.registerModule(new JavaTimeModule());
        System.out.println("测试开始:");
    }

    @Test
    @DisplayName("测试ConsumptionRecordController中的getByRoomNum方法,数据存在")
    void getByRoomNum_Success() throws Exception {
        MvcResult result = mockMvc.perform(MockMvcRequestBuilders.get("/api/ConsumptionRecord/GetByRoomNum/303")
                        .accept(MediaType.APPLICATION_JSON_UTF8)) // Set the Accept header to application/json with UTF-8
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8); // Decode the response as UTF-8
        ResponseResult<List<ConsumptionRecord>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<ConsumptionRecord>>>() {});

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss"); // Formatter with seconds
        assertTrue(response.getStatus());
        assertEquals(1, response.getData().size());
        assertEquals(15, response.getData().get(0).getConsumeId());
        assertEquals(15, response.getData().get(0).getRoomId());
        assertEquals(7, response.getData().get(0).getConsumeAmount());
        assertEquals("2022-10-26T08:08:08", response.getData().get(0).getConsumeTime().format(formatter)); // Expected value with seconds
        assertEquals("饮料", response.getData().get(0).getConsumeType());
    }


    @Test
    @DisplayName("测试ConsumptionRecordController中的getByRoomNum方法,数据不存在")
    void getByRoomNum_F1() throws Exception {
        MvcResult result = mockMvc.perform(get("/api/ConsumptionRecord/GetByRoomNum/999"))
                .andExpect(status().isOk())
                .andReturn();
        String content = result.getResponse().getContentAsString();
        ResponseResult<List<ConsumptionRecord>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<ConsumptionRecord>>>() {});
        assertFalse(response.getStatus());
    }
    @Test
    @DisplayName("测试ConsumptionRecordController中的getByID方法,数据存在")
    void getByID_Success() throws Exception {
        MvcResult result = mockMvc.perform(MockMvcRequestBuilders.get("/api/ConsumptionRecord/GetByID/210112200412144453")
                        .accept(new MediaType("application", "json", StandardCharsets.UTF_8))) // Set the Accept header to application/json with UTF-8
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8); // Decode the response as UTF-8
        ResponseResult<List<ConsumptionRecord>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<ConsumptionRecord>>>() {});

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss"); // Formatter with seconds
        assertTrue(response.getStatus());
        assertEquals(1, response.getData().size());
        assertEquals(15, response.getData().get(0).getConsumeId());
        assertEquals(15, response.getData().get(0).getRoomId());
        assertEquals(7, response.getData().get(0).getConsumeAmount());
        assertEquals("2022-10-26T08:08:08", response.getData().get(0).getConsumeTime().format(formatter)); // Expected value with seconds
        assertEquals("饮料", response.getData().get(0).getConsumeType());
    }

    @Test
    @DisplayName("测试ConsumptionRecordController中的getByID方法,数据不存在")
    void getByID_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/ConsumptionRecord/GetByID/11111111111111"))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString();
        ResponseResult<List<ConsumptionRecord>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<ConsumptionRecord>>>() {});
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        assertFalse(response.getStatus());
    }
}
