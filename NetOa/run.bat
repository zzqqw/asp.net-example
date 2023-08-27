@echo off
 
title Asp.Net Core publish

echo ------------------------------------------------
echo    下载安装 dotnet-sdk-2.1.201-win-x64
echo    下载安装 DotNetCore.2.0.8-WindowsHosting
echo    下载安装 sqlite-tools-win32-x86-3240000.zip
echo    下载安装 sqlite-dll-win64-x64-3240000
echo ------------------------------------------------

dotnet publish -r win-x64

echo %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\

echo 正在删除 %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\NetOA.sqlite

del %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\NetOA.sqlite

echo 已经删除 %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\NetOA.sqlite

echo 复制 %cd%\NetOA.sqlite 到 %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\

copy %cd%\NetOA.sqlite  %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\

echo 复制完成 

cls

echo ------------------------------------------------
echo 发布已完成
echo 1.网站执行的根目录 %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\
echo 2.%cd%\bin\Debug\netcoreapp2.0\win-x64\publish\NetOA.sqlite 文件权限配置
echo 3.重启IIS
echo 4.按照自己IIS配置进行访问



pause