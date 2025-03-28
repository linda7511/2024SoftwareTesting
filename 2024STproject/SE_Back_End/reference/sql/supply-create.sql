
CREATE TABLE GOODS (
  GOODS_ID INT AUTO_INCREMENT PRIMARY KEY,
  CATEGORY VARCHAR(20),
  GOODS_NAME VARCHAR(20),
  INVENTORY INT
);

CREATE TABLE CONSUME (
    CONSUME_ID INT AUTO_INCREMENT PRIMARY KEY,
    DEPARTMENT_ID INT,
    GOODS_ID INT,
    CONSUME_AMOUNT INT,
    FOREIGN KEY (GOODS_ID) REFERENCES GOODS(GOODS_ID)
);

CREATE TABLE MAINTAIN (
  ROOM_ID INT,
  EMPLOYEE_ID INT,
  MAINTAINING_TIME TIMESTAMP,
  MAINTAINING_ITEM VARCHAR(20),
  MAINTAINING_RESULT VARCHAR(20),
  PRIMARY KEY (ROOM_ID, EMPLOYEE_ID, MAINTAINING_TIME)
);

CREATE TABLE PURCHASE (
  PURCHASE_ID INT AUTO_INCREMENT PRIMARY KEY,
  GOODS_ID INT,
  EMPLOYEE_ID INT,
  PURCHASE_DATE DATE,
  PURCHASE_QUANTITY INT,
  UNIT_PRICE DECIMAL(10, 2),
  FOREIGN KEY (GOODS_ID) REFERENCES GOODS(GOODS_ID)
);


