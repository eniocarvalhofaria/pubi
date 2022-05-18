SET ENDLOADFILE="FILES\%1\CONTROL\%1.endload"

:loop

if exist %ENDLOADFILE% goto end

timeout 60

goto loop

:end




