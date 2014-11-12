CREATE TABLE T_ProductInfo_Wafer (
"Guid"  Guid NOT NULL,
PRIMARY KEY ("Guid" ASC)
)
;
CREATE TABLE T_Order_Wafer (
"Guid"  Guid NOT NULL,
PRIMARY KEY ("Guid" ASC)
)
;
CREATE TABLE T_Warehouse_Wafer (
"Guid"  Guid NOT NULL,
PRIMARY KEY ("Guid" ASC)
)
;
Update T_System_Settings SET Value='1.6.3' WHERE ID=2;