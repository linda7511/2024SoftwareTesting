package com.jike.logistics.controller;

import cn.hutool.core.collection.CollUtil;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.logistics.domain.dto.ConsumeDTO;
import com.jike.logistics.domain.dto.GoodsDTO;
import com.jike.logistics.domain.dto.NewConsumeDTO;
import com.jike.logistics.domain.po.Consume;
import com.jike.logistics.domain.po.Goods;
import com.jike.logistics.service.IConsumeService;
import com.jike.logistics.service.IGoodsService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Api(tags = "消耗相关接口")
@RestController
@RequestMapping("api/Consume")
@RequiredArgsConstructor
@CrossOrigin
public class ConsumeController {
    private final IConsumeService consumeService;
    private final IGoodsService goodsService;

    @ApiOperation("返回所有物资信息（若干条）")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<Consume> consumeList = consumeService.list();
        if (CollUtil.isEmpty(consumeList)) {
            return ResponseResult.success();
        }
        return ResponseResult.success(BeanUtils.copyList(consumeList, ConsumeDTO.class));
    }

    @ApiOperation("通过物资id获取信息（若干条）")
    @GetMapping("GetByGoodsId/{id}")
    public ResponseResult<?> getConsumeInfoByGoodsId(@Param("id") @PathVariable("id") int id) {
        QueryWrapper<Consume> queryWrapper = new QueryWrapper<Consume>()
                .eq("GOODS_ID", id);
        List<Consume> consumeList = consumeService.list(queryWrapper);
        if (CollUtil.isEmpty(consumeList)) {
            return ResponseResult.success();
        }
        return ResponseResult.success(BeanUtils.copyList(consumeList, ConsumeDTO.class));
    }

    @ApiOperation("通过主码删除")
    @DeleteMapping("Delete/{consumeId}")
    public ResponseResult<?> deleteConsumeInfo(@Param("consumeId") @PathVariable("consumeId") int consumeId) {
        QueryWrapper<Consume> queryWrapper = new QueryWrapper<Consume>()
                .eq("CONSUME_ID", consumeId);
        if (consumeService.remove(queryWrapper)) {
            return ResponseResult.success("删除成功");
        }
        return ResponseResult.fail("找不到物资");
    }

    @ApiOperation("新建消耗")
    @PostMapping("Add")
    public ResponseResult<?> addConsumeInfo(@RequestBody NewConsumeDTO consumeDTO) {
        Consume consume = BeanUtils.copyBean(consumeDTO, Consume.class);
        Goods goods = goodsService.getById(consume.getGoodsId());
        if (goods != null) {
            int nowInventory=goods.getInventory() - consume.getConsumeAmount();
            if(nowInventory>=0) {
                goods.setInventory(nowInventory);
                if (goodsService.updateById(goods) && consumeService.save(consume))
                    return ResponseResult.success("新增成功");
            }
        }
        return ResponseResult.fail("新增失败");
    }
}
