package com.jike.canteen.service.impl;

import com.jike.canteen.controller.CombinedController;
import com.jike.canteen.domain.dto.*;
import com.jike.common.utils.ResponseResult;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.Rollback;
import org.springframework.transaction.annotation.Transactional;

import com.jike.canteen.service.IDishService;

import static org.junit.jupiter.api.Assertions.assertEquals;

import io.qameta.allure.Step;

@Transactional
@Rollback
@SpringBootTest

public class DishUnitTest {
    @Autowired
    private IDishService dishService;
    @Autowired
    private CombinedController combinedController;

    @Step("测试成功删除菜品 [{dishId}]")
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishDeleteSuccess.csv", numLinesToSkip = 1)
    @DisplayName("成功删除菜品测试")
    public void removeExistingDish(int dishId) {
        ResponseResult<Void> response = combinedController.deleteDishById(dishId);
        assertEquals("成功删除", response.getMessage());
    }

    @Step("测试删除不存在的菜品 [{dishId}]")
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishDeleteF1.csv", numLinesToSkip = 1)
    @DisplayName("删除不存在的菜品测试")
    public void removeNonExistentDish(int dishId) {
        ResponseResult<Void> response = combinedController.deleteDishById(dishId);
        assertEquals("删除菜品失败", response.getMessage());
    }

    @Step("测试删除存在外键约束的菜品 [{dishId}]")
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishDeleteF2.csv", numLinesToSkip = 1)
    @DisplayName("删除有外键约束的菜品测试")
    public void removeDishWithForeignKeyConstraint(int dishId) {
        ResponseResult<Void> response = combinedController.deleteDishById(dishId);
        assertEquals("删除菜品失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishUpdateSuccess.csv", numLinesToSkip = 1)
    @DisplayName("成功更新菜品测试")
    public void modifyDishDetails(int dishId, String dishName, double dishPrice, String dishTaste) {
        DishDTO dishDTO = new DishDTO();
        dishDTO.setDishId(dishId);
        dishDTO.setDishName(dishName);
        dishDTO.setDishPrice(dishPrice);
        dishDTO.setDishTaste(dishTaste);

        ResponseResult<String> response = combinedController.updateDish(dishDTO);
        assertEquals("修改成功", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishUpdateF1.csv", numLinesToSkip = 1)
    @DisplayName("更新失败，参数为空测试")
    public void modifyDishWithEmptyParameters(Integer dishId, String dishName, Double dishPrice, String dishTaste) {
        DishDTO dishDTO = new DishDTO();
        dishDTO.setDishId(dishId != null ? dishId : 0);
        dishDTO.setDishName(dishName);
        dishDTO.setDishPrice(dishPrice != null ? dishPrice : 0.0);
        dishDTO.setDishTaste(dishTaste);

        ResponseResult<String> response = combinedController.updateDish(dishDTO);
        assertEquals("修改失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishUpdateF2.csv", numLinesToSkip = 1)
    @DisplayName("更新失败，价格非法测试")
    public void modifyDishWithInvalidPrice(int dishId, String dishName, double dishPrice, String dishTaste) {
        DishDTO dishDTO = new DishDTO();
        dishDTO.setDishId(dishId);
        dishDTO.setDishName(dishName);
        dishDTO.setDishPrice(dishPrice);
        dishDTO.setDishTaste(dishTaste);

        ResponseResult<String> response = combinedController.updateDish(dishDTO);
        assertEquals("修改失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishAddSuccess.csv", numLinesToSkip = 1)
    @DisplayName("成功添加菜品测试")
    public void createNewDish(String dishName, double dishPrice, String dishTaste) {
        NewDishDTO newDishDTO = new NewDishDTO();
        newDishDTO.setDishName(dishName);
        newDishDTO.setDishPrice(dishPrice);
        newDishDTO.setDishTaste(dishTaste);

        ResponseResult<String> response = combinedController.addDish(newDishDTO);
        assertEquals("新增成功", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishAddF1.csv", numLinesToSkip = 1)
    @DisplayName("添加失败，参数为空测试")
    public void createDishWithEmptyParameters(String dishName, Double dishPrice, String dishTaste) {
        NewDishDTO newDishDTO = new NewDishDTO();
        newDishDTO.setDishName(dishName);
        newDishDTO.setDishPrice(dishPrice != null ? dishPrice : 0.0);
        newDishDTO.setDishTaste(dishTaste);

        ResponseResult<String> response = combinedController.addDish(newDishDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Dish/DishAddF2.csv", numLinesToSkip = 1)
    @DisplayName("添加失败，价格非法测试")
    public void createDishWithInvalidPrice(String dishName, double dishPrice, String dishTaste) {
        NewDishDTO newDishDTO = new NewDishDTO();
        newDishDTO.setDishName(dishName);
        newDishDTO.setDishPrice(dishPrice);
        newDishDTO.setDishTaste(dishTaste);

        ResponseResult<String> response = combinedController.addDish(newDishDTO);
        assertEquals("新增失败", response.getMessage());
    }
}
