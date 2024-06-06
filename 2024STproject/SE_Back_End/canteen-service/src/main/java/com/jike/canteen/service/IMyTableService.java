package com.jike.canteen.service;

import com.baomidou.mybatisplus.extension.service.IService;
import com.jike.canteen.domain.dto.MyTableDTO;
import com.jike.canteen.domain.dto.NewMyTableDTO;
import com.jike.canteen.domain.po.MyTable;
import com.jike.common.utils.ResponseResult;

import java.util.List;

public interface IMyTableService extends IService<MyTable> {
    public ResponseResult<Void> deleteTableById(int tableId);
    public ResponseResult<List<MyTableDTO>> getAllTables();
    public ResponseResult<String> updateTable(MyTableDTO myTableDTO);
    public ResponseResult<String> addTable(NewMyTableDTO newMyTableDTO);
    public ResponseResult<String> freeTable(int tableId);
}
