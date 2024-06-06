Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (1,10,'大桌','16B','空闲','适用于10人以上',1,1);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (2,10,'大桌','16A','空闲','适用于10人以上',1,1);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (3,6,'中桌','8A','使用中','生日宴会',1,0);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (4,4,'小桌','10C','空闲','适用于3-5人',1,1);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (5,2,'双人座','4B','故障','椅子损坏，暂停使用',0,0);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (6,10,'大桌','12D','使用中','婚礼宴请',1,0);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (7,6,'中桌','6C','空闲','适用于6-8人',1,1);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (8,4,'小桌','9A','使用中','商务会议',1,0);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (9,2,'双人座','3D','空闲','适用于1-2人',1,1);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (10,10,'大桌','15C','空闲','此桌位适用于公司团建',1,1);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (11,6,'中桌','7B','故障','桌面有划痕',0,0);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (12,4,'小桌','11A','使用中','生日庆祝',1,0);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (13,2,'双人座','5C','空闲','适用于1-2人',1,1);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (14,10,'大桌','14D','空闲','适用于8-12人',1,1);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (15,6,'中桌','8B','使用中','聚餐',1,0);
Insert into MYTABLE (TABLE_ID,CAPACITY,TABLE_TYPE,TABLE_LOCATION,TABLE_STATUS,NOTE,BOOKABLE,AVAILABLE) values (16,4,'小桌','10A','空闲','适用于3-5人',1,1);



Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (1,'奶油蘑菇汤',12,'甜');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (2,'牛肉面',18,'咸鲜');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (3,'香辣虾',28,'辣鲜');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (4,'红烧猪蹄',38,'甜咸');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (5,'麻辣香锅',68,'辣麻');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (6,'清蒸鲈鱼',58,'清淡');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (7,'蚝油芥兰',22,'鲜香');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (8,'宫保鸡丁',32,'微辣');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (9,'水煮鱼',48,'麻辣');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (10,'扬州炒饭',25,'咸鲜');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (11,'豆腐脑',15,'清淡');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (12,'干炒牛河',26,'咸鲜');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (13,'蒜蓉西兰花',20,'蒜香');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (14,'京酱肉丝',28,'甜咸');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (15,'麻辣火锅',88,'辣麻');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (16,'糖醋排骨',35,'甜酸');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (17,'麻辣拌鸡块',32,'麻辣');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (18,'清炒时蔬',18,'清淡');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (19,'鱼香茄子',22,'鱼香');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (20,'蒸蛋',15,'清淡');
Insert into DISH (DISH_ID,DISH_NAME,DISH_PRICE,DISH_TASTE) values (21,'黄焖鸡米饭',28,'微辣');




