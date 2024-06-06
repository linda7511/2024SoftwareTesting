package com.jike.frontdesk.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.frontdesk.domain.dto.CustomerDTO;
import com.jike.frontdesk.domain.dto.NewCustomerDTO;
import com.jike.frontdesk.domain.dto.RoomOrderDTO;
import com.jike.frontdesk.domain.po.Customer;
import com.jike.frontdesk.domain.po.RoomOrder;
import com.jike.frontdesk.domain.vo.CustomerVO;
import com.jike.frontdesk.service.ICustomerService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;


@Api(tags = "顾客相关接口")
@RestController
@RequestMapping("api/Customer")
@RequiredArgsConstructor
@CrossOrigin
public class CustomerController {
    private final ICustomerService customerService;

    @ApiOperation("通过主码查")
    @GetMapping("Get/{id}")
    public ResponseResult<CustomerDTO> getCustomerById(@Param("id") @PathVariable("id") int id) {
        Customer customer = customerService.getById(id);
        if (customer != null) {
            return ResponseResult.success(BeanUtils.copyBean(customer, CustomerDTO.class));
        }
        return ResponseResult.fail("找不到顾客");
    }

    @ApiOperation("通过身份证号ID查")
    @GetMapping("GetById/{ID}")
    public ResponseResult<CustomerDTO> getCustomerByID(@Param("身份证号") @PathVariable("ID") String ID) {
        QueryWrapper<Customer> queryWrapper = new QueryWrapper<Customer>()
                .eq("ID", ID);
        Customer customer = customerService.getOne(queryWrapper, false);
        if (customer != null) {
            return ResponseResult.success(BeanUtils.copyBean(customer, CustomerDTO.class));
        }
        return ResponseResult.fail("找不到顾客");
    }

    @ApiOperation("通过姓名查")
    @GetMapping("GetByName/{name}")
    public ResponseResult<CustomerDTO> getCustomerByName(@Param("姓名") @PathVariable("name") String name) {
        QueryWrapper<Customer> queryWrapper = new QueryWrapper<Customer>()
                .eq("NAME", name);
        Customer customer = customerService.getOne(queryWrapper, false);
        if (customer != null) {
            return ResponseResult.success(BeanUtils.copyBean(customer, CustomerDTO.class));
        }
        return ResponseResult.fail("找不到顾客");
    }

    @ApiOperation("通过手机号查")
    @GetMapping("GetByPhone/{phone}")
    public ResponseResult<CustomerDTO> getCustomerByPhone(@Param("手机号") @PathVariable("phone") String phone) {
        QueryWrapper<Customer> queryWrapper = new QueryWrapper<Customer>()
                .eq("PHONE", phone);
        Customer customer = customerService.getOne(queryWrapper, false);
        if (customer != null) {
            return ResponseResult.success(BeanUtils.copyBean(customer, CustomerDTO.class));
        }
        return ResponseResult.fail("找不到顾客");
    }

    @ApiOperation("新建顾客")
    @PostMapping("Add")
    public ResponseResult<String> addCustomer(@RequestBody NewCustomerDTO newCustomerDTO) {
        Customer customer = BeanUtils.copyBean(newCustomerDTO, Customer.class);
        if (customerService.save(customer))
            return ResponseResult.success("新增成功");
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("更新顾客")
    @PutMapping("Update")
    public ResponseResult<String> updateCustomer(@RequestBody CustomerDTO CustomerDTO) {
        Customer customer = BeanUtils.copyBean(CustomerDTO, Customer.class);
        if (customerService.updateById(customer))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("通过房间号查")
    @GetMapping("GetCustomerAndTimeInfo/{roomNum}")
    public ResponseResult<?> getCustomerAndTimeInfo(@Param("房间号") @PathVariable("roomNum") int roomNum) {
            return customerService.getCustomerAndTimeInfo(roomNum);
    }

    @ApiOperation("查找所有顾客")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<Customer> customers = customerService.list();
        if (CollUtils.isEmpty(customers)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(customers, CustomerDTO.class));
    }

}
