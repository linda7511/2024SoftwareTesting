package com.jike.frontdesk.controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.RoomDTO;
import com.jike.frontdesk.domain.po.Room;
import com.jike.frontdesk.service.IRoomService;
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
import java.util.Collections;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.anyInt;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
class RoomControllerTest {

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
    @DisplayName("测试RoomController中的getRoomById方法,数据存在")
    void getRoomById_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Room/Get/1")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<RoomDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<RoomDTO>>() {});

        assertTrue(response.getStatus());
        assertEquals(1, response.getData().getRoomId());
        assertEquals(101, response.getData().getRoomNumber());
        assertEquals("已入住", response.getData().getStatus());
        assertEquals(1, response.getData().getTypeId());
        assertEquals("2101", response.getData().getRoomPhone());
    }

    @Test
    @DisplayName("测试RoomController中的getRoomById方法,数据不存在")
    void getRoomById_Fail() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Room/Get/999")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<RoomDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<RoomDTO>>() {});

        assertFalse(response.getStatus());
    }

    @Test
    @DisplayName("测试RoomController中的getRoomIdByRoomNum方法,数据存在")
    void getRoomIdByRoomNum_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Room/GetRoomIdByRoomNum/101")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<Integer> response = objectMapper.readValue(content, new TypeReference<ResponseResult<Integer>>() {});

        assertTrue(response.getStatus());
        assertEquals(1, response.getData());
    }

    @Test
    @DisplayName("测试RoomController中的getRoomIdByRoomNum方法,数据不存在")
    void getRoomIdByRoomNum_Fail() throws Exception {
        MvcResult result = mockMvc.perform(get("/api/Room/GetRoomIdByRoomNum/999")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();
        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<Integer> response = objectMapper.readValue(content, new TypeReference<ResponseResult<Integer>>() {});
        assertFalse(response.getStatus());
    }
}
