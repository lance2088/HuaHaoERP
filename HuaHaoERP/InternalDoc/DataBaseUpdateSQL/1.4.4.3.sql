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