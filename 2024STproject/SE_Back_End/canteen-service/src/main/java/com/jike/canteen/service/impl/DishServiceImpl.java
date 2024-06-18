package com.jike.canteen.service.impl;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.canteen.domain.dto.DishDTO;
import com.jike.canteen.domain.dto.NewDishDTO;
import com.jike.canteen.domain.po.Dish;
import com.jike.canteen.mapper.DishMapper;
import com.jike.canteen.service.IDishService;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.dao.DataAccessException;

import java.util.List;


@Service
public class DishServiceImpl extends ServiceImpl<DishMapper, Dish> implements IDishService{
    public ResponseResult<Void> deleteDishById(@PathVariable("DishId") int dishId) {

            boolean isDeleted = this.removeById(dishId);
            if (isDeleted) {
                return ResponseResult.success("成功删除");

            }
            return ResponseResult.fail("删除菜品失败");

    }
    public ResponseResult<?> getAll() {
        List<Dish> dishes = this.list();
        if (CollUtils.isEmpty(dishes)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(dishes, DishDTO.class));
    }
    public ResponseResult<String> updateDish(@RequestBody DishDTO DishDTO) {

            if(DishDTO.getDishName()==null || DishDTO.getDishName().equals("")){
                return ResponseResult.fail("修改失败");
            }
            if(DishDTO.getDishTaste()==null || DishDTO.getDishTaste().equals("")){
                return ResponseResult.fail("修改失败");
            }

            if(DishDTO.getDishPrice()<=0){
                return ResponseResult.fail("修改失败");
            }
            Dish dish = BeanUtils.copyBean(DishDTO, Dish.class);
            if (this.updateById(dish))
                return ResponseResult.success("修改成功");
            return ResponseResult.fail("修改失败");


    }
    public ResponseResult<String> addDish(@RequestBody NewDishDTO newDishDTO) {

            if(newDishDTO.getDishPrice()<=0){
                return ResponseResult.fail("新增失败");
            }
            Dish dish = BeanUtils.copyBean(newDishDTO, Dish.class);
            if (this.save(dish))
                return ResponseResult.success("新增成功");
            return ResponseResult.fail("新增失败");

    }
}
