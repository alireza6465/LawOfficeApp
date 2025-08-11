; Inno Setup script for LawOfficeApp (auto-generated)
[Setup]
AppName=Law Office Manager
AppVersion=1.0
DefaultDirName={pf}\LawOfficeManager
DefaultGroupName=Law Office Manager
OutputBaseFilename=LawOffice_Setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "persian"; MessagesFile: "compiler:Languages\Persian.isl"

[Files]
; All files from publish folder will be installed to {app}
Source: "..\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

; Database: if not exists, copy default DB to user documents
Source: "..\publish\LawOfficeData.db"; DestDir: "{userdocs}\LawOfficeData"; Flags: onlyifdoesntexist createallsubdirs

[Icons]
Name: "{group}\Law Office Manager"; Filename: "{app}\LawOfficeApp.exe"
Name: "{commondesktop}\Law Office Manager"; Filename: "{app}\LawOfficeApp.exe"; Tasks: desktopicon

[Tasks]
Name: desktopicon; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"; Flags: unchecked

[Run]
Filename: "{app}\LawOfficeApp.exe"; Description: "Start Law Office Manager"; Flags: nowait postinstall skipifsilent
