CREATE TABLE "T_Warehouse_ProductBatchInput" (
"Guid"  Guid NOT NULL,
"Number"  TEXT NOT NULL,
"Name"  TEXT,
"Date"  DateTime,
"DeleteMark"  DateTime,
"Remark"  TEXT,
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
CREATE TABLE "T_PM_ProcessBatchInput" (
"Guid"  Guid NOT NULL,
"Number"  TEXT NOT NULL,
"Name"  TEXT,
"Date"  DateTime,
"DeleteMark"  DateTime,
"Remark"  TEXT,
PRIMARY KEY ("Guid" ASC)
);
alter TABLE T_Warehouse_Product ADD "DeleteMark" DateTime;
alter TABLE T_Warehouse_ProductPacking ADD "DeleteMark" DateTime;
alter TABLE T_Warehouse_ProductBatchInput ADD "OrderType" Text;
alter TABLE T_PM_ProcessBatchInput ADD "OrderType" Text;