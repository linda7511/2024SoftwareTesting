

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (1, '章北海', 'zbh', '男', 42, 1, STR_TO_DATE('22-07-23', '%d-%m-%y'), '13269664877', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268381');

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (2, '罗辑', 'lj', '男', 31, 2, STR_TO_DATE('13-12-22', '%d-%m-%y'), '13736984895', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268382');

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (3, '叶文洁', 'ywj', '女', 45, 3, STR_TO_DATE('15-04-18', '%d-%m-%y'), '13712345678', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268383');

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (4, '汪淼', 'wm', '男', 39, 4, STR_TO_DATE('23-08-18', '%d-%m-%y'), '15823456789', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268384');

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (5, '杨冬', 'yd', '女', 43, 5, STR_TO_DATE('10-02-19', '%d-%m-%y'), '18298765432', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268385');

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (6, '程心', 'cx', '女', 28, 6, STR_TO_DATE('28-06-19', '%d-%m-%y'), '15087654321', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268386');

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (7, '韩大为', 'hdw', '男', 33, 7, STR_TO_DATE('03-01-22', '%d-%m-%y'), '13744443333', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268387');

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (8, '陈红秀', 'chx', '女', 33, 8, STR_TO_DATE('03-01-22', '%d-%m-%y'), '13769857423', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268388');

Insert into EMPLOYEE (EMPLOYEE_ID, NAME, ACCOUNT, SEX, AGE, POST_ID, ENTRY_TIME, PHONE_NUMBER, BASE_PAY, PASSWORD, BANK_NAME, CREDIT_CARD_NUMBER) 
values (9, '郑有为', 'zyw', '男', 38, 8, STR_TO_DATE('03-01-22', '%d-%m-%y'), '15870104865', 0, 'e10adc3949ba59abbe56e057f20f88', '中国农业银行', '6217009670700268389');

INSERT INTO APPLICATION (APPLICATION_ID, APPLICATION_TIME, APPLICATION_TYPE, APPLICATION_CONTENT, IF_APPROVE, EMPLOYEE_ID, DEPARTMENT_ID) 
VALUES 
(1, '2023-07-25', '请假一天', '因家庭原因需要请假一天', '是', 1, 1),
(2, '2023-07-26', '请假半天', '上午有事情，下午需要请假', '是', 1, 2),
(3, '2023-08-02', '调休', '希望将本周六的加班调休到下周', '是', 1, 2),
(4, '2023-08-08', '请假半天', '下午身体不适，需要请假', '是', 1, 3),
(5, '2023-08-15', '请假一天', '明天有紧急家庭事务，请求请假', '是', 1, 3),
(6, '2023-08-22', '调休', '因上周末加班，希望调休到本周', '是', 1, 4),
(7, '2023-09-01', '请假一天', '感冒发烧，不适于工作，请求请假', '是', 1, 4),
(8, '2023-09-10', '请假半天', '上午有个重要会议，下午需要请假', '是', 1, 5),
(9, '2023-09-18', '调休', '希望将加班调休到下周一', '是', 1, 5),
(10, '2023-09-25', '请假一天', '明天有个紧急项目需要处理，请求请假', '是', 1, 6);
