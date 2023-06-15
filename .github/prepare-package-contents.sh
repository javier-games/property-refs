rm -r Documentation~/Images
sed -i '/^!\[/d' README.md || sed -i '' '/^!\[/d' README.md
rm -r SourceGenerator~
rm -r .github
rm .gitignore
rm .releaserc.json