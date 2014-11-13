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
Update T_System_Settings SET Value='1.6.3' WHERE ID=2;