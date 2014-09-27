DROP TABLE IF EXISTS "main"."T_Warehouse_HalfProduct";
CREATE TABLE "T_Warehouse_HalfProduct" (
"Guid"  Guid NOT NULL,
"ProductID"  Guid NOT NULL,
"Date"  DateTime,
"Operator"  TEXT,
"Quantity"  INTEGER,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
DROP TABLE IF EXISTS "main"."T_Warehouse_SparePartsInventory";
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
DROP TABLE IF EXISTS "main"."T_PM_ProductOutProcess";
CREATE TABLE "T_PM_ProductOutProcess" (
"Guid"  Guid NOT NULL,
"Number" TEXT,
"ProcessorID" Guid not null,
"Date"  DateTime,
"Operator"  TEXT,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
DROP TABLE IF EXISTS "main"."T_PM_ProductOutProcessDetail";
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
DROP TABLE IF EXISTS "main"."T_PM_ProductInProcess";
CREATE TABLE "T_PM_ProductInProcess" (
"Guid"  Guid NOT NULL,
"Number" TEXT,
"ProcessorID" Guid not null,
"Date"  DateTime,
"Operator"  TEXT,
"Remark"  TEXT, "DeleteMark" DateTime,
PRIMARY KEY ("Guid" ASC)
);
DROP TABLE IF EXISTS "main"."T_PM_ProductInProcessDetail";
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