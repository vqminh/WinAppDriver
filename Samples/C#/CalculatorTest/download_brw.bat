call cleanup.bat
gsutil cp gs://lender-rate.appspot.com/%1/BRW/* C:\Users\ADMIN\Documents\input\
for /r %%x in (*) do move "%%x" "%%x.brw"
