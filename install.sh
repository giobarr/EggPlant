#!/bin/bash
set -e

SRC_DIR="/home/projects/EggPlant"
DEST_DIR="/opt/egg-sorter"

# Ensure destination exists
mkdir -p "$DEST_DIR"

publish_app() {
	    local app=$1
	        local target=$2

		    echo "Publishing $app -> $target"
		        dotnet publish "$SRC_DIR/$app" -c Release -o "$DEST_DIR/$target"
		}

	# Publish SmartVision, Balance, Sorter
	publish_app "SmartVision" "SmartVision"
	publish_app "Balance" "Balance"
	publish_app "Sorter" "Sorter"

	# Publish PresenceSensor three times
	publish_app "PresenceSensor" "PresenceSensor1"
	publish_app "PresenceSensor" "PresenceSensor2"
	publish_app "PresenceSensor" "PresenceSensor3"

	echo "All applications published to $DEST_DIR"
