package com.jike.canteen.service.impl;

import com.jike.canteen.controller.CombinedController;
import com.jike.canteen.domain.dto.*;
import com.jike.canteen.service.IBookService;
import com.jike.common.utils.ResponseResult;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.CsvFileSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.Rollback;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.assertEquals;

@Transactional
@Rollback
@SpringBootTest

public class BookUnitTest {
    @Autowired
    private IBookService bookService;
    @Autowired
    private CombinedController combinedController;

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BoundaryAddFalse.csv", numLinesToSkip = 1)
    @DisplayName("边界值测试，添加失败")
    public void registerNewBooking_False(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                                 String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId);
        bookDTO.setCustomerId(customerId);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> response = bookService.addBookInfo(bookDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BoundaryAddTrue.csv", numLinesToSkip = 1)
    @DisplayName("边界值测试，添加成功")
    public void registerNewBooking_True(int tableId, int customerId, LocalDateTime bookTime, int bookNumber,
                                           String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId);
        bookDTO.setCustomerId(customerId);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> response = bookService.addBookInfo(bookDTO);
        assertEquals("新增成功", response.getMessage());
    }
    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BoundaryUpdateFalse.csv", numLinesToSkip = 1)
    @DisplayName("边界值测试，修改失败")
    public void amendBookingInfo_False(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                               String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId != null ? tableId : 0);
        bookDTO.setCustomerId(customerId != null ? customerId : 0);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber != null ? bookNumber : 0);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> updateResponse = bookService.updateBookInfo(bookDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BoundaryUpdateTrue.csv", numLinesToSkip = 1)
    @DisplayName("边界值测试，修改成功")
    public void amendBookingInfo_True(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                         String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId != null ? tableId : 0);
        bookDTO.setCustomerId(customerId != null ? customerId : 0);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber != null ? bookNumber : 0);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> updateResponse = bookService.updateBookInfo(bookDTO);
        assertEquals("修改成功", updateResponse.getMessage());
    }






    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookAddSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试添加预订信息成功")
    public void registerNewBooking_Success(int tableId, int customerId, LocalDateTime bookTime, int bookNumber,
                                           String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId);
        bookDTO.setCustomerId(customerId);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> response = bookService.addBookInfo(bookDTO);
        assertEquals("新增成功", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookAddF1.csv", numLinesToSkip = 1)
    @DisplayName("测试添加预订信息失败，参数为空")
    public void registerNewBooking_EmptyParameters(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                                   String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId != null ? tableId : 0);
        bookDTO.setCustomerId(customerId != null ? customerId : 0);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber != null ? bookNumber : 0);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> response = bookService.addBookInfo(bookDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookAddF2.csv", numLinesToSkip = 1)
    @DisplayName("测试添加预订信息失败，外键不存在")
    public void registerNewBooking_ForeignKeyMissing(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                                     String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId);
        bookDTO.setCustomerId(customerId);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> response = bookService.addBookInfo(bookDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookAddF3.csv", numLinesToSkip = 1)
    @DisplayName("测试添加预订信息失败，bookstatus非法")
    public void registerNewBooking_InvalidStatus(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                                 String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId);
        bookDTO.setCustomerId(customerId);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> response = bookService.addBookInfo(bookDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookAddF4.csv", numLinesToSkip = 1)
    @DisplayName("测试添加预订信息失败，booknumber非法")
    public void registerNewBooking_InvalidNumber(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                                 String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId);
        bookDTO.setCustomerId(customerId);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> response = bookService.addBookInfo(bookDTO);
        assertEquals("新增失败", response.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookUpdateF1.csv", numLinesToSkip = 1)
    @DisplayName("测试修改预订信息失败，主键冲突")
    public void amendBookingInfo_DuplicateKey(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                              String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId != null ? tableId : 0);
        bookDTO.setCustomerId(customerId != null ? customerId : 0);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber != null ? bookNumber : 0);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> updateResponse = bookService.updateBookInfo(bookDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookUpdateF2.csv", numLinesToSkip = 1)
    @DisplayName("测试修改预订信息失败，bookStatus非法")
    public void amendBookingInfo_InvalidStatus(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                               String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId != null ? tableId : 0);
        bookDTO.setCustomerId(customerId != null ? customerId : 0);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber != null ? bookNumber : 0);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> updateResponse = bookService.updateBookInfo(bookDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookUpdateF3.csv", numLinesToSkip = 1)
    @DisplayName("测试修改预订信息失败，参数为空")
    public void amendBookingInfo_EmptyParameters(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                                 String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId != null ? tableId : 0);
        bookDTO.setCustomerId(customerId != null ? customerId : 0);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber != null ? bookNumber : 0);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> updateResponse = bookService.updateBookInfo(bookDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookUpdateF4.csv", numLinesToSkip = 1)
    @DisplayName("测试修改预订信息失败，booknumber非法")
    public void amendBookingInfo_InvalidNumber(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                               String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId != null ? tableId : 0);
        bookDTO.setCustomerId(customerId != null ? customerId : 0);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber != null ? bookNumber : 0);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> updateResponse = bookService.updateBookInfo(bookDTO);
        assertEquals("修改失败", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookUpdateSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试修改预订信息成功")
    public void amendBookingInfo_Success(Integer tableId, Integer customerId, LocalDateTime bookTime, Integer bookNumber,
                                         String bookStatus, String bookRequement, String bookNote) {
        BookDTO bookDTO = new BookDTO();
        bookDTO.setTableId(tableId != null ? tableId : 0);
        bookDTO.setCustomerId(customerId != null ? customerId : 0);
        bookDTO.setBookTime(bookTime);
        bookDTO.setBookNumber(bookNumber != null ? bookNumber : 0);
        bookDTO.setBookStatus(bookStatus);
        bookDTO.setBookRequement(bookRequement);
        bookDTO.setBookNote(bookNote);

        ResponseResult<?> updateResponse = bookService.updateBookInfo(bookDTO);
        assertEquals("修改成功", updateResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookDeleteSuccess.csv", numLinesToSkip = 1)
    @DisplayName("测试删除预订信息成功")
    public void cancelBooking_Success(Integer tableId, Integer customerId) {
        ResponseResult<?> deleteResponse = bookService.deleteBookInfo(tableId, customerId);
        assertEquals("预订信息删除成功", deleteResponse.getMessage());
    }

    @ParameterizedTest
    @CsvFileSource(resources = "/UnitTest/Book/BookDeleteF1.csv", numLinesToSkip = 1)
    @DisplayName("测试删除预订信息失败")
    public void cancelBooking_NotFound(Integer tableId, Integer customerId) {
        ResponseResult<?> deleteResponse = bookService.deleteBookInfo(tableId, customerId);
        assertEquals("找不到对应的预订信息，删除失败", deleteResponse.getMessage());
    }
}
