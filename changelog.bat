cd "C:\Transfer\SimpleRpg\SimpleRPG"
git pull
echo SimpleRPG > README.md
echo ========= >> README.md
echo. >> README.md
echo. >> README.md

git log --no-merges --date=iso8601 --pretty=format:"%%ad %%s (%%an)%%n" >> README.md

git add .
git commit -am "Test"
git push