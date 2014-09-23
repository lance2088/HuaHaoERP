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
Update T_System_Settings SET Value='1.6.1' WHERE ID=2;