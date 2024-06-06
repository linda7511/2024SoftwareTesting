package com.jike.personnel.domain.dto;


import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
import com.baomidou.mybatisplus.annotation.TableId;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;

@Data
@ApiModel(description = "岗位实体")
public class PostDTO {

    @ApiModelProperty("岗位id")
    private int postId;


    @ApiModelProperty("岗位名称")
    private String postName;


    @ApiModelProperty("所属部门id")
    private int departmentId;



    @ApiModelProperty("岗位等级")
    private String positionLevel;


    @ApiModelProperty("岗位工资")
    private int positionSalary;


    @ApiModelProperty("缴费类型")
    private String paymentType;

    @ApiModelProperty("缴费基数")
    private String paymentBase;

    @ApiModelProperty("五险")
    private String insurance;

}
