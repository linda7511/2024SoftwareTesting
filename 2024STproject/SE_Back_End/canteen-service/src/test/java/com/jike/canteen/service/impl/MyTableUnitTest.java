package com.jike.canteen.service.impl;

import com.jike.canteen.controller.CombinedController;
import com.jike.canteen.domain.dto.*;
import com.jike.canteen.service.IMyTableService;
import com.jike.common.utils.ResponseResult;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.Rollback;
import org.springframework.transaction.annotation.Transactional;

import static org.junit.jupiter.api.Assertions.assertEquals;

import io.qameta.allure.Step;

@Transactional
@Rollback
@SpringBootTest
public class MyTableUnitTest {

    @Autowired
    private CombinedController combinedController;

    @Autowired
    private IMyTableService myTableService;

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/BoundaryAddTrue.csv", numLinesToSkip = 1)
    @DisplayName("边界值测试，添加成功")
    public void registerNewTable_True(int capacity,String tabletype,String tablelocation,
                               String tableStatus,String note,int bookable, int available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增成功", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/BoundaryAddFalse.csv", numLinesToSkip = 1)
    @DisplayName("边界值测试，添加成功")
    public void registerNewTable_False(int capacity,String tabletype,String tablelocation,
                                      String tableStatus,String note,int bookable, int available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/BoundaryUpdateTrue.csv", numLinesToSkip = 1)
    @DisplayName("边界值测试，修改成功")
    public void amendTableInfo_True(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                       String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改成功", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/BoundaryUpdateFalse.csv", numLinesToSkip = 1)
    @DisplayName("边界值测试，修改失败")
    public void amendTableInfo_False(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                    String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }



    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableAddSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试添加桌位成功")
    public void createNewTable(int capacity,String tabletype,String tablelocation,
                                   String tableStatus,String note,int bookable, int available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增成功", response.getMessage());
    }


    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableAddF1.csv", numLinesToSkip = 1)
    @DisplayName("测试添加桌位失败，参数为空")
    public void createTableWithEmptyParameters(Integer capacity,String tabletype,String tablelocation,
                              String tableStatus,String note,Integer bookable, Integer available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增失败", response.getMessage());
    }


    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableAddF2.csv", numLinesToSkip = 1)
    @DisplayName("测试添加桌位信息失败，tablestatus非法")
    public void registerNewTable_InvalidStatus(Integer capacity,String tabletype,String tablelocation,
                              String tableStatus,String note,Integer bookable, Integer available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableAddF3.csv", numLinesToSkip = 1)
    @DisplayName("测试添加桌位信息失败，tabletype非法")
    public void registerNewTable_InvalidType(Integer capacity,String tabletype,String tablelocation,
                              String tableStatus,String note,Integer bookable, Integer available) {
        NewMyTableDTO NewMyTableDTO = new NewMyTableDTO();
        NewMyTableDTO.setCapacity(capacity);
        NewMyTableDTO.setTableType(tabletype);
        NewMyTableDTO.setTableLocation(tablelocation);
        NewMyTableDTO.setTableStatus(tableStatus);
        NewMyTableDTO.setNote(note);
        NewMyTableDTO.setBookable(bookable);
        NewMyTableDTO.setAvailable(available);

        ResponseResult<?> response = myTableService.addTable(NewMyTableDTO);
        assertEquals("新增失败", response.getMessage());
    }




    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateF1.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位失败，主键冲突")
    public void amendTableInfo_DuplicateKey(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                     String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateF2.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位失败，tablestatus非法")
    public void amendTableInfo_InvalidStatus(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                     String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateF3.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位失败，tabletype非法")
    public void amendTableInfo_InvalidType(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                     String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateF4.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位失败，参数为空")
    public void amendTableInfo_EmptyParameters(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                     String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/MyTableUpdateSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试修改桌位信息成功")
    public void amendTableInfo_Success(Integer tableId,Integer capacity,String tabletype,String tablelocation,
                                          String tableStatus,String note,Integer bookable, Integer available) {
        MyTableDTO MyTableDTO = new MyTableDTO();
        MyTableDTO.setTableId(tableId != null ? tableId : 0);
        MyTableDTO.setCapacity(capacity);
        MyTableDTO.setTableType(tabletype);
        MyTableDTO.setTableLocation(tablelocation);
        MyTableDTO.setTableStatus(tableStatus);
        MyTableDTO.setNote(note);
        MyTableDTO.setBookable(bookable);
        MyTableDTO.setAvailable(available);

        ResponseResult<?> updateResponse = myTableService.updateTable(MyTableDTO);
        assertEquals("修改成功", updateResponse.getMessage());
    }

    // @Step("测试删除存在的桌位 [{tableId}]")
    // @ParameterizedTest
    // @CsvFileSource(resources = "/TableDeleteSuccess.csv", numLinesToSkip = 1)
    // @DisplayName("测试删除存在的桌位")
    // public void removeExistingTable(int tableId) {
    //     ResponseResult<Void> response = myTableService.deleteTableById(tableId);
    //     assertEquals("成功删除", response.getMessage());
    // }

    @Step("测试删除不存在的桌位 [{tableId}]")
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/TableDeleteF1.csv", numLinesToSkip = 1)
    @DisplayName("删除桌位失败，桌位不存在")
    public void removeNonExistentTable(int tableId) {
        ResponseResult<Void> response = combinedController.deleteTableById(tableId);
        assertEquals("删除桌位失败", response.getMessage());
    }

    @Step("测试删除存在外键约束的桌位 [{tableId}]")
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/MyTable/TableDeleteF2.csv", numLinesToSkip = 1)
    @DisplayName("删除桌位失败，存在外键约束")
    public void removeTableWithForeignKeyConstraint(int tableId) {
        ResponseResult<Void> response = combinedController.deleteTableById(tableId);
        assertEquals("删除失败：该桌位存在关联订单，无法删除", response.getMessage());
    }

}
