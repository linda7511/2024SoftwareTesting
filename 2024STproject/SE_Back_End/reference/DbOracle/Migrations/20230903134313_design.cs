using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbOracle.Migrations
{
    /// <inheritdoc />
    public partial class design : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DESIGN");

            migrationBuilder.CreateTable(
                name: "BILL",
                schema: "DESIGN",
                columns: table => new
                {
                    BILL_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ROOM_COST = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    FOOD_COST = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    OTHER_COST = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    BILL_TYPE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    INVOICE_NUMBER = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008804", x => x.BILL_ID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER",
                schema: "DESIGN",
                columns: table => new
                {
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    ID = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    GENDER = table.Column<string>(type: "VARCHAR2(6)", unicode: false, maxLength: 6, nullable: true),
                    PHONE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    CREDIT_GRADE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    MEMBER_GRADE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008803", x => x.CUSTOMER_ID);
                });

            migrationBuilder.CreateTable(
                name: "DEPARTMENT",
                schema: "DESIGN",
                columns: table => new
                {
                    DEPARTMENT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DEPARTMENT_NAME = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    NUMBER_OF_PEOPLE = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008792", x => x.DEPARTMENT_ID);
                });

            migrationBuilder.CreateTable(
                name: "DISH",
                schema: "DESIGN",
                columns: table => new
                {
                    DISH_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DISH_NAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    DISH_PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    DISH_TASTE = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008802", x => x.DISH_ID);
                });

            migrationBuilder.CreateTable(
                name: "GOODS",
                schema: "DESIGN",
                columns: table => new
                {
                    GOODS_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CATEGORY = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    GOODS_NAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    INVENTORY = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008800", x => x.GOODS_ID);
                });

            migrationBuilder.CreateTable(
                name: "MYTABLE",
                schema: "DESIGN",
                columns: table => new
                {
                    TABLE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CAPACITY_NUM = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    TABLE_TYPE = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    TABLE_LOCATION = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    TABLE_STATUS = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    NOTE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    BOOKABLE = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    AVAILABLE = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008801", x => x.TABLE_ID);
                });

            migrationBuilder.CreateTable(
                name: "ROOMTYPE",
                schema: "DESIGN",
                columns: table => new
                {
                    TYPE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TYPE_NAME = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    NOTE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    REMAINING = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008806", x => x.TYPE_ID);
                });

            migrationBuilder.CreateTable(
                name: "SUPPLIER",
                schema: "DESIGN",
                columns: table => new
                {
                    SUPPLIER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SUPPLIER_PHONE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    ADDRESS = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008799", x => x.SUPPLIER_ID);
                });

            migrationBuilder.CreateTable(
                name: "UNIT_PRICE",
                schema: "DESIGN",
                columns: table => new
                {
                    PARKING_PLACE_TYPE = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: false),
                    MEMBER_PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    NOT_MEMBER_PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008805", x => x.PARKING_PLACE_TYPE);
                });

            migrationBuilder.CreateTable(
                name: "CAR",
                schema: "DESIGN",
                columns: table => new
                {
                    CAR_NUMBER = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: false),
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008828", x => x.CAR_NUMBER);
                    table.ForeignKey(
                        name: "SYS_C008829",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CUSTOMER",
                        principalColumn: "CUSTOMER_ID");
                });

            migrationBuilder.CreateTable(
                name: "COMPLAINT",
                schema: "DESIGN",
                columns: table => new
                {
                    COMPLAINT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    WAY = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    SUBMIT_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    COMPLAINT_TYPE = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    FEEDBACK = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    RESULT = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008819", x => x.COMPLAINT_ID);
                    table.ForeignKey(
                        name: "SYS_C008820",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CUSTOMER",
                        principalColumn: "CUSTOMER_ID");
                });

            migrationBuilder.CreateTable(
                name: "PAY",
                schema: "DESIGN",
                columns: table => new
                {
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    BILL_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    PAY_WAY = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    PAY_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    PAY_COUNT = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    STATUS = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008823", x => new { x.CUSTOMER_ID, x.BILL_ID });
                    table.ForeignKey(
                        name: "SYS_C008824",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CUSTOMER",
                        principalColumn: "CUSTOMER_ID");
                    table.ForeignKey(
                        name: "SYS_C008825",
                        column: x => x.BILL_ID,
                        principalSchema: "DESIGN",
                        principalTable: "BILL",
                        principalColumn: "BILL_ID");
                });

            migrationBuilder.CreateTable(
                name: "STAGING",
                schema: "DESIGN",
                columns: table => new
                {
                    LUGGAGE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    STAGING_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    FETCH_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    LUGGAGE_TYPE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008817", x => x.LUGGAGE_ID);
                    table.ForeignKey(
                        name: "SYS_C008818",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CUSTOMER",
                        principalColumn: "CUSTOMER_ID");
                });

            migrationBuilder.CreateTable(
                name: "ASSIGNMENT",
                schema: "DESIGN",
                columns: table => new
                {
                    ASSIGNMENT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DEPARTMENT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ASSIGNMENT_NAME = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    ASSIGNMENT_NOTE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008793", x => x.ASSIGNMENT_ID);
                    table.ForeignKey(
                        name: "SYS_C008794",
                        column: x => x.DEPARTMENT_ID,
                        principalSchema: "DESIGN",
                        principalTable: "DEPARTMENT",
                        principalColumn: "DEPARTMENT_ID");
                });

            migrationBuilder.CreateTable(
                name: "POST",
                schema: "DESIGN",
                columns: table => new
                {
                    POST_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    POST_NAME = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    DEPARTMENT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    AUTHORITY = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    POSITION_LEVEL = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    POSITION_SALARY = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    PAYMENT_TYPE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    PAYMENT_BASE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    INSURANCE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    ACCUMULATION_FUNDS = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008795", x => x.POST_ID);
                    table.ForeignKey(
                        name: "SYS_C008796",
                        column: x => x.DEPARTMENT_ID,
                        principalSchema: "DESIGN",
                        principalTable: "DEPARTMENT",
                        principalColumn: "DEPARTMENT_ID");
                });

            migrationBuilder.CreateTable(
                name: "CONSUME",
                schema: "DESIGN",
                columns: table => new
                {
                    DEPARTMENT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    GOODS_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    CONSUME_QUANTITY = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008868", x => new { x.DEPARTMENT_ID, x.GOODS_ID });
                    table.ForeignKey(
                        name: "SYS_C008869",
                        column: x => x.DEPARTMENT_ID,
                        principalSchema: "DESIGN",
                        principalTable: "DEPARTMENT",
                        principalColumn: "DEPARTMENT_ID");
                    table.ForeignKey(
                        name: "SYS_C008870",
                        column: x => x.GOODS_ID,
                        principalSchema: "DESIGN",
                        principalTable: "GOODS",
                        principalColumn: "GOODS_ID");
                });

            migrationBuilder.CreateTable(
                name: "BOOK",
                schema: "DESIGN",
                columns: table => new
                {
                    TABLE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    BOOK_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false),
                    NUM = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    BOOK_STATUS = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    SPECIAL_DEMAND = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    NOTE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008814", x => new { x.TABLE_ID, x.CUSTOMER_ID, x.BOOK_TIME });
                    table.ForeignKey(
                        name: "SYS_C008815",
                        column: x => x.TABLE_ID,
                        principalSchema: "DESIGN",
                        principalTable: "MYTABLE",
                        principalColumn: "TABLE_ID");
                    table.ForeignKey(
                        name: "SYS_C008816",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CUSTOMER",
                        principalColumn: "CUSTOMER_ID");
                });

            migrationBuilder.CreateTable(
                name: "ROOM",
                schema: "DESIGN",
                columns: table => new
                {
                    ROOM_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TYPE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ROOM_NUM = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ROOM_PHONE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    STATUS = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008821", x => x.ROOM_ID);
                    table.ForeignKey(
                        name: "SYS_C008822",
                        column: x => x.TYPE_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ROOMTYPE",
                        principalColumn: "TYPE_ID");
                });

            migrationBuilder.CreateTable(
                name: "PARK_PLACE",
                schema: "DESIGN",
                columns: table => new
                {
                    PARKING_SPACE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TYPE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    AREA = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    STATUS = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008826", x => x.PARKING_SPACE_ID);
                    table.ForeignKey(
                        name: "SYS_C008827",
                        column: x => x.TYPE,
                        principalSchema: "DESIGN",
                        principalTable: "UNIT_PRICE",
                        principalColumn: "PARKING_PLACE_TYPE");
                });

            migrationBuilder.CreateTable(
                name: "ENTER_EXIT",
                schema: "DESIGN",
                columns: table => new
                {
                    INFO_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CAR_NUMBER = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    ENTER_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    EXIT_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008855", x => x.INFO_ID);
                    table.ForeignKey(
                        name: "SYS_C008856",
                        column: x => x.CAR_NUMBER,
                        principalSchema: "DESIGN",
                        principalTable: "CAR",
                        principalColumn: "CAR_NUMBER");
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                schema: "DESIGN",
                columns: table => new
                {
                    EMPLOYEE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    ACCOUNT = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    SEX = table.Column<string>(type: "VARCHAR2(6)", unicode: false, maxLength: 6, nullable: true),
                    AGE = table.Column<byte>(type: "NUMBER(3)", precision: 3, nullable: true),
                    POST_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ENTRY_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    BASE_PAY = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    PASSWORD = table.Column<string>(type: "VARCHAR2(40)", unicode: false, maxLength: 40, nullable: true),
                    BANK_NAME = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    CREDIT_CARD_NUMBER = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008797", x => x.EMPLOYEE_ID);
                    table.ForeignKey(
                        name: "SYS_C008798",
                        column: x => x.POST_ID,
                        principalSchema: "DESIGN",
                        principalTable: "POST",
                        principalColumn: "POST_ID");
                });

            migrationBuilder.CreateTable(
                name: "CHECKIN",
                schema: "DESIGN",
                columns: table => new
                {
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    ROOM_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    IN_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false),
                    OUT_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008838", x => new { x.CUSTOMER_ID, x.ROOM_ID, x.IN_TIME });
                    table.ForeignKey(
                        name: "SYS_C008839",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CUSTOMER",
                        principalColumn: "CUSTOMER_ID");
                    table.ForeignKey(
                        name: "SYS_C008840",
                        column: x => x.ROOM_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ROOM",
                        principalColumn: "ROOM_ID");
                });

            migrationBuilder.CreateTable(
                name: "LOCATION",
                schema: "DESIGN",
                columns: table => new
                {
                    LOCATION_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FLOOR = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ROOM_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    NOTE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008871", x => x.LOCATION_ID);
                    table.ForeignKey(
                        name: "SYS_C008872",
                        column: x => x.ROOM_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ROOM",
                        principalColumn: "ROOM_ID");
                });

            migrationBuilder.CreateTable(
                name: "PARKING",
                schema: "DESIGN",
                columns: table => new
                {
                    PARKING_SPACE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    CAR_NUMBER = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: false),
                    START_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false),
                    END_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008852", x => new { x.PARKING_SPACE_ID, x.CAR_NUMBER, x.START_TIME });
                    table.ForeignKey(
                        name: "SYS_C008853",
                        column: x => x.PARKING_SPACE_ID,
                        principalSchema: "DESIGN",
                        principalTable: "PARK_PLACE",
                        principalColumn: "PARKING_SPACE_ID");
                    table.ForeignKey(
                        name: "SYS_C008854",
                        column: x => x.CAR_NUMBER,
                        principalSchema: "DESIGN",
                        principalTable: "CAR",
                        principalColumn: "CAR_NUMBER");
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNTING_SUBJECTS",
                schema: "DESIGN",
                columns: table => new
                {
                    ACCOUNT_SUBJECT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    CATEGORY = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    DEBIT = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    CREDIT = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    AMOUNT = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008866", x => x.ACCOUNT_SUBJECT_ID);
                    table.ForeignKey(
                        name: "SYS_C008867",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "APPLICATION",
                schema: "DESIGN",
                columns: table => new
                {
                    APPLICATION_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    APPLICATION_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    APPLICATION_TYPE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    APPLICATION_CONTENT = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    IF_APPROVE = table.Column<string>(type: "VARCHAR2(20)", unicode: false, maxLength: 20, nullable: true),
                    EMPLOYEE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008857", x => x.APPLICATION_ID);
                    table.ForeignKey(
                        name: "SYS_C008858",
                        column: x => x.EMPLOYEE_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "ATTENDANCE_INFORMATION",
                schema: "DESIGN",
                columns: table => new
                {
                    ATTENDANCE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    YEAR = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    MONTH = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    EXPECT_ATTENDANCE_DAYS = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ACTUAL_ATTENDANCE_DAYS = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    PERSONAL_LEAVE_DAYS = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    SICK_LEAVE_DAYS = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    LATE_DAYS = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    EARLY_DEPARTURE_DAYS = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ABSENT_DAYS = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008862", x => x.ATTENDANCE_ID);
                    table.ForeignKey(
                        name: "SYS_C008863",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "CHECK_IN_CHECK_OUT",
                schema: "DESIGN",
                columns: table => new
                {
                    CHECK_IN_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    CHECK_IN_DATE = table.Column<DateTime>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008864", x => x.CHECK_IN_ID);
                    table.ForeignKey(
                        name: "SYS_C008865",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "CLEANING",
                schema: "DESIGN",
                columns: table => new
                {
                    ROOM_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    CLEANING_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008832", x => new { x.ROOM_ID, x.EMP_ID, x.CLEANING_TIME });
                    table.ForeignKey(
                        name: "SYS_C008833",
                        column: x => x.ROOM_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ROOM",
                        principalColumn: "ROOM_ID");
                    table.ForeignKey(
                        name: "SYS_C008834",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "CONSUMPTION_RECORDS",
                schema: "DESIGN",
                columns: table => new
                {
                    CONSUME_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ROOM_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    BILL_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    EMPLOYEE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    CONSUME_TYPE = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    CONSUME_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    CONSUME_AMOUNT = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008848", x => x.CONSUME_ID);
                    table.ForeignKey(
                        name: "SYS_C008849",
                        column: x => x.ROOM_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ROOM",
                        principalColumn: "ROOM_ID");
                    table.ForeignKey(
                        name: "SYS_C008850",
                        column: x => x.BILL_ID,
                        principalSchema: "DESIGN",
                        principalTable: "BILL",
                        principalColumn: "BILL_ID");
                    table.ForeignKey(
                        name: "SYS_C008851",
                        column: x => x.EMPLOYEE_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "MAINTAIN",
                schema: "DESIGN",
                columns: table => new
                {
                    ROOM_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    MAINTAIN_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false),
                    OBJECT = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    RESULT = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008845", x => new { x.ROOM_ID, x.EMP_ID, x.MAINTAIN_TIME });
                    table.ForeignKey(
                        name: "SYS_C008846",
                        column: x => x.ROOM_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ROOM",
                        principalColumn: "ROOM_ID");
                    table.ForeignKey(
                        name: "SYS_C008847",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "PURCHASE",
                schema: "DESIGN",
                columns: table => new
                {
                    GOODS_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    EMPLOYEE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    SUPPLIER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    PURCHASE_DATE = table.Column<DateTime>(type: "DATE", nullable: true),
                    PURCHASE_QUANTITY = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    UNIT_PRICE = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008859", x => new { x.GOODS_ID, x.EMPLOYEE_ID, x.SUPPLIER_ID });
                    table.ForeignKey(
                        name: "SYS_C008860",
                        column: x => x.GOODS_ID,
                        principalSchema: "DESIGN",
                        principalTable: "GOODS",
                        principalColumn: "GOODS_ID");
                    table.ForeignKey(
                        name: "SYS_C008861",
                        column: x => x.EMPLOYEE_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                    table.ForeignKey(
                        name: "SYS_C008900",
                        column: x => x.SUPPLIER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "SUPPLIER",
                        principalColumn: "SUPPLIER_ID");
                });

            migrationBuilder.CreateTable(
                name: "ROOMORDER",
                schema: "DESIGN",
                columns: table => new
                {
                    ORDER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    TYPE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ORDER_STATUS = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    CREATE_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    NUM = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    EXPECT_IN_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    EXPECT_OUT_TIME = table.Column<DateTime>(type: "DATE", nullable: true),
                    PRICE = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    NOTE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008841", x => x.ORDER_ID);
                    table.ForeignKey(
                        name: "SYS_C008842",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CUSTOMER",
                        principalColumn: "CUSTOMER_ID");
                    table.ForeignKey(
                        name: "SYS_C008843",
                        column: x => x.TYPE_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ROOMTYPE",
                        principalColumn: "TYPE_ID");
                    table.ForeignKey(
                        name: "SYS_C008844",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "SALARY",
                schema: "DESIGN",
                columns: table => new
                {
                    ASSETS_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    YEAR = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    MONTH = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    BONUS = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    HOLIDAY_ALLOWANCE = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    OTHER_ALLOWANCE = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    COMMISSION = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    YEAR_END_BONUS = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    OVERTIME_PAY = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    REWARD_TYPE = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true),
                    REWARD_AMOUNT = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    LATE_DEDUCTION = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    EARLY_DEPARTURE_DEDUCTION = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    INCOME_TAX = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    SOCIAL_INSURANCE = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    GROSS_SALARY = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true),
                    NET_SALARY = table.Column<decimal>(type: "NUMBER(12,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008830", x => x.ASSETS_ID);
                    table.ForeignKey(
                        name: "SYS_C008831",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "SERVICE",
                schema: "DESIGN",
                columns: table => new
                {
                    CUSTOMER_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    SERVICE_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false),
                    SERVICE_TYPE = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true),
                    RESULT = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008835", x => new { x.CUSTOMER_ID, x.EMP_ID, x.SERVICE_TIME });
                    table.ForeignKey(
                        name: "SYS_C008836",
                        column: x => x.CUSTOMER_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CUSTOMER",
                        principalColumn: "CUSTOMER_ID");
                    table.ForeignKey(
                        name: "SYS_C008837",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                });

            migrationBuilder.CreateTable(
                name: "ASSETS_INFORMATION",
                schema: "DESIGN",
                columns: table => new
                {
                    ASSET_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ACCOUNT_SUBJECT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ASSETS_NAME = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    ASSETS_CATEGORY = table.Column<string>(type: "VARCHAR2(50)", unicode: false, maxLength: 50, nullable: true),
                    LOCATION_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    REGISTRATION_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    ORIGINAL_ASSETS_AMOUNT = table.Column<long>(type: "NUMBER(12)", precision: 12, nullable: true),
                    VALID = table.Column<string>(type: "VARCHAR2(10)", unicode: false, maxLength: 10, nullable: true, defaultValueSql: "'Y'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008874", x => x.ASSET_ID);
                    table.ForeignKey(
                        name: "SYS_C008875",
                        column: x => x.ACCOUNT_SUBJECT_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ACCOUNTING_SUBJECTS",
                        principalColumn: "ACCOUNT_SUBJECT_ID");
                    table.ForeignKey(
                        name: "SYS_C008876",
                        column: x => x.LOCATION_ID,
                        principalSchema: "DESIGN",
                        principalTable: "LOCATION",
                        principalColumn: "LOCATION_ID");
                });

            migrationBuilder.CreateTable(
                name: "ASSETS_SUMMARIZE",
                schema: "DESIGN",
                columns: table => new
                {
                    ASSET_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ACCOUNT_SUBJECT_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ASSETS_AMOUNT = table.Column<long>(type: "NUMBER(12)", precision: 12, nullable: true),
                    ASSETS_DEPRECIATION = table.Column<long>(type: "NUMBER(12)", precision: 12, nullable: true),
                    ASSET_PRESENT_VALUE = table.Column<long>(type: "NUMBER(12)", precision: 12, nullable: true),
                    RESPONS_PERSON_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008877", x => x.ASSET_ID);
                    table.ForeignKey(
                        name: "SYS_C008878",
                        column: x => x.ACCOUNT_SUBJECT_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ACCOUNTING_SUBJECTS",
                        principalColumn: "ACCOUNT_SUBJECT_ID");
                });

            migrationBuilder.CreateTable(
                name: "MYORDER",
                schema: "DESIGN",
                columns: table => new
                {
                    TABLE_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    DISH_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    ORDER_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: false),
                    CONSUMPTION_RECORD_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: true),
                    ORDER_STATUS = table.Column<string>(type: "VARCHAR2(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008810", x => new { x.TABLE_ID, x.DISH_ID, x.ORDER_TIME });
                    table.ForeignKey(
                        name: "SYS_C008811",
                        column: x => x.TABLE_ID,
                        principalSchema: "DESIGN",
                        principalTable: "MYTABLE",
                        principalColumn: "TABLE_ID");
                    table.ForeignKey(
                        name: "SYS_C008812",
                        column: x => x.DISH_ID,
                        principalSchema: "DESIGN",
                        principalTable: "DISH",
                        principalColumn: "DISH_ID");
                    table.ForeignKey(
                        name: "SYS_C008813",
                        column: x => x.CONSUMPTION_RECORD_ID,
                        principalSchema: "DESIGN",
                        principalTable: "CONSUMPTION_RECORDS",
                        principalColumn: "CONSUME_ID");
                });

            migrationBuilder.CreateTable(
                name: "CHECKREPAIR",
                schema: "DESIGN",
                columns: table => new
                {
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    ASSET_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    CHECK_REPAIR_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    CHECK_REPAIR_CONTENT = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008882", x => new { x.EMP_ID, x.ASSET_ID });
                    table.ForeignKey(
                        name: "SYS_C008883",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                    table.ForeignKey(
                        name: "SYS_C008884",
                        column: x => x.ASSET_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ASSETS_INFORMATION",
                        principalColumn: "ASSET_ID");
                });

            migrationBuilder.CreateTable(
                name: "SCRAP",
                schema: "DESIGN",
                columns: table => new
                {
                    EMP_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    ASSET_ID = table.Column<decimal>(type: "NUMBER(38)", nullable: false),
                    SCRAP_TIME = table.Column<DateTime>(type: "TIMESTAMP(6)", precision: 6, nullable: true),
                    SCRAP_REASON = table.Column<string>(type: "VARCHAR2(128)", unicode: false, maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C008879", x => new { x.EMP_ID, x.ASSET_ID });
                    table.ForeignKey(
                        name: "SYS_C008880",
                        column: x => x.EMP_ID,
                        principalSchema: "DESIGN",
                        principalTable: "EMPLOYEE",
                        principalColumn: "EMPLOYEE_ID");
                    table.ForeignKey(
                        name: "SYS_C008881",
                        column: x => x.ASSET_ID,
                        principalSchema: "DESIGN",
                        principalTable: "ASSETS_INFORMATION",
                        principalColumn: "ASSET_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNTING_SUBJECTS_EMP_ID",
                schema: "DESIGN",
                table: "ACCOUNTING_SUBJECTS",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATION_EMPLOYEE_ID",
                schema: "DESIGN",
                table: "APPLICATION",
                column: "EMPLOYEE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ASSETS_INFORMATION_ACCOUNT_SUBJECT_ID",
                schema: "DESIGN",
                table: "ASSETS_INFORMATION",
                column: "ACCOUNT_SUBJECT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ASSETS_INFORMATION_LOCATION_ID",
                schema: "DESIGN",
                table: "ASSETS_INFORMATION",
                column: "LOCATION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ASSETS_SUMMARIZE_ACCOUNT_SUBJECT_ID",
                schema: "DESIGN",
                table: "ASSETS_SUMMARIZE",
                column: "ACCOUNT_SUBJECT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ASSIGNMENT_DEPARTMENT_ID",
                schema: "DESIGN",
                table: "ASSIGNMENT",
                column: "DEPARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ATTENDANCE_INFORMATION_EMP_ID",
                schema: "DESIGN",
                table: "ATTENDANCE_INFORMATION",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BOOK_CUSTOMER_ID",
                schema: "DESIGN",
                table: "BOOK",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CAR_CUSTOMER_ID",
                schema: "DESIGN",
                table: "CAR",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CHECK_IN_CHECK_OUT_EMP_ID",
                schema: "DESIGN",
                table: "CHECK_IN_CHECK_OUT",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CHECKIN_ROOM_ID",
                schema: "DESIGN",
                table: "CHECKIN",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CHECKREPAIR_ASSET_ID",
                schema: "DESIGN",
                table: "CHECKREPAIR",
                column: "ASSET_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CLEANING_EMP_ID",
                schema: "DESIGN",
                table: "CLEANING",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMPLAINT_CUSTOMER_ID",
                schema: "DESIGN",
                table: "COMPLAINT",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONSUME_GOODS_ID",
                schema: "DESIGN",
                table: "CONSUME",
                column: "GOODS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONSUMPTION_RECORDS_BILL_ID",
                schema: "DESIGN",
                table: "CONSUMPTION_RECORDS",
                column: "BILL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONSUMPTION_RECORDS_EMPLOYEE_ID",
                schema: "DESIGN",
                table: "CONSUMPTION_RECORDS",
                column: "EMPLOYEE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONSUMPTION_RECORDS_ROOM_ID",
                schema: "DESIGN",
                table: "CONSUMPTION_RECORDS",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_POST_ID",
                schema: "DESIGN",
                table: "EMPLOYEE",
                column: "POST_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ENTER_EXIT_CAR_NUMBER",
                schema: "DESIGN",
                table: "ENTER_EXIT",
                column: "CAR_NUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_LOCATION_ROOM_ID",
                schema: "DESIGN",
                table: "LOCATION",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MAINTAIN_EMP_ID",
                schema: "DESIGN",
                table: "MAINTAIN",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MYORDER_CONSUMPTION_RECORD_ID",
                schema: "DESIGN",
                table: "MYORDER",
                column: "CONSUMPTION_RECORD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MYORDER_DISH_ID",
                schema: "DESIGN",
                table: "MYORDER",
                column: "DISH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PARK_PLACE_TYPE",
                schema: "DESIGN",
                table: "PARK_PLACE",
                column: "TYPE");

            migrationBuilder.CreateIndex(
                name: "IX_PARKING_CAR_NUMBER",
                schema: "DESIGN",
                table: "PARKING",
                column: "CAR_NUMBER");

            migrationBuilder.CreateIndex(
                name: "IX_PAY_BILL_ID",
                schema: "DESIGN",
                table: "PAY",
                column: "BILL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_POST_DEPARTMENT_ID",
                schema: "DESIGN",
                table: "POST",
                column: "DEPARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_EMPLOYEE_ID",
                schema: "DESIGN",
                table: "PURCHASE",
                column: "EMPLOYEE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_SUPPLIER_ID",
                schema: "DESIGN",
                table: "PURCHASE",
                column: "SUPPLIER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ROOM_TYPE_ID",
                schema: "DESIGN",
                table: "ROOM",
                column: "TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ROOMORDER_CUSTOMER_ID",
                schema: "DESIGN",
                table: "ROOMORDER",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ROOMORDER_EMP_ID",
                schema: "DESIGN",
                table: "ROOMORDER",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ROOMORDER_TYPE_ID",
                schema: "DESIGN",
                table: "ROOMORDER",
                column: "TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALARY_EMP_ID",
                schema: "DESIGN",
                table: "SALARY",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SCRAP_ASSET_ID",
                schema: "DESIGN",
                table: "SCRAP",
                column: "ASSET_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICE_EMP_ID",
                schema: "DESIGN",
                table: "SERVICE",
                column: "EMP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STAGING_CUSTOMER_ID",
                schema: "DESIGN",
                table: "STAGING",
                column: "CUSTOMER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPLICATION",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ASSETS_SUMMARIZE",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ASSIGNMENT",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ATTENDANCE_INFORMATION",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "BOOK",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "CHECK_IN_CHECK_OUT",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "CHECKIN",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "CHECKREPAIR",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "CLEANING",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "COMPLAINT",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "CONSUME",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ENTER_EXIT",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "MAINTAIN",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "MYORDER",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "PARKING",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "PAY",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "PURCHASE",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ROOMORDER",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "SALARY",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "SCRAP",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "SERVICE",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "STAGING",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "MYTABLE",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "DISH",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "CONSUMPTION_RECORDS",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "PARK_PLACE",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "CAR",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "GOODS",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "SUPPLIER",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ASSETS_INFORMATION",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "BILL",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "UNIT_PRICE",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "CUSTOMER",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ACCOUNTING_SUBJECTS",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "LOCATION",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "EMPLOYEE",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ROOM",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "POST",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "ROOMTYPE",
                schema: "DESIGN");

            migrationBuilder.DropTable(
                name: "DEPARTMENT",
                schema: "DESIGN");
        }
    }
}
