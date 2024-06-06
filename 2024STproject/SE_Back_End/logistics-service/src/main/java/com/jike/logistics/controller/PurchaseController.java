package com.jike.logistics.controller;

import cn.hutool.core.collection.CollUtil;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.logistics.domain.dto.GoodsDTO;
import com.jike.logistics.domain.dto.MaintainDTO;
import com.jike.logistics.domain.dto.NewPurchaseDTO;
import com.jike.logistics.domain.dto.PurchaseDTO;
import com.jike.logistics.domain.po.Goods;
import com.jike.logistics.domain.po.Maintain;
import com.jike.logistics.domain.po.Purchase;
import com.jike.logistics.mapper.PurchaseMapper;
import com.jike.logistics.service.IPurchaseService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@Api(tags = "采购相关接口")
@RestController
@RequestMapping("api/Purchase")
@RequiredArgsConstructor
@CrossOrigin
public class PurchaseController {
    private final IPurchaseService purchaseService;

    private final PurchaseMapper purchaseMapper;

    @ApiOperation("获取所有采购信息（若干条）")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<Purchase> purchaseList = purchaseService.list();
        if (CollUtil.isEmpty(purchaseList)) {
            return ResponseResult.success();
        }
        return ResponseResult.success(BeanUtils.copyList(purchaseList, PurchaseDTO.class));
    }


    @ApiOperation("通过物资id查询")
    @GetMapping("GetByGoodsId/{GoodsId}")
    public ResponseResult<?> getPurchaseInfoByGoodsId(@Param("GoodsId") @PathVariable("GoodsId") int id) {
        QueryWrapper<Purchase> queryWrapper = new QueryWrapper<Purchase>()
                .eq("GOODS_ID", id);
        List<Purchase> purchaseList = purchaseService.list(queryWrapper);
        if (purchaseList != null && !purchaseList.isEmpty()) {
            List<PurchaseDTO> purchaseDTOList = purchaseList.stream()
                    .map(purchase -> BeanUtils.copyBean(purchase, PurchaseDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(purchaseDTOList);
        }
        return ResponseResult.fail("找不到购买记录");
    }


    @ApiOperation("新建购买记录信息")
    @PostMapping("Add")
    public ResponseResult<?> addPurchaseInfo(@RequestBody NewPurchaseDTO purchaseDTO) {
        Purchase purchase = BeanUtils.copyBean(purchaseDTO, Purchase.class);
        if (purchaseService.save(purchase)) {
            purchaseMapper.updateGoodsInventory(purchase.getGoodsId(),purchase.getPurchaseQuantity());
            return ResponseResult.success("新增成功");
        }
        return ResponseResult.fail("新增失败");
    }

    @ApiOperation("通过物资id和员工id删除")
    @DeleteMapping("Delete/{PurchaseId}/{EmpId}")
    public ResponseResult<?> deletePurchaseInfo(@Param("购买id") @PathVariable("PurchaseId") int purchaseId,
                                        @Param("员工id") @PathVariable("EmpId") int empId) {
        QueryWrapper<Purchase> queryWrapper = new QueryWrapper<Purchase>()
                .eq("PURCHASE_ID", purchaseId)
                .eq("EMPLOYEE_ID", empId);
        boolean isRemoved = purchaseService.remove(queryWrapper);
        if (isRemoved) {
            return ResponseResult.success("采购信息删除成功");
        }
        return ResponseResult.fail("找不到对应的采购信息，删除失败");
    }

}
