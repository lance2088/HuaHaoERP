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
)
;
Update T_System_Settings SET Value='1.6.0' WHERE ID=2;