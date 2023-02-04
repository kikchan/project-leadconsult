@echo off
SET project=project-leadconsult
SET endpoint=http://192.168.1.10:9000
SET token=4d4e8e1b82b17f35d76503a6eee0be5b70f79b8a

cls 
SonarScanner.MSBuild.exe begin /k:"%project%" /d:sonar.host.url="%endpoint%" /d:sonar.login="%token%"
MSBuild.exe /t:Rebuild
SonarScanner.MSBuild.exe end /d:sonar.login="%token%"