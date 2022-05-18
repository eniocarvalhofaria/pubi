cd ..\Java\salesforce-replica
set LOADLOG="..\..\CARGASF\LOG\%1.Load.%datetimef%.log" 
SalesForceCopyLoader\bin\Debug\SalesforceCopyloader.exe --address sqlaapddp13.peixeurbano.local,1443 --database SALESFORCE --listenerdirectory "..\..\CARGASF\FILES\%1"  --redshiftuid  usr_carga_salesforce --redshiftpwd  xPfXEaY3r3b0LthZ7  --s3accesskey  AKIAJDW3S47FIYSOKIVQ --s3secretkey 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  --target redshift   >> %LOADLOG% 2>&1

cd ..\..\CARGASF
SET ENDLOADFILE="FILES\%1\CONTROL\%1.endload"
type nul >%ENDLOADFILE%
EXIT

