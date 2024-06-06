package com.jike.frontdesk;

import com.github.jeffreyning.mybatisplus.conf.EnableMPP;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.openfeign.EnableFeignClients;

@MapperScan("com.jike.frontdesk.mapper")
@SpringBootApplication
@EnableMPP
@EnableFeignClients
public class FrontDeskApplication {
    public static void main(String[] args) {
        SpringApplication.run(FrontDeskApplication.class, args);
    }
}
