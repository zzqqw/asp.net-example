@echo off
 
title Asp.Net Core publish

echo ------------------------------------------------
echo    ���ذ�װ dotnet-sdk-2.1.201-win-x64
echo    ���ذ�װ DotNetCore.2.0.8-WindowsHosting
echo    ���ذ�װ sqlite-tools-win32-x86-3240000.zip
echo    ���ذ�װ sqlite-dll-win64-x64-3240000
echo ------------------------------------------------

dotnet publish -r win-x64

echo %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\

echo ����ɾ�� %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\NetOA.sqlite

del %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\NetOA.sqlite

echo �Ѿ�ɾ�� %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\NetOA.sqlite

echo ���� %cd%\NetOA.sqlite �� %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\

copy %cd%\NetOA.sqlite  %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\

echo ������� 

cls

echo ------------------------------------------------
echo ���������
echo 1.��վִ�еĸ�Ŀ¼ %cd%\bin\Debug\netcoreapp2.0\win-x64\publish\
echo 2.%cd%\bin\Debug\netcoreapp2.0\win-x64\publish\NetOA.sqlite �ļ�Ȩ������
echo 3.����IIS
echo 4.�����Լ�IIS���ý��з���



pause