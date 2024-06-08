package com.jike.frontdesk.controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.NewRoomOrderDTO;
import com.jike.frontdesk.domain.dto.RoomOrderDTO;
import com.jike.frontdesk.domain.dto.RoomOrderUpdateDTO;
import com.jike.frontdesk.domain.po.RoomOrder;
import com.jike.frontdesk.domain.po.RoomType;
import com.jike.frontdesk.service.IRoomOrderService;
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
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.Collections;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.anyInt;
import static org.mockito.ArgumentMatchers.anyString;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
class RoomOrderControllerTest {

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
    @DisplayName("测试RoomOrderController中的getRoomOrderByCustomerId方法,数据存在")
    void getRoomOrderByCustomerId_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Roomorder/GetByCustomerId/24")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<RoomOrderDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<RoomOrderDTO>>>() {});
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        assertTrue(response.getStatus());
        assertEquals(1, response.getData().size());
        assertEquals(24, response.getData().get(0).getCustomerId());
        assertEquals(1, response.getData().get(0).getOrderId());
        assertEquals(1, response.getData().get(0).getTypeId());
        assertEquals(5, response.getData().get(0).getEmployeeId());
        assertEquals("未处理", response.getData().get(0).getOrderStatus());
        assertEquals("2023-08-01T09:30:00", response.getData().get(0).getCreateTime().format(formatter));
        assertEquals("2023-08-02T00:00:00", response.getData().get(0).getExpectInTime().format(formatter));
        assertEquals("2023-08-03T00:00:00", response.getData().get(0).getExpectOutTime().format(formatter));
        assertEquals(269, response.getData().get(0).getPrice());
        assertEquals(null, response.getData().get(0).getNote());
        assertEquals(1, response.getData().get(0).getCustomerNum());

    }

    @Test
    @DisplayName("测试RoomOrderController中的getRoomOrderByCustomerId方法,数据不存在")
    void getRoomOrderByCustomerId_Fail() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Roomorder/GetByCustomerId/9999")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<RoomOrderDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<RoomOrderDTO>>>() {});

        assertTrue(response.getStatus());
        assertTrue(response.getData().isEmpty());
    }



}