INSERT INTO MYORDER (TABLE_ID, DISH_ID, ORDER_TIME, CONSUMPTION_RECORD_ID, ORDER_STATUS) 
VALUES 
(6, 15, '2022-11-10 13:45:00', NULL, '点单成功'),
(9, 21, '2022-11-10 15:00:00', NULL, '点单成功'),
(12, 5, '2022-11-10 16:15:00', NULL, '点单成功'),
(15, 11, '2022-11-10 17:30:00', NULL, '点单成功'),
(3, 17, '2022-11-10 18:45:00', NULL, '点单成功'),
(6, 6, '2022-11-10 20:00:00', NULL, '点单成功'),
(9, 13, '2022-11-10 21:15:00', NULL, '点单成功'),
(12, 19, '2022-11-10 22:30:00', NULL, '点单成功'),
(15, 9, '2022-11-10 23:45:00', NULL, '点单成功'),
(3, 3, '2022-11-11 00:00:00', NULL, '点单成功'),
(6, 20, '2022-11-11 01:15:00', NULL, '点单成功'),
(9, 14, '2022-11-11 02:30:00', NULL, '点单成功'),
(12, 7, '2022-11-11 03:45:00', NULL, '点单成功'),
(15, 12, '2022-11-11 04:00:00', NULL, '点单成功'),
(3, 18, '2022-11-11 05:15:00', NULL, '点单成功'),
(6, 8, '2022-11-11 06:30:00', NULL, '点单成功'),
(9, 1, '2022-11-11 07:45:00', NULL, '点单成功'),
(12, 16, '2022-11-11 09:00:00', NULL, '点单成功'),
(15, 10, '2022-11-11 10:15:00', NULL, '点单成功'),
(3, 4, '2022-11-11 11:30:00', NULL, '点单成功'),
(6, 19, '2022-11-11 12:45:00', NULL, '点单成功'),
(9, 13, '2022-11-11 14:00:00', NULL, '点单成功'),
(12, 7, '2022-11-11 15:15:00', NULL, '点单成功'),
(15, 11, '2022-11-11 16:30:00', NULL, '点单成功'),
(3, 15, '2022-11-11 17:45:00', NULL, '点单成功'),
(6, 2, '2022-11-11 19:00:00', NULL, '点单成功'),
(9, 18, '2022-11-11 20:15:00', NULL, '点单成功'),
(12, 9, '2022-11-11 21:30:00', NULL, '点单成功'),
(15, 3, '2022-11-11 22:45:00', NULL, '点单成功'),
(3, 12, '2022-11-12 00:00:00', NULL, '点单成功'),
(6, 6, '2022-11-12 01:15:00', NULL, '点单成功'),
(9, 21, '2022-11-12 02:30:00', NULL, '点单失败'),
(12, 16, '2022-11-12 03:45:00', NULL, '点单成功'),
(15, 9, '2022-11-12 05:00:00', NULL, '点单成功'),
(3, 4, '2022-11-12 06:15:00', NULL, '点单成功'),
(6, 19, '2022-11-12 07:30:00', NULL, '点单失败'),
(9, 13, '2022-11-12 08:45:00', NULL, '点单成功'),
(12, 7, '2022-11-12 10:00:00', NULL, '点单成功'),
(15, 11, '2022-11-12 11:15:00', NULL, '点单成功'),
(3, 10, '2022-11-13 12:30:00', NULL, '点单成功'),
(6, 15, '2022-11-13 13:45:00', NULL, '点单成功'),
(9, 21, '2022-11-13 15:00:00', NULL, '点单成功'),
(12, 5, '2022-11-13 16:15:00', NULL, '点单成功'),
(15, 11, '2022-11-13 17:30:00', NULL, '点单成功'),
(3, 17, '2022-11-13 18:45:00', NULL, '点单成功'),
(6, 6, '2022-11-13 20:00:00', NULL, '点单成功'),
(9, 13, '2022-11-13 21:15:00', NULL, '点单成功'),
(12, 19, '2022-11-13 22:30:00', NULL, '点单成功'),
(15, 9, '2022-11-13 23:45:00', NULL, '点单成功'),
(3, 3, '2022-11-14 00:00:00', NULL, '点单成功'),
(6, 20, '2022-11-14 01:15:00', NULL, '点单成功'),
(9, 14, '2022-11-14 02:30:00', NULL, '点单成功'),
(12, 7, '2022-11-14 03:45:00', NULL, '点单成功'),
(15, 12, '2022-11-14 04:00:00', NULL, '点单成功'),
(3, 18, '2022-11-14 05:15:00', NULL, '点单成功'),
(6, 8, '2022-11-14 06:30:00', NULL, '点单成功'),
(9, 1, '2022-11-14 07:45:00', NULL, '点单成功'),
(12, 16, '2022-11-14 09:00:00', NULL, '点单成功'),
(15, 10, '2022-11-14 10:15:00', NULL, '点单成功'),
(3, 4, '2022-11-14 11:30:00', NULL, '点单成功'),
(6, 19, '2022-11-14 12:45:00', NULL, '点单成功'),
(9, 13, '2022-11-14 14:00:00', NULL, '点单成功'),
(12, 7, '2022-11-14 15:15:00', NULL, '点单成功'),
(15, 11, '2022-11-14 16:30:00', NULL, '点单成功'),
(3, 15, '2022-11-14 17:45:00', NULL, '点单成功'),
(6, 2, '2022-11-14 19:00:00', NULL, '点单成功'),
(9, 18, '2022-11-14 20:15:00', NULL, '点单成功'),
(12, 9, '2022-11-14 21:30:00', NULL, '点单成功'),
(15, 3, '2022-11-14 22:45:00', NULL, '点单成功'),
(3, 12, '2022-11-15 00:00:00', NULL, '点单成功'),
(6, 6, '2022-11-15 01:15:00', NULL, '点单成功'),
(9, 21, '2022-11-15 02:30:00', NULL, '点单失败'),
(12, 16, '2022-11-15 03:45:00', NULL, '点单成功'),
(15, 9, '2022-11-15 05:00:00', NULL, '点单成功'),
(3, 4, '2022-11-15 06:15:00', NULL, '点单成功'),
(6, 19, '2022-11-15 07:30:00', NULL, '点单成功'),
(9, 13, '2022-11-15 08:45:00', NULL, '点单成功'),
(12, 7, '2022-11-15 10:00:00', NULL, '点单成功'),
(15, 11, '2022-11-15 11:15:00', NULL, '点单成功');

