call cleanup.bat
gsutil cp gs://lender-rate.appspot.com/%1/BRW/* C:\Users\ADMIN\Documents\input\
setlocal EnableDelayedExpansion
set filename=A
set suffix=1
for /F "delims=" %%i in ('dir') do (
   set /A suffix+=1
   ren "%%i" "%filename%-!suffix:~1!.brw"
)
