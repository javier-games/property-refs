if [ $# -ne 1 ]; then
    exit 0
fi

NAME="$1"

if [ -z "$NAME" ]; then
    exit 0
fi

jq --arg name "$NAME" '.name=$name' package.json > tmp.package.json
mv tmp.package.json package.json