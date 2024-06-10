package com.jike.canteen.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.jike.canteen.domain.dto.OrderDishDTO;
import com.jike.common.utils.ResponseResult;
import com.jike.canteen.domain.dto.DishDTO;
import com.jike.canteen.domain.dto.MyTableDTO;
import com.jike.canteen.domain.dto.MyOrderDTO;
import com.jike.canteen.domain.po.Dish;
import com.jike.canteen.domain.po.MyTable;
import com.jike.canteen.domain.po.MyOrder;
import com.jike.canteen.service.IDishService;
import com.jike.canteen.service.IMyTableService;
import com.jike.canteen.service.IMyOrderService;
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
        assertEquals(23, response.getData().size());
        assertEquals(1, response.getData().get(0).getDishId());
        assertEquals("奶油蘑菇汤", response.getData().get(0).getDishName());
        assertEquals(12, response.getData().get(0).getDishPrice());
        assertEquals("甜", response.getData().get(0).getDishTaste());

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
        assertEquals(16, response.getData().size());
        assertEquals(3, response.getData().get(0).getTableId());
        assertEquals(3, response.getData().get(0).getDishId());
        assertEquals(0, response.getData().get(0).getConsumptionRecordId());
        assertEquals("2022-11-11T00:00:00", response.getData().get(0).getOrderTime().format(formatter));
        assertEquals("点单成功", response.getData().get(0).getOrderStatus());
        assertEquals("香辣虾", response.getData().get(0).getDishName());
        assertEquals(28.0, response.getData().get(0).getDishPrice());
        assertEquals("辣鲜", response.getData().get(0).getDishTaste());

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
        assertEquals(16, response.getData().size());
        assertEquals(1, response.getData().get(0).getTableId());
        assertEquals(10, response.getData().get(0).getCapacity());
        assertEquals("大桌", response.getData().get(0).getTableType());
        assertEquals("16B", response.getData().get(0).getTableLocation());
        assertEquals("空闲", response.getData().get(0).getTableStatus());
        assertEquals("适用于10人以上", response.getData().get(0).getNote());
        assertEquals(1, response.getData().get(0).getBookable());
        assertEquals(1, response.getData().get(0).getAvailable());

    }

}
