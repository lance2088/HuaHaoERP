/*
Navicat SQLite Data Transfer

Source Server         : HuaHao
Source Server Version : 30714
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30714
File Encoding         : 65001

Date: 2014-06-04 20:25:21
*/


-- ----------------------------
-- Table structure for T_Orders_Processors
-- ----------------------------
CREATE TABLE "T_Orders_Processors" (
"Guid"  Guid NOT NULL,
"OrderNumber"  TEXT,
"ProcessorsID"  Guid,
"DeliveryDate"  DateTime,
"OrderDate"  DateTime,
"DeleteMark"  DateTime,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Orders_Processors
-- ----------------------------

-- ----------------------------
-- Table structure for T_Orders_ProcessorsDetails
-- ----------------------------
CREATE TABLE "T_Orders_ProcessorsDetails" (
"Guid"  Guid NOT NULL,
"Order"  Guid,
"Product"  Guid,
"NumberOfItems"  INTEGER,
"Quantity"  INTEGER,
"Unit"  TEXT,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Orders_ProcessorsDetails
-- ----------------------------

-- ----------------------------
-- Table structure for T_Orders_Product
-- ----------------------------
CREATE TABLE "T_Orders_Product" (
"Guid"  Guid NOT NULL,
"OrderNumber"  TEXT,
"CustomerID"  Guid,
"DeliveryDate"  DateTime,
"OrderDate"  DateTime,
"DeleteMark"  DateTime,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Orders_Product
-- ----------------------------

-- ----------------------------
-- Table structure for T_Orders_ProductDetails
-- ----------------------------
CREATE TABLE "T_Orders_ProductDetails" (
"Guid"  Guid NOT NULL,
"OrderID"  Guid,
"ProductID"  Guid,
"NumberOfItems"  INTEGER,
"Quantity"  INTEGER,
"Unit"  TEXT,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Orders_ProductDetails
-- ----------------------------

-- ----------------------------
-- Table structure for T_Orders_Supplier
-- ----------------------------
CREATE TABLE "T_Orders_Supplier" (
"Guid"  Guid NOT NULL,
"OrderNumber"  TEXT,
"SupplierID"  Guid,
"DeliveryDate"  DateTime,
"OrderDate"  DateTime,
"DeleteMark"  DateTime,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Orders_Supplier
-- ----------------------------

-- ----------------------------
-- Table structure for T_Orders_SupplierDetails
-- ----------------------------
CREATE TABLE "T_Orders_SupplierDetails" (
"Guid"  Guid NOT NULL,
"RawMaterialsID"  Guid,
"Weight"  TEXT,
"Material"  TEXT,
"Sp1"  TEXT,
"Sp2"  TEXT,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Orders_SupplierDetails
-- ----------------------------

-- ----------------------------
-- Table structure for T_PM_ProcessSchedule
-- ----------------------------
CREATE TABLE "T_PM_ProcessSchedule" (
"Guid"  Guid NOT NULL,
"Date"  DateTime,
"ProductID"  Guid,
"ProcessorsID"  Guid,
"Quantity"  INTEGER,
"MinorInjuries"  INTEGER,
"Injuries"  INTEGER,
"Lose"  INTEGER,
"OrderType"  TEXT,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_PM_ProcessSchedule
-- ----------------------------

-- ----------------------------
-- Table structure for T_PM_ProductionSchedule
-- ----------------------------
CREATE TABLE "T_PM_ProductionSchedule" (
"Guid"  Guid NOT NULL,
"Date"  DateTime,
"StaffID"  Guid,
"ProductID"  Guid,
"Process"  TEXT,
"Number"  INTEGER,
"Break"  INTEGER DEFAULT 0,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_PM_ProductionSchedule
-- ----------------------------

-- ----------------------------
-- Table structure for T_ProductInfo_Product
-- ----------------------------
CREATE TABLE "T_ProductInfo_Product" (
"GUID"  GUID NOT NULL,
"Number"  TEXT,
"Name"  TEXT,
"Material"  TEXT,
"Type"  TEXT,
"Specification"  TEXT,
"P1"  TEXT,
"P2"  TEXT,
"P3"  TEXT,
"P4"  TEXT,
"P5"  TEXT,
"P6"  TEXT,
"P7"  TEXT,
"P8"  TEXT,
"P9"  TEXT,
"P10"  TEXT,
"PackageNumber"  INTEGER,
"Remark"  TEXT,
"DeleteMark"  DateTime,
"AddTime"  DateTime,
"RawMaterialsID"  Guid,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("GUID" ASC)
);

-- ----------------------------
-- Records of T_ProductInfo_Product
-- ----------------------------

-- ----------------------------
-- Table structure for T_ProductInfo_RawMaterials
-- ----------------------------
CREATE TABLE "T_ProductInfo_RawMaterials" (
"GUID"  GUID NOT NULL,
"Number"  TEXT,
"Name"  TEXT,
"Weight"  TEXT,
"Material"  TEXT,
"Supplier"  Guid,
"Sp1"  TEXT,
"Sp2"  TEXT,
"Remark"  TEXT,
"DeleteMark"  DateTime,
"AddTime"  DateTime,
"ProductID"  Guid,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("GUID" ASC)
);

-- ----------------------------
-- Records of T_ProductInfo_RawMaterials
-- ----------------------------

-- ----------------------------
-- Table structure for T_System_Logs
-- ----------------------------
CREATE TABLE "T_System_Logs" (
"Guid"  Guid NOT NULL,
"Name"  TEXT,
"Value"  TEXT,
"Type"  TEXT,
"DateTime"  DateTime,
"Remark"  TEXT,
PRIMARY KEY ("Guid")
);

-- ----------------------------
-- Records of T_System_Logs
-- ----------------------------

-- ----------------------------
-- Table structure for T_System_Settings
-- ----------------------------
CREATE TABLE "T_System_Settings" (
"ID"  INTEGER NOT NULL,
"Key"  TEXT,
"Value"  TEXT,
"Remark"  TEXT,
PRIMARY KEY ("ID")
);

-- ----------------------------
-- Records of T_System_Settings
-- ----------------------------

-- ----------------------------
-- Table structure for T_System_User
-- ----------------------------
CREATE TABLE "T_System_User" (
"ID"  INTEGER NOT NULL,
"Name"  TEXT,
"Password"  TEXT,
"UserGroup"  INTEGER,
"RealName"  TEXT,
"Permissions"  INTEGER,
"Remark"  TEXT,
"DeleteMark"  DateTime,
PRIMARY KEY ("ID" ASC)
);

-- ----------------------------
-- Records of T_System_User
-- ----------------------------
INSERT INTO "main"."T_System_User" VALUES (1, 'root', 'Hh123123', 1, 'StoneAnt', 9, '超级管理员', null);
INSERT INTO "main"."T_System_User" VALUES (2, 'admin', 123, 3, 'StoneAnt', 8, '管理员', null);

-- ----------------------------
-- Table structure for T_System_UserGroup
-- ----------------------------
CREATE TABLE "T_System_UserGroup" (
"ID"  INTEGER NOT NULL,
"Name"  TEXT,
"Permissions"  TEXT,
"DeleteMark"  DateTime,
"Remark"  TEXT,
PRIMARY KEY ("ID" ASC)
);

-- ----------------------------
-- Records of T_System_UserGroup
-- ----------------------------
INSERT INTO "main"."T_System_UserGroup" VALUES (1, 'root', 777, null, null);
INSERT INTO "main"."T_System_UserGroup" VALUES (3, 'admin', '077', null, null);
INSERT INTO "main"."T_System_UserGroup" VALUES (4, '仓管', 1, null, null);
INSERT INTO "main"."T_System_UserGroup" VALUES (5, '生产', 2, null, null);
INSERT INTO "main"."T_System_UserGroup" VALUES (6, '管理组', 3, null, null);

-- ----------------------------
-- Table structure for T_UserInfo_Customer
-- ----------------------------
CREATE TABLE "T_UserInfo_Customer" (
"GUID"  GUID NOT NULL,
"Number"  TEXT NOT NULL,
"Name"  TEXT NOT NULL,
"Address"  TEXT,
"Area"  TEXT,
"Phone"  TEXT,
"MobilePhone"  TEXT,
"Fax"  TEXT,
"Business"  TEXT,
"Clerk"  TEXT,
"DebtCeiling"  decimal,
"Remark"  TEXT,
"DeleteMark"  DateTime,
"AddTime"  DateTime,
PRIMARY KEY ("GUID" ASC)
);

-- ----------------------------
-- Records of T_UserInfo_Customer
-- ----------------------------

-- ----------------------------
-- Table structure for T_UserInfo_Processors
-- ----------------------------
CREATE TABLE "T_UserInfo_Processors" (
"GUID"  Guid NOT NULL,
"Number"  TEXT,
"Name"  TEXT,
"Address"  TEXT,
"Area"  TEXT,
"Phone"  TEXT,
"MobilePhone"  TEXT,
"Fax"  TEXT,
"Business"  TEXT,
"Clerk"  TEXT,
"OpeningBank"  TEXT,
"BankCardNo"  TEXT,
"BankCardName"  TEXT,
"Remark"  TEXT,
"DeleteMark"  DateTime,
"AddTime"  DateTime,
PRIMARY KEY ("GUID" ASC)
);

-- ----------------------------
-- Records of T_UserInfo_Processors
-- ----------------------------

-- ----------------------------
-- Table structure for T_UserInfo_Staff
-- ----------------------------
CREATE TABLE "T_UserInfo_Staff" (
"GUID"  Guid NOT NULL,
"Number"  TEXT,
"Name"  TEXT,
"Jobs"  TEXT,
"EntryTime"  DateTime,
"Contact"  TEXT,
"IDNumber"  TEXT,
"Remark"  TEXT,
"DepartureTime"  DateTime,
"DeleteMark"  DateTime,
"AddTime"  DateTime,
PRIMARY KEY ("GUID" ASC)
);

-- ----------------------------
-- Records of T_UserInfo_Staff
-- ----------------------------

-- ----------------------------
-- Table structure for T_UserInfo_Supplier
-- ----------------------------
CREATE TABLE "T_UserInfo_Supplier" (
"GUID"  GUID NOT NULL,
"Number"  TEXT,
"Name"  TEXT,
"Address"  TEXT,
"Area"  TEXT,
"Phone"  TEXT,
"MobilePhone"  TEXT,
"Fax"  TEXT,
"Business"  TEXT,
"Clerk"  TEXT,
"OpeningBank"  TEXT,
"BankCardNo"  TEXT,
"BankCardName"  TEXT,
"Remark"  TEXT,
"DeleteMark"  DateTime,
"AddTime"  DateTime,
PRIMARY KEY ("GUID" ASC)
);

-- ----------------------------
-- Records of T_UserInfo_Supplier
-- ----------------------------

-- ----------------------------
-- Table structure for T_Warehouse_Product
-- ----------------------------
CREATE TABLE "T_Warehouse_Product" (
"Guid"  Guid NOT NULL,
"ProductID"  Guid,
"Date"  DateTime,
"Operator"  TEXT,
"Quantity"  INTEGER,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Warehouse_Product
-- ----------------------------

-- ----------------------------
-- Table structure for T_Warehouse_ProductPacking
-- ----------------------------
CREATE TABLE "T_Warehouse_ProductPacking" (
"Guid"  Guid NOT NULL,
"ProductID"  Guid,
"Date"  DateTime,
"Operator"  TEXT,
"Quantity"  INTEGER,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Warehouse_ProductPacking
-- ----------------------------

-- ----------------------------
-- Table structure for T_Warehouse_RawMaterials
-- ----------------------------
CREATE TABLE "T_Warehouse_RawMaterials" (
"Guid"  Guid NOT NULL,
"RawMaterialsID"  Guid,
"Date"  DateTime,
"Operator"  TEXT,
"Number"  INTEGER,
"Remark"  TEXT,
"Type"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Warehouse_RawMaterials
-- ----------------------------

-- ----------------------------
-- Table structure for T_Warehouse_Scrap
-- ----------------------------
CREATE TABLE "T_Warehouse_Scrap" (
"Guid"  Guid NOT NULL,
"Name"  TEXT,
"Date"  DateTime,
"Operator"  TEXT,
"Number"  REAL,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);

-- ----------------------------
-- Records of T_Warehouse_Scrap
-- ----------------------------
