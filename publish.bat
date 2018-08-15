@echo off
if "%1"=="" goto nopass
set pass=%1
:dopublish
dotnet publish-ssh --ssh-host 192.168.1.11 --ssh-port 22 --ssh-user pi --ssh-password %pass% --ssh-path /home/pi/netapps/str2ascii
goto end

:nopass
set /p pass=enter password 
goto dopublish

:end