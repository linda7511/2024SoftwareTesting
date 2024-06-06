package com.jike.frontdesk.controller;

import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.NewCustomerDTO;
import com.jike.frontdesk.domain.dto.PayDTO;
import com.jike.frontdesk.domain.po.Customer;
import com.jike.frontdesk.domain.po.Pay;
import com.jike.frontdesk.service.IPayService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Api(tags = "支付相关接口")
@RestController
@RequestMapping("api/Pay")
@RequiredArgsConstructor
@CrossOrigin
public class PayController {
    private final IPayService payService;

    @ApiOperation("新建支付")
    @PostMapping("Add")
    public ResponseResult<?> addPay(@RequestBody PayDTO payDTO) {
        Pay pay = BeanUtils.copyBean(payDTO, Pay.class);
        if (payService.save(pay))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("查找所有支付信息")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<Pay> pays = payService.list();
        if (CollUtils.isEmpty(pays)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(pays, PayDTO.class));
    }
}
