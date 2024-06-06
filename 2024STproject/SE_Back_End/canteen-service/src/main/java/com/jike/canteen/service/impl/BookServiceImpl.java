package com.jike.canteen.service.impl;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.github.jeffreyning.mybatisplus.service.MppServiceImpl;
import com.jike.canteen.domain.dto.BookDTO;
import com.jike.canteen.domain.po.Book;
import com.jike.canteen.mapper.BookMapper;
import com.jike.canteen.service.IBookService;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.apache.ibatis.annotations.Param;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.*;
import org.springframework.dao.DataAccessException;


import java.util.List;
import java.util.Objects;


@Service
public class BookServiceImpl extends MppServiceImpl<BookMapper, Book> implements IBookService {

    public ResponseResult<?> addBookInfo(@RequestBody BookDTO bookDTO) {
        try {
            if(bookDTO.getBookNumber()<1){
                return ResponseResult.fail("新增失败");
            }
            if(!Objects.equals(bookDTO.getBookStatus(), "预订成功") && !Objects.equals(bookDTO.getBookStatus(), "预订失败")){
                return ResponseResult.fail("新增失败");
            }
            if (bookDTO.getCustomerId()<=0||bookDTO.getCustomerId()>50){
                return ResponseResult.fail("新增失败");
            }
            Book book = BeanUtils.copyBean(bookDTO, Book.class);
            if (this.save(book))
                return ResponseResult.success("新增成功");
            else{
                return ResponseResult.fail("新增失败");
            }
        } catch (DataAccessException e) {
            return ResponseResult.fail("新增失败");
        }
    }

    public ResponseResult<?> updateBookInfo(@RequestBody BookDTO BookDTO) {
        try {
            if(BookDTO.getBookNumber()<1){
                return ResponseResult.fail("修改失败");
            }
            if(!Objects.equals(BookDTO.getBookStatus(), "预订成功") && !Objects.equals(BookDTO.getBookStatus(), "预订失败")){
                return ResponseResult.fail("修改失败");
            }
            if (BookDTO.getCustomerId()<=0||BookDTO.getCustomerId()>50){
                return ResponseResult.fail("修改失败");
            }
            Book book = BeanUtils.copyBean(BookDTO, Book.class);
            if (this.updateByMultiId(book))
                return ResponseResult.success("修改成功");
            else{
                return ResponseResult.fail("修改失败");
            }
        } catch (DataAccessException e) {
            return ResponseResult.fail("修改失败");
        }
    }

    public ResponseResult<?> deleteBookInfo(@Param("桌位id") @PathVariable("TableId") int tableId,
                                            @Param("顾客id") @PathVariable("CustomerId") int customerId) {
        try {
            QueryWrapper<Book> queryWrapper = new QueryWrapper<Book>()
                    .eq("TABLE_ID", tableId)
                    .eq("CUSTOMER_ID", customerId);
            boolean isRemoved = this.remove(queryWrapper);
            if (isRemoved) {
                return ResponseResult.success("预订信息删除成功");
            }
            return ResponseResult.fail("找不到对应的预订信息，删除失败");
        }
        catch (DataAccessException e) {
            return ResponseResult.fail("找不到对应的预订信息，删除失败");
        }
    }

    public ResponseResult<?> getAll() {
        List<Book> books = this.list();
        if (CollUtils.isEmpty(books)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(books, BookDTO.class));
    }
}
