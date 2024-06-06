package com.jike.canteen.service;

import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

@FeignClient("frontdesk-service")
public interface FrontdeskClient {
    @PostMapping("/api/ConsumptionRecord/AddCateringRecord")
    int addCateringRecord(@RequestParam int roomNumber,@RequestParam double ConsumeAmount);
}
