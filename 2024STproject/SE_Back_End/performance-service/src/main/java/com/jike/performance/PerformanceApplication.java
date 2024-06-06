package com.jike.performance;

import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@MapperScan("com.jike.performance.mapper")
@SpringBootApplication
public class PerformanceApplication {
    public static void main(String[] args) {
        SpringApplication.run(PerformanceApplication.class, args);
    }
}
