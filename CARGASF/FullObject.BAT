SET SFPWD=MelhorEquipePU1
SET SFTOKEN=zW3IRC7OzrEy6Ww22e6cFz9G7
SET SFUSER=dw@peixeurbano.com
set datetimef=%date:~6,4%%date:~3,2%%date:~0,2%.%time:~0,2%%time:~3,2%%time:~6,2%
echo %datetimef%
echo object %1

SET ENDLOADFILE="FILES\%1\CONTROL\%1.endload"

if exist %ENDLOADFILE% del %ENDLOADFILE%

cd ..\Java\salesforce-replica

SET FILESDIR="..\..\CARGASF\FILES\%1%"

if not exist %FILESDIR%    mkdir %FILESDIR%
if not exist "..\..\CARGASF\LOG"   mkdir "..\..\CARGASF\LOG"


set EXTRACTLOG="..\..\CARGASF\LOG\%1.Extract.%datetimef%.log" 

java -jar TARGET\salesforce-replica-1.0-SNAPSHOT-jar-with-dependencies.jar -a bulk-copy -o %1 -s 1 -e  "NA" -b  SALESFORCE -t %FILESDIR%  -u NA -p NA -x NA  -k NA -y NA  -r  NA -j  NA   >> %EXTRACTLOG% 2>&1 

echo %ERRORLEVEL%

cd ..\..\cargasf
start CopyLoader.bat  %1



