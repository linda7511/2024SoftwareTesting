package com.jike.canteen.service;

import com.github.jeffreyning.mybatisplus.service.IMppService;
import com.baomidou.mybatisplus.extension.service.IService;
import com.jike.canteen.domain.dto.MyTableDTO;
import com.jike.canteen.domain.dto.NewMyTableDTO;
import com.jike.canteen.domain.po.MyTable;
import com.jike.common.utils.ResponseResult;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;


import java.util.List;

public interface IMyTableService extends IService<MyTable> {
    // 根据桌号删除桌子
    public ResponseResult<Void> deleteTableById(@PathVariable("tableId") int tableId);

    // 获取所有桌子的信息
    public ResponseResult<List<MyTableDTO>> getAllTables();

    // 更新桌子的信息
    public ResponseResult<String> updateTable(@RequestBody MyTableDTO myTableDTO);

    // 添加新桌子
    public ResponseResult<String> addTable(@RequestBody NewMyTableDTO newMyTableDTO);

    // 空闲桌子
    public ResponseResult<String> freeTable(@PathVariable("tableId") int tableId);
}
