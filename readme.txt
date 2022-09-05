1) Установить СУБД Firebird  (Firebird-4.0.2.2816-0-x64.exe)
Все параметры можно оставить по умолчанию


2) после этого можно запускать тестовое приложение (подключение с БД, выполнение запросов). БД при каждом запуске создается и перезатирается
В приложении используяется затянутый через nuget набор библиотек  FirebirdSql.Data.FirebirdClient (https://firebirdsql.org/en/net-provider/)



(минимаьный набор команд для .net https://www.firebirdfaq.org/faq348/)

//////////////////////////////////////////////////////////////////
Как создавать БД через консоль
//////////////////////////////////////////////////////////////////

3) Создать БД .
для этого в меню Пуск в разделе Firebird4.0(x64) запускаем утилиту Firebird ISQL Tool
Следуя инструкции https://firebirdsql.org/manual/bk02ar08s06.html  создаём бд

создаем папку на ПК 
C:\firebird\

последовательно вводим 2 команды 
CREATE DATABASE 'C:\firebird\testdb2.fdb' page_size 8192
user 'SYSDBA' password 'masterkey';

по итогу консольный вывод будет примерно такой 

ISQL Version: WI-V4.0.2.2816 Firebird 4.0
Use CONNECT or CREATE DATABASE to specify a database
SQL> create database 'c:\firebird\testdb2.fdb' page_size 8192
CON> user 'SYSDBA' password 'masterkey';
Server version:
WI-V4.0.2.2816 Firebird 4.0
SQL>

консоль можно закрыть

4) установить какое нибудь приложение для визуального менеджметра БД (https://firebirdsql.org/en/third-party-tools/)
я устанавливал Dbeaver (https://dbeaver.io/)

в приложении подключиться к созданной БД.