INSERT INTO BOOK (TABLE_ID, CUSTOMER_ID, BOOK_TIME, BOOK_NUMBER, BOOK_STATUS, BOOK_REQUEMENT, BOOK_NOTE) 
VALUES 
(1, 1, '2023-01-15 12:30:00', 10, '预定成功', '素食', '客户要求素食菜单'),
(2, 2, '2022-09-10 18:45:00', 9, '预定成功', NULL, NULL),
(3, 3, '2023-03-20 20:00:00', 6, '预定成功', '无辣', NULL),
(4, 4, '2023-05-08 14:15:00', 4, '预定成功', NULL, '靠窗'),
(5, 5, '2023-07-01 19:30:00', 2, '预定成功', '素食', NULL),
(6, 6, '2022-11-18 21:00:00', 10, '预定成功', NULL, NULL),
(7, 7, '2022-12-10 17:45:00', 5, '预定成功', '生日蛋糕', NULL),
(8, 8, '2023-04-25 13:30:00', 4, '预定成功', NULL, NULL),
(9, 9, '2023-01-02 19:00:00', 2, '预定成功', '海鲜过敏', NULL),
(10, 10, '2023-07-12 14:00:00', 10, '预定成功', NULL, '靠窗'),
(11, 11, '2022-09-30 18:30:00', 6, '预定成功', '无葱', NULL),
(12, 12, '2023-03-05 20:15:00', 4, '预定成功', NULL, NULL),
(13, 13, '2023-05-23 12:00:00', 2, '预定成功', NULL, '靠窗'),
(14, 14, '2022-11-08 19:30:00', 10, '预定成功', '素食', NULL),
(15, 15, '2023-02-18 17:45:00', 5, '预定成功', NULL, NULL),
(16, 16, '2023-06-15 14:30:00', 4, '预定失败', '无辣', '客户要求不要辣椒'),
(6, 24, '2023-09-12 18:30:00', 2, '预订成功', '素食', '客户需要素食菜单'),
(12, 41, '2023-09-15 20:00:00', 4, '预定成功', '无海鲜', '客户对海鲜过敏'),
(1, 33, '2023-09-18 19:15:00', 10, '预定成功', '无鸡肉', '客户不吃鸡肉'),
(9, 29, '2023-09-21 17:45:00', 2, '预定失败', '无花生', '客户过敏'),
(16, 47, '2023-09-24 12:30:00', 4, '预定成功', '无辣', '客户不吃辣'),
(4, 22, '2023-09-27 14:00:00', 6, '预定成功', '无葱蒜', '客户忌口'),
(7, 36, '2023-09-13 19:45:00', 6, '预定成功', '无乳制品', '客户乳糖不耐受'),
(14, 17, '2023-09-16 21:30:00', 10, '预定成功', '无坚果', '客户过敏'),
(10, 44, '2023-09-19 13:15:00', 10, '预定成功', '无鱼类', '客户不吃鱼'),
(3, 26, '2023-09-22 15:00:00', 6, '预定成功', '无辣', '客户不吃辣');
