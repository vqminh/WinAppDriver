setlocal EnableDelayedExpansion
set suffix=0
cd C:\Users\ADMIN\Documents\input\
for /F "delims=" %%i in ('dir /b /a-d') do (
   set /A suffix+=1
   ren "%%i" "!suffix!.%1"
)