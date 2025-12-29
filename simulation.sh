#!/bin/bash
set -e

BASE_DIR="/opt/egg-sorter"

echo "Starting Egg Sorter production line simulation..."
echo "Press Ctrl+C to stop."

while true; do
    echo ">>> Running PresenceSensor1"
    "$BASE_DIR/PresenceSensor1/PresenceSensor" &
    wait $!
    sleep 5

    echo ">>> Running PresenceSensor2"
    "$BASE_DIR/PresenceSensor2/PresenceSensor" &
    wait $!
    sleep 2

    echo ">>> Running PresenceSensor3"
    "$BASE_DIR/PresenceSensor3/PresenceSensor" &
    wait $!

    echo "Cycle complete. Restarting..."
done
