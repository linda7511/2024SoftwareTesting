package com.jike.canteen.service.impl;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import com.jike.canteen.domain.dto.MyTableDTO;
import com.jike.canteen.domain.dto.NewMyTableDTO;
import com.jike.canteen.domain.po.MyOrder;
import com.jike.canteen.domain.po.MyTable;
import com.jike.canteen.mapper.MyOrderMapper;
import com.jike.canteen.mapper.MyTableMapper;
import com.jike.canteen.service.IMyTableService;
import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;

import java.util.List;

@Service
public class MyTableServiceImpl extends ServiceImpl<MyTableMapper, MyTable> implements IMyTableService {

    private final MyTableMapper myTableMapper;
    private final MyOrderMapper myOrderMapper;

    public MyTableServiceImpl(MyTableMapper myTableMapper, MyOrderMapper myOrderMapper) {
        this.myTableMapper = myTableMapper;
        this.myOrderMapper = myOrderMapper;
    }


    private boolean isValidtableStatus(String tableStatus) {
        return "空闲".equals(tableStatus) || "使用中".equals(tableStatus)|| "故障".equals(tableStatus);
    }

    private boolean isValidtableType(String tableType) {
        return "小桌".equals(tableType) || "中桌".equals(tableType)|| "大桌".equals(tableType)|| "双人座".equals(tableType);
    }

    public ResponseResult<Void> deleteTableById(@PathVariable("tableId") int tableId) {
        QueryWrapper<MyOrder> queryWrapper = new QueryWrapper<>();
        queryWrapper.eq("table_id", tableId);
        int associatedOrdersCount = Math.toIntExact(myOrderMapper.selectCount(queryWrapper));

        if (associatedOrdersCount > 0) {
            return ResponseResult.fail("删除失败：该桌位存在关联订单，无法删除");
        }
        boolean isDeleted = myTableMapper.deleteById(tableId) > 0;
        if (isDeleted) {
            return ResponseResult.success("成功删除");
        }
        return ResponseResult.fail("删除桌位失败");
    }

    public ResponseResult<List<MyTableDTO>> getAllTables() {
        List<MyTable> myTables = myTableMapper.selectList(null);
        if (CollUtils.isEmpty(myTables)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(myTables, MyTableDTO.class));
    }

    public ResponseResult<String> updateTable(@RequestBody MyTableDTO myTableDTO) {
        if (myTableDTO.getTableId() == 0 || myTableDTO.getTableLocation() == null || myTableDTO.getNote() == null ||
                !isValidtableStatus(myTableDTO.getTableStatus()) || !isValidtableType(myTableDTO.getTableType())) {
            return ResponseResult.fail("修改失败");
        }
        MyTable myTable = BeanUtils.copyBean(myTableDTO, MyTable.class);
        if (myTableMapper.updateById(myTable) > 0) {
            return ResponseResult.success("修改成功");
        }
        return ResponseResult.fail("修改失败");
    }

    public ResponseResult<String> addTable(@RequestBody NewMyTableDTO newMyTableDTO) {
        if (newMyTableDTO.getTableLocation() == null || newMyTableDTO.getNote() == null ||
                !isValidtableStatus(newMyTableDTO.getTableStatus()) || !isValidtableType(newMyTableDTO.getTableType())) {
            return ResponseResult.fail("新增失败");
        }

        MyTable myTable = BeanUtils.copyBean(newMyTableDTO, MyTable.class);
        if (myTableMapper.insert(myTable) > 0) {
            return ResponseResult.success("新增成功");
        }
        return ResponseResult.fail("新增失败");
    }

    public ResponseResult<String> freeTable(@PathVariable("tableId") int tableId) {
        MyTable myTable = myTableMapper.selectById(tableId);
        if (myTable != null) {
            myTable.setTableStatus("空闲");
            if (myTableMapper.updateById(myTable) > 0) {
                return ResponseResult.success("修改成功");
            }
        }
        return ResponseResult.fail("修改失败");
    }
}
