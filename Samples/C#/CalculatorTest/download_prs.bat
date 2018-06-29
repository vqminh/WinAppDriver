call cleanup.bat
call gsutil -m mv gs://lender-rate.appspot.com/%1/PRS/* C:\Users\ADMIN\Documents\input\
call rename.bat prs
