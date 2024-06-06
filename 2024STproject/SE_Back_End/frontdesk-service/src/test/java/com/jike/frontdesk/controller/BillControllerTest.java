package com.jike.frontdesk.controller;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.NewBillDTO;
import com.jike.frontdesk.domain.po.Bill;
import com.jike.frontdesk.service.IBillService;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.when;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

class BillControllerTest {

    @Mock
    private IBillService billService;

    @InjectMocks
    private BillController billController;

    private MockMvc mockMvc;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
        mockMvc = MockMvcBuilders.standaloneSetup(billController).build();
    }

    @Test
    void addBill() throws Exception {
        NewBillDTO newBillDTO = new NewBillDTO();
        newBillDTO.setRoomCost(100.0);
        newBillDTO.setFoodCost(50.0);
        newBillDTO.setOtherCost(20.0);
        newBillDTO.setSumCost(170.0);


        ObjectMapper objectMapper = new ObjectMapper();
        String newBillDTOJson = objectMapper.writeValueAsString(newBillDTO);

        when(billService.save(any(Bill.class))).thenReturn(true);

        mockMvc.perform(post("/api/Bill/Add")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(newBillDTOJson))
                .andExpect(status().isOk());

        // Verify that the service method was called
        Mockito.verify(billService).save(any(Bill.class));
    }
}
