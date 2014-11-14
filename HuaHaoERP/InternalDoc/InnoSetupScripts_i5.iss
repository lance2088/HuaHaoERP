; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{F60249E1-D51A-43A5-B469-BF891D8D1300}
AppName=HuaHaoERP
AppVersion=1.6.5
;AppVerName=PyramidERP 1.6.5
AppPublisher=StoneAnt, Inc.
AppPublisherURL=http://www.stoneant.net/
AppSupportURL=http://www.stoneant.net/
AppUpdatesURL=http://www.stoneant.net/
DefaultDirName={pf}\HuaHaoERP
DefaultGroupName=石蚁金字塔ERP
AllowNoIcons=yes
InfoBeforeFile=D:\WorkSpaces\Visual Studio\HuaHaoERP\HuaHaoERP\bin\Release\安装说明.txt
OutputDir=D:\WorkSpaces\Visual Studio\HuaHaoERP\HuaHaoERP\bin\Release\HuaHaoERP_Secure
OutputBaseFilename=HuaHaoERP_Setup
SetupIconFile=D:\WorkSpaces\Visual Studio\HuaHaoERP\HuaHaoERP\View\Resources\Ico\Pyramid_Logo_white_128x128.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "D:\WorkSpaces\Visual Studio\HuaHaoERP\HuaHaoERP\bin\Release\HuaHaoERP_Secure\HuaHaoERP.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\WorkSpaces\Visual Studio\HuaHaoERP\HuaHaoERP\安装说明.txt"; DestDir: "{app}\\Doc"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\PyramidERP"; Filename: "{app}\HuaHaoERP.exe"
Name: "{group}\{cm:ProgramOnTheWeb,HuaHaoERP}"; Filename: "http://www.stoneant.net/"
Name: "{group}\{cm:UninstallProgram,HuaHaoERP}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\PyramidERP"; Filename: "{app}\HuaHaoERP.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\PyramidERP"; Filename: "{app}\HuaHaoERP.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\HuaHaoERP.exe"; Description: "{cm:LaunchProgram,PyramidERP}"; Flags: nowait postinstall skipifsilent

