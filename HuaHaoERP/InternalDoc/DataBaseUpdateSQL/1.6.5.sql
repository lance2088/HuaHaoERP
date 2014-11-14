alter TABLE T_Warehouse_Wafer ADD "LossQuantity" INTEGER;
alter TABLE T_Warehouse_Wafer ADD "HalfProductGuid" Guid;
alter TABLE T_Warehouse_Wafer ADD "HalfProductQuantity" INTEGER;
Update T_System_Settings SET Value='1.6.5' WHERE ID=2;