package com.jike.canteen.service;

import com.github.jeffreyning.mybatisplus.service.IMppService;
import com.jike.canteen.domain.dto.BookDTO;
import com.jike.canteen.domain.po.Book;
import com.jike.common.utils.ResponseResult;
import org.apache.ibatis.annotations.Param;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;

public interface IBookService extends IMppService<Book> {
    public ResponseResult<?> addBookInfo(@RequestBody BookDTO BookDTO);
    public ResponseResult<?> updateBookInfo(@RequestBody BookDTO BookDTO);
    public ResponseResult<?> deleteBookInfo(@Param("桌位id") @PathVariable("TableId") int tableId,
                                            @Param("顾客id") @PathVariable("CustomerId") int customerId);
    public ResponseResult<?> getAll();
}
