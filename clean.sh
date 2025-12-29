#!/bin/bash
set -e

SRC_DIR="/home/projects/EggPlant"

echo "Cleaning build artifacts under $SRC_DIR..."

# Find and remove all bin/ and obj/ directories
find "$SRC_DIR" -type d -name "bin" -exec rm -rf {} +
find "$SRC_DIR" -type d -name "obj" -exec rm -rf {} +

echo "Cleanup complete."
