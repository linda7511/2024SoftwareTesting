package com.jike.canteen;

import com.github.jeffreyning.mybatisplus.conf.EnableMPP;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.openfeign.EnableFeignClients;

@MapperScan("com.jike.canteen.mapper")
@SpringBootApplication
@EnableMPP
@EnableFeignClients
public class CanteenApplication {
    public static void main(String[] args) {
        SpringApplication.run(CanteenApplication.class, args);
    }
}
