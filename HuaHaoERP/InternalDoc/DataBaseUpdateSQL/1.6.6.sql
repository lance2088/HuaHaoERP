alter TABLE T_Warehouse_HalfProduct ADD "OrderGuid" Guid;
alter TABLE T_PM_ProductInProcessDetail ADD "OrderGuid" Guid;
alter TABLE T_Warehouse_SparePartsInventory ADD "OrderGuid" Guid;
alter TABLE T_Warehouse_Product ADD "OrderGuid" Guid;
alter TABLE T_PM_ProductOutProcessDetail ADD "OrderGuid" Guid;
Update T_System_Settings SET Value='1.6.6' WHERE ID=2;