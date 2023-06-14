if [ $# -ne 2 ]; then
  exit 0
fi

OVERWRITE_PUBLISH_CONFIG="$1"

if [ -n "$OVERWRITE_PUBLISH_CONFIG" ]; then
  jq --arg publishConfig "$OVERWRITE_PUBLISH_CONFIG" '. + {"publishConfig":{"registry": $publishConfig }}' package.json > tmp.$$.json
  mv tmp.$$.json package.json
fi

GIT_URL="$2"

if [ -n "$GIT_URL" ]; then
  jq --arg gitUrl "$GIT_URL" '. + { "repository" : $gitUrl }' package.json > tmp.$$.json
  mv tmp.$$.json package.json
fi
