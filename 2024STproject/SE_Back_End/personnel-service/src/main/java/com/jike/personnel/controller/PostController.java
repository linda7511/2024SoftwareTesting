package com.jike.personnel.controller;


import com.jike.common.utils.BeanUtils;
import com.jike.common.utils.CollUtils;
import com.jike.common.utils.ResponseResult;
import com.jike.personnel.domain.dto.FirstPostDTO;
import com.jike.personnel.domain.dto.PostDTO;
import com.jike.personnel.domain.po.Post;
import com.jike.personnel.service.IPostService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Api(tags = "岗位相关接口")
@RestController
@RequestMapping("api/Post")
@RequiredArgsConstructor
@CrossOrigin
public class PostController {
    private final IPostService postService;


    @ApiOperation("查找所有岗位")
    @GetMapping("GetAll")
    public ResponseResult<?> getAll() {
        List<Post> posts = postService.list();
        if (CollUtils.isEmpty(posts)) {
            return ResponseResult.success(CollUtils.emptyList());
        }
        return ResponseResult.success(BeanUtils.copyList(posts, PostDTO.class));
    }

    @ApiOperation("更新岗位信息")
    @PutMapping("Update")
    public ResponseResult<?> updatePost(@RequestBody FirstPostDTO firstPostDTO) {
        Post post = postService.getById(firstPostDTO.getPostId());
        post.setPositionSalary(firstPostDTO.getPositionSalary());
        if (postService.updateById(post))
            return ResponseResult.success("修改成功");
        return ResponseResult.fail("修改失败");
    }
}
