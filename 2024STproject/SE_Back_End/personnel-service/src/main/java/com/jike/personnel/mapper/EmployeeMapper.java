package com.jike.personnel.mapper;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import com.jike.personnel.domain.dto.EmployeeDetailsDTO;
import com.jike.personnel.domain.po.Employee;
import org.apache.ibatis.annotations.Result;
import org.apache.ibatis.annotations.Results;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

public interface EmployeeMapper extends BaseMapper<Employee> {
    @Select("SELECT d.DEPARTMENT_NAME FROM EMPLOYEE e " +
            "JOIN POST p ON e.POST_ID = p.POST_ID " +
            "JOIN DEPARTMENT d ON p.DEPARTMENT_ID = d.DEPARTMENT_ID " +
            "WHERE e.EMPLOYEE_ID = #{employeeId}")
    String selectDepartment(int roomNumber);

    // 添加的查询所有员工信息及其岗位名称和部门名称的方法
    @Select("SELECT e.*, p.POST_NAME, d.DEPARTMENT_NAME " +
            "FROM EMPLOYEE e " +
            "JOIN POST p ON e.POST_ID = p.POST_ID " +
            "JOIN DEPARTMENT d ON p.DEPARTMENT_ID = d.DEPARTMENT_ID")
    List<EmployeeDetailsDTO> selectAllEmployeeDetails();


    @Update("UPDATE DEPARTMENT SET NUMBER_OF_PEOPLE = NUMBER_OF_PEOPLE + 1 WHERE DEPARTMENT_ID = (SELECT DEPARTMENT_ID FROM POST WHERE POST_ID = #{postId})")
    int updateDepartmentPeopleCount(int postId);



}
