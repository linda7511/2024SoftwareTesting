package com.jike.canteen.controller;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.canteen.domain.dto.OrderDishDTO;
import com.jike.common.utils.ResponseResult;
import com.jike.canteen.domain.dto.DishDTO;
import com.jike.canteen.domain.dto.MyTableDTO;
import com.jike.canteen.domain.dto.MyOrderDTO;
import org.junit.jupiter.api.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.MvcResult;

import java.nio.charset.StandardCharsets;
import java.time.format.DateTimeFormatter;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.any;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@SpringBootTest
@AutoConfigureMockMvc
class DishOrderTableControllerTest {

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
    @DisplayName("测试CombinedController中的getAllDishes方法,该数据存在")
    public void testGetAllDishes_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/Dish/GetAllDishes")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<DishDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<DishDTO>>>() {});
        assertTrue(response.getStatus());


    }


    @Test
    @DisplayName("测试CombinedController中的getOrderAndDishInfo方法,该数据存在")
    public void testGetOrderAndDishInfo_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/MyOrder/GetOrderAndDishInfo/3")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<OrderDishDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<OrderDishDTO>>>() {});
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        assertTrue(response.getStatus());
    }

    @Test
    @DisplayName("测试CombinedController中的getOrderAndDishInfo方法,该数据不存在")
    public void testGetOrderAndDishInfo_F1() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/MyOrder/GetOrderAndDishInfo/20")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<MyOrderDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<MyOrderDTO>>>() {});

        assertFalse(response.getStatus());
    }

    @Test
    @DisplayName("测试CombinedController中的getAllTables方法,该数据存在")
    public void testGetAllTables_Success() throws Exception {

        MvcResult result = mockMvc.perform(get("/api/MyTable/GetAll")
                        .accept(MediaType.APPLICATION_JSON_UTF8))
                .andExpect(status().isOk())
                .andReturn();

        String content = result.getResponse().getContentAsString(StandardCharsets.UTF_8);
        ResponseResult<List<MyTableDTO>> response = objectMapper.readValue(content, new TypeReference<ResponseResult<List<MyTableDTO>>>() {});
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");
        assertTrue(response.getStatus());
    }

}
