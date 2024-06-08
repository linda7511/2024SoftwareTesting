package com.jike.frontdesk.controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.RoomTypeDTO;
import com.jike.frontdesk.domain.po.RoomType;
import com.jike.frontdesk.service.IRoomTypeService;
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

import java.nio.charset.StandardCharsets;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.anyInt;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
class RoomTypeControllerTest {

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
    @DisplayName("测试RoomTypeController中的getRoomTypeById方法,数据存在")
    void getRoomTypeById_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/RoomType/Get/1")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<RoomTypeDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<RoomTypeDTO>>() {});

        assertTrue(response.getStatus());
        assertEquals(1, response.getData().getTypeId());
        assertEquals("双人间", response.getData().getTypeName());
        assertEquals(269, response.getData().getPrice());
        assertEquals(1, response.getData().getRemaining());
        assertEquals(null, response.getData().getNote());
    }

    @Test
    @DisplayName("测试RoomTypeController中的getRoomTypeById方法,数据不存在")
    void getRoomTypeById_Fail() throws Exception {


        MvcResult result = mockMvc.perform(get("/api/RoomType/Get/999")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<RoomTypeDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<RoomTypeDTO>>() {});

        assertFalse(response.getStatus());
    }
}
