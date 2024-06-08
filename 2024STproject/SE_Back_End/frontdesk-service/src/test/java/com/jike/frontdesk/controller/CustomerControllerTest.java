package com.jike.frontdesk.controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.CustomerDTO;
import com.jike.frontdesk.domain.po.Customer;
import com.jike.frontdesk.service.ICustomerService;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit.jupiter.SpringExtension;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;

import java.nio.charset.StandardCharsets;
import java.util.Collections;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.eq;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
class CustomerControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private ObjectMapper objectMapper;


    @Test
    @DisplayName("通过主码查找顾客 - 数据存在")
    public void getCustomerById_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Customer/Get/2")
                        .accept(new MediaType("application", "json", StandardCharsets.UTF_8)))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<CustomerDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<CustomerDTO>>() {});

        assertTrue(response.getStatus());

        assertEquals("刘军", response.getData().getName());
        assertEquals("320102199508152014", response.getData().getID());
        assertEquals(2, response.getData().getCustomerId());
        assertEquals("男", response.getData().getGender());
        assertEquals("3", response.getData().getCreditGrade());
        assertEquals("13712345678", response.getData().getPhone());
        assertEquals("1", response.getData().getMemberGrade());
    }
    @Test
    @DisplayName("通过主码查找顾客 - 数据不存在")
    public void getCustomerById_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Customer/Get/999")
                        .accept(new MediaType("application", "json", StandardCharsets.UTF_8)))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<CustomerDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<CustomerDTO>>() {});

        assertFalse(response.getStatus());
    }
    @Test
    @DisplayName("通过身份证号查找顾客 - 成功")
    public void getCustomerByID_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Customer/GetById/320102199508152014")
                        .accept(new MediaType("application", "json", StandardCharsets.UTF_8)))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<CustomerDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<CustomerDTO>>() {});

        assertTrue(response.getStatus());

        assertEquals("刘军", response.getData().getName());
        assertEquals("320102199508152014", response.getData().getID());
        assertEquals(2, response.getData().getCustomerId());
        assertEquals("男", response.getData().getGender());
        assertEquals("3", response.getData().getCreditGrade());
        assertEquals("13712345678", response.getData().getPhone());
        assertEquals("1", response.getData().getMemberGrade());
    }

    @Test
    @DisplayName("通过身份证号查找顾客 - 数据不存在")
    public void getCustomerByID_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Customer/GetById/1111111111111")
                        .accept(new MediaType("application", "json", StandardCharsets.UTF_8)))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<CustomerDTO> response = objectMapper.readValue(content, new TypeReference<ResponseResult<CustomerDTO>>() {});

        assertFalse(response.getStatus());
    }

}
