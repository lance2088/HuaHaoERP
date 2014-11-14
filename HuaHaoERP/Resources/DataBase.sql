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
CREATE TABLE "T_PM_ProcessBatchInput" (
"Guid"  Guid NOT NULL,
"Number"  TEXT NOT NULL,
"Name"  TEXT,
"Date"  DateTime,
"DeleteMark"  DateTime,
"Remark"  TEXT, "OrderType" Text, "ProcessorsID" Guid,
PRIMARY KEY ("Guid" ASC)
);
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
"DeleteMark"  DateTime,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);
CREATE TABLE "T_PM_ProductionBatchInput" (
"Guid"  Guid NOT NULL,
"Number"  TEXT NOT NULL,
"Name"  TEXT,
"Date"  DateTime,
"DeleteMark"  DateTime,
"Remark"  TEXT,
PRIMARY KEY ("Guid" ASC)
);
CREATE TABLE T_PM_ProductionBookkeeping (
"Guid"  Guid NOT NULL,
"OrderNum"  TEXT,
"ProductID"  Guid,
"P1Num"  INTEGER DEFAULT 0,
"P2Num"  INTEGER DEFAULT 0,
"P3Num"  INTEGER DEFAULT 0,
"P4Num"  INTEGER DEFAULT 0,
"P5Num"  INTEGER DEFAULT 0,
"P6Num"  INTEGER DEFAULT 0,
"Remark"  TEXT,
"AddDate"  DateTime,
"DeleteMark"  DateTime,
PRIMARY KEY ("Guid" ASC)
);
CREATE TABLE "T_PM_ProductionSchedule" (
"Guid"  Guid NOT NULL,
"Date"  DateTime,
"StaffID"  Guid,
"ProductID"  Guid,
"Process"  TEXT,
"Number"  INTEGER,
"Break"  INTEGER DEFAULT 0,
"Remark"  TEXT,
"DeleteMark"  DateTime,
"ParentGuid"  Guid,
"Obligate1"  TEXT,
"Obligate2"  TEXT,
PRIMARY KEY ("Guid" ASC)
);
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
CREATE TABLE "T_System_Logs" (
"Guid"  Guid NOT NULL,
"Name"  TEXT,
"Value"  TEXT,
"Type"  TEXT,
"DateTime"  DateTime,
"Remark"  TEXT,
PRIMARY KEY ("Guid")
);
CREATE TABLE "T_System_Settings" (
"ID"  INTEGER NOT NULL,
"Key"  TEXT,
"Value"  TEXT,
"Remark"  TEXT,
PRIMARY KEY ("ID")
);
INSERT INTO "main"."T_System_Settings" VALUES (1, 'License', null, null);
INSERT INTO "main"."T_System_Settings" VALUES (2, 'DBVersion', '1.6.0', 'Insert');

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
INSERT INTO "main"."T_System_User" VALUES (1, 'root', 'Hh123123', 1, 'StoneAnt', 9, '超级管理员', null);
INSERT INTO "main"."T_System_User" VALUES (2, 'admin', 123, 3, 'StoneAnt', 8, '管理员', null);

CREATE TABLE "T_System_UserGroup" (
"ID"  INTEGER NOT NULL,
"Name"  TEXT,
"Permissions"  TEXT,
"DeleteMark"  DateTime,
"Remark"  TEXT,
PRIMARY KEY ("ID" ASC)
);

INSERT INTO "main"."T_System_UserGroup" VALUES (1, 'root', 777, null, null);
INSERT INTO "main"."T_System_UserGroup" VALUES (3, 'admin', '077', null, null);
INSERT INTO "main"."T_System_UserGroup" VALUES (4, '仓管', 1, null, null);
INSERT INTO "main"."T_System_UserGroup" VALUES (5, '生产', 2, null, null);
INSERT INTO "main"."T_System_UserGroup" VALUES (6, '管理组', 3, null, null);

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

