if [ $# -ne 1 ]; then
    exit 0
fi

REGISTRY_URL="$1"

if [ -z "$REGISTRY_URL" ]; then
    exit 0
fi

jq --arg registryUrl "$REGISTRY_URL" '. + { "publishConfig": { "registry": $registryUrl }}' package.json > tmp.$$.json
mv tmp.$$.json package.json