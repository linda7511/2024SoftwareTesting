package com.jike.frontdesk.controller;

import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.CheckinDTO;
import com.jike.frontdesk.domain.dto.NewBillDTO;
import com.jike.frontdesk.domain.po.Bill;
import com.jike.frontdesk.domain.po.Checkin;
import com.jike.frontdesk.service.IBillService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

@Api(tags = "账单相关接口")
@RestController
@RequestMapping("api/Bill")
@RequiredArgsConstructor
@CrossOrigin
public class BillController {
    private final IBillService billService;

    @ApiOperation("新建账单")
    @PostMapping("Add")
    public ResponseResult<String> addBill(@RequestBody NewBillDTO newBillDTO) {

        Bill bill = BeanUtils.copyBean(newBillDTO, Bill.class);
        if (billService.save(bill))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }
}
