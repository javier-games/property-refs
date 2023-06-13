if [ $# -ne 1 ]; then
    exit 1
fi

VERSION="$1"

jq --arg version "${VERSION}" '.version=$version' package.json > changed_version.package.json
mv changed_version.package.json package.json