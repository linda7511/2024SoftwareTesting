CREATE TABLE SALARY (
    SALARY_ID INT AUTO_INCREMENT PRIMARY KEY,
    EMPLOYEE_ID INT,
    YEAR INT NOT NULL,
    MONTH INT NOT NULL,
    BONUS DECIMAL(12, 2) CHECK (BONUS >= 0),
    HOLIDAY_ALLOWANCE DECIMAL(12, 2) CHECK (HOLIDAY_ALLOWANCE >= 0),
    OTHER_ALLOWANCE DECIMAL(12, 2) CHECK (OTHER_ALLOWANCE >= 0),
    COMMISSION DECIMAL(12, 2) CHECK (COMMISSION >= 0),
    YEAR_END_BONUS DECIMAL(12, 2) CHECK (YEAR_END_BONUS >= 0),
    OVERTIME_PAY DECIMAL(12, 2) CHECK (OVERTIME_PAY >= 0),
    REWARD_TYPE VARCHAR(20) NOT NULL,
    REWARD_AMOUNT DECIMAL(12, 2) CHECK (REWARD_AMOUNT >= 0),
    LATE_DEDUCTION DECIMAL(12, 2) CHECK (LATE_DEDUCTION >= 0),
    EARLY_DEPARTURE_DEDUCTION DECIMAL(12, 2) CHECK (EARLY_DEPARTURE_DEDUCTION >= 0),
    INCOME_TAX DECIMAL(12, 2) CHECK (INCOME_TAX >= 0),
    SOCIAL_INSURANCE DECIMAL(12, 2) CHECK (SOCIAL_INSURANCE >= 0),
    GROSS_SALARY DECIMAL(12, 2) CHECK (GROSS_SALARY >= 0),
    NET_SALARY DECIMAL(12, 2) CHECK (NET_SALARY >= 0)
);
CREATE TABLE ATTENDENCEINFORMATION (
    ATTENDANCE_ID INT AUTO_INCREMENT PRIMARY KEY,
    EMPLOYEE_ID INT,
    YEAR INT NOT NULL,
    MONTH INT NOT NULL,
    EXPECT_ATTENDANCE_DAYS INT CHECK (EXPECT_ATTENDANCE_DAYS >= 0),
    ACTUAL_ATTENDANCE_DAYS INT CHECK (ACTUAL_ATTENDANCE_DAYS >= 0),
    PERSONAL_LEAVE_DAYS INT CHECK (PERSONAL_LEAVE_DAYS >= 0),
    SICK_LEAVE_DAYS INT CHECK (SICK_LEAVE_DAYS >= 0),
    LATE_DAYS INT CHECK (LATE_DAYS >= 0),
    EARLY_DEPARTURE_DAYS INT CHECK (EARLY_DEPARTURE_DAYS >= 0),
    ABSENT_DAYS INT CHECK (ABSENT_DAYS >= 0)
);
CREATE TABLE CLOCKIN (
    CLOCKIN_ID INT AUTO_INCREMENT PRIMARY KEY,
    EMPLOYEE_ID INT,
    CHECKIN_TIME DATE NOT NULL
);

