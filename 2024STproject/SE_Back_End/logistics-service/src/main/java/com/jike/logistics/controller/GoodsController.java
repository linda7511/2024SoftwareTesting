package com.jike.logistics.controller;

import cn.hutool.core.collection.CollUtil;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.logistics.domain.dto.GoodsDTO;
import com.jike.logistics.domain.po.Goods;
import com.jike.logistics.service.IGoodsService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.stream.Collectors;

@Api(tags = "物资相关接口")
@RestController
@RequestMapping("api/Good")
@RequiredArgsConstructor
@CrossOrigin
public class GoodsController {
    private final IGoodsService goodsService;

    @ApiOperation("通过主码查")
    @GetMapping("Get/{id}")
    public ResponseResult<?> getGoodsById(@Param("id") @PathVariable("id") int id) {
        Goods goods = goodsService.getById(id);
        if (goods != null) {
            return ResponseResult.success(BeanUtils.copyBean(goods, GoodsDTO.class));
        }
        return ResponseResult.fail("找不到物资");
    }

    @ApiOperation("通过物资名查询")
    @GetMapping("GetByName/{name}")
    public ResponseResult<List<GoodsDTO>> getGoodsByName(@Param("name") @PathVariable("name") String name) {
        QueryWrapper<Goods> queryWrapper = new QueryWrapper<Goods>()
                .eq("GOODS_NAME", name);
        List<Goods> goodsList = goodsService.list(queryWrapper);
        if (goodsList != null && !goodsList.isEmpty()) {
            List<GoodsDTO> goodsDTOList = goodsList.stream()
                    .map(goods -> BeanUtils.copyBean(goods, GoodsDTO.class))
                    .collect(Collectors.toList());
            return ResponseResult.success(goodsDTOList);
        }
        return ResponseResult.fail("找不到物资");
    }

    @ApiOperation("返回所有物资信息（若干条）")
    @GetMapping("GetAllInfo")
    public ResponseResult<?> getAllInfo() {
        List<Goods> goodsList = goodsService.list();
        if (CollUtil.isEmpty(goodsList)) {
            return ResponseResult.success();
        }
        return ResponseResult.success(BeanUtils.copyList(goodsList, GoodsDTO.class));
    }

    @ApiOperation("更新物资")
    @PutMapping("Update")
    public ResponseResult<?> updateGoods(@RequestBody GoodsDTO goodsDTO) {
        Goods goods = BeanUtils.copyBean(goodsDTO, Goods.class);
        if (goodsService.updateById(goods))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }

    @ApiOperation("通过主码删除")
    @DeleteMapping("Delete/{id}")
    public ResponseResult<?> deleteGoods(@Param("id") @PathVariable("id") int id) {

        if (goodsService.removeById(id)) {
            return ResponseResult.success("删除成功");
        }
        return ResponseResult.fail("找不到物资");
    }
}
