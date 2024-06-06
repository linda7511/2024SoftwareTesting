package com.jike.canteen.domain.dto;


import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "菜品实体")
public class DishDTO {

    @ApiModelProperty("菜品id")
    private int dishId;
    @ApiModelProperty("菜品名称")
    private String dishName;
    @ApiModelProperty("菜品价格")
    private double dishPrice;
    @ApiModelProperty("菜品口味")
    private String dishTaste;
}