CREATE TABLE "T_Warehouse_Product" (
"Guid"  Guid NOT NULL,
"ProductID"  Guid,
"Date"  DateTime,
"Operator"  TEXT,
"Quantity"  INTEGER,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);

CREATE TABLE "T_Warehouse_ProductBatchInput" (
"Guid"  Guid NOT NULL,
"Number"  TEXT NOT NULL,
"Name"  TEXT,
"Date"  DateTime,
"DeleteMark"  DateTime,
"Remark"  TEXT, "OrderType" Text,
PRIMARY KEY ("Guid" ASC)
);

CREATE TABLE "T_Warehouse_ProductPacking" (
"Guid"  Guid NOT NULL,
"ProductID"  Guid,
"Date"  DateTime,
"Operator"  TEXT,
"Quantity"  INTEGER,
"Remark"  TEXT,
"Obligate1"  TEXT,
"Obligate2"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);

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

CREATE TABLE "T_Warehouse_HalfProduct" (
"Guid"  Guid NOT NULL,
"ProductID"  Guid NOT NULL,
"Date"  DateTime,
"Operator"  TEXT,
"Quantity"  INTEGER,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
CREATE TABLE "T_Warehouse_SparePartsInventory" (
"Guid"  Guid NOT NULL,
"ProcessorID" Guid not null,
"ProductID"  Guid NOT NULL,
"Date"  DateTime,
"Operator"  TEXT,
"Quantity"  INTEGER,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
CREATE TABLE "T_PM_ProductOutProcess" (
"Guid"  Guid NOT NULL,
"Number" TEXT,
"ProcessorID" Guid not null,
"Date"  DateTime,
"Operator"  TEXT,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
CREATE TABLE "T_PM_ProductOutProcessDetail" (
"Guid"  Guid NOT NULL,
"ParentId" Guid not null,
"ProductID"  Guid NOT NULL,
"Date"  DateTime,
"Operator"  TEXT,
"QuantityA"  INTEGER,
"QuantityB"  INTEGER,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
CREATE TABLE "T_PM_ProductInProcess" (
"Guid"  Guid NOT NULL,
"Number" TEXT,
"ProcessorID" Guid not null,
"Date"  DateTime,
"Operator"  TEXT,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
CREATE TABLE "T_PM_ProductInProcessDetail" (
"Guid"  Guid NOT NULL,
"ParentId" Guid not null,
"ProductID"  Guid NOT NULL,
"Date"  DateTime,
"Operator"  TEXT,
"QuantityA"  INTEGER,
"QuantityB"  INTEGER,
"QuantityC"  INTEGER,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
Update T_System_Settings SET Value='1.6.2' WHERE ID=2;
CREATE TABLE T_ProductInfo_Wafer (
"Guid"  Guid NOT NULL,
"Number"  TEXT,
"Diameter"  TEXT,
"Thickness"  TEXT,
"Remark"  TEXT,
PRIMARY KEY ("Guid" ASC)
)
;
CREATE TABLE T_Order_Wafer (
"Guid"  Guid NOT NULL,
"OrderType"  INTEGER,
"OrderNo"  TEXT,
"Date"  DateTime,
"Remark"  TEXT,
PRIMARY KEY ("Guid" ASC)
)
;
CREATE TABLE T_Warehouse_Wafer (
"Guid"  Guid NOT NULL,
"OrderGuid"  Guid,
"WaferGuid"  Guid,
"Quantity"  INTEGER,
PRIMARY KEY ("Guid" ASC)
)
;
Update T_System_Settings SET Value='1.6.4' WHERE ID=2;
alter TABLE T_Warehouse_Wafer ADD "LossQuantity" INTEGER;
alter TABLE T_Warehouse_Wafer ADD "HalfProductGuid" Guid;
alter TABLE T_Warehouse_Wafer ADD "HalfProductQuantity" INTEGER;
Update T_System_Settings SET Value='1.6.5' WHERE ID=2;