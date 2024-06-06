package com.jike.canteen.domain.dto;

import io.swagger.annotations.ApiModel;
import lombok.Data;

import java.time.LocalDateTime;

@Data
@ApiModel(description = "辅助新建CateringRecord信息中的数组")
public class TempOrderDTO {
    private int tableId;
    private int dishId;
    private LocalDateTime orderTime;
}
