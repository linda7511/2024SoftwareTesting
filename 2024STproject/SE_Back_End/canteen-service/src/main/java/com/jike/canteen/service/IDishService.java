package com.jike.canteen.service;


import com.baomidou.mybatisplus.extension.service.IService;
import com.github.jeffreyning.mybatisplus.service.IMppService;
import com.jike.canteen.domain.dto.DishDTO;
import com.jike.canteen.domain.dto.NewDishDTO;
import com.jike.canteen.domain.po.Dish;
import com.jike.common.utils.ResponseResult;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;

public interface IDishService extends IService<Dish> {
    public ResponseResult<Void> deleteDishById(@PathVariable("DishId") int dishId);
    public ResponseResult<?> getAll();
    public ResponseResult<String> updateDish(@RequestBody DishDTO DishDTO);
    public ResponseResult<String> addDish(@RequestBody NewDishDTO newDishDTO);
}
