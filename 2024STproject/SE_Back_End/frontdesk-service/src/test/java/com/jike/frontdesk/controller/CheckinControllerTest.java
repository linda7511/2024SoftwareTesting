package com.jike.frontdesk.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.CheckinDTO;
import com.jike.frontdesk.domain.po.Checkin;
import com.jike.frontdesk.domain.po.Room;
import com.jike.frontdesk.service.ICheckinService;
import com.jike.frontdesk.service.IRoomService;
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
class CheckinControllerTest {

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
    @DisplayName("测试CheckinController中的getCheckinInfoByCustomerIdAndInTime方法,该数据存在")
    public void testGetCheckinInfoByCustomerIdAndInTime_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Checkin/GetByCustomerIdAndInTime/1/2023-07-28T12:00:00"))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString();
        ResponseResult<CheckinDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<CheckinDTO>>() {});

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");

        assertTrue(response.getStatus());
        assertEquals(1, response.getData().getCustomerId());
        assertEquals(10, response.getData().getRoomId());
        assertEquals("2023-07-28T12:00:00", response.getData().getCheckinTime().format(formatter));
        assertEquals("2023-07-31T12:00:00", response.getData().getCheckoutTime().format(formatter));
    }

    @Test
    @DisplayName("测试CheckinController中的getCheckinInfoByCustomerIdAndInTime方法,该数据不存在")
    public void testGetCheckinInfoByCustomerIdAndInTime_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Checkin/GetByCustomerIdAndInTime/1/2023-06-28T12:00:00"))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString();
        ResponseResult<CheckinDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<CheckinDTO>>() {});

        assertFalse(response.getStatus());
    }
    @Test
    @DisplayName("测试CheckinController中的getCheckinInfoByRoomNum方法，该数据存在")
    public void testGetCheckinInfoByRoomNum_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Checkin/GetByRoomNum/101"))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString();
        ResponseResult<List<CheckinDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<CheckinDTO>>>() {});

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        assertTrue(response.getStatus());
        assertEquals(1, response.getData().size());
        assertEquals(26, response.getData().get(0).getCustomerId());
        assertEquals(1, response.getData().get(0).getRoomId());
        assertEquals("2023-06-15T12:00:00", response.getData().get(0).getCheckinTime().format(formatter));
        assertEquals("2023-06-16T12:00:00", response.getData().get(0).getCheckoutTime().format(formatter));
    }
    @Test
    @DisplayName("测试CheckinController中的getCheckinInfoByRoomNum方法，该数据不存在")
    public void testGetCheckinInfoByRoomNum_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Checkin/GetByRoomNum/999"))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString();
        ResponseResult<List<CheckinDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<CheckinDTO>>>() {});

        assertFalse(response.getStatus());
    }

}
