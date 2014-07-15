Update T_PM_ProductionSchedule SET Process='圆片' WHere Process='冲版';
Update T_PM_ProductionSchedule SET Process='液压' WHere Process='拉伸';
Update T_ProductInfo_Product SET P1='圆片' WHere P1='冲版';
Update T_ProductInfo_Product SET P2='液压' WHere P2='拉伸';
Insert Into T_System_Settings(ID,"Key",Value,Remark) Values(2,'DBVersion','1.4.5','Insert');
alter TABLE T_PM_ProcessBatchInput ADD "ProcessorsID" Guid;