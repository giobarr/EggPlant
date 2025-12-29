#!/bin/bash
set -e

BASE_DIR="/opt/egg-sorter"

# Ensure directories exist
mkdir -p "$BASE_DIR/PresenceSensor1"
mkdir -p "$BASE_DIR/PresenceSensor2"
mkdir -p "$BASE_DIR/PresenceSensor3"

# Create config.cfg files pointing to the right script
echo "$BASE_DIR/PresenceSensor1/SmartVision.sh" > "$BASE_DIR/PresenceSensor1/config.cfg"
echo "$BASE_DIR/PresenceSensor2/Balance.sh"    > "$BASE_DIR/PresenceSensor2/config.cfg"
echo "$BASE_DIR/PresenceSensor3/Sorter.sh"     > "$BASE_DIR/PresenceSensor3/config.cfg"

# Create wrapper scripts
cat > "$BASE_DIR/SmartVision.sh" <<'EOF'
#!/bin/bash
APP_DIR="/opt/egg-sorter/SmartVision"
dotnet "$APP_DIR/SmartVision.dll"
EOF

cat > "$BASE_DIR/Balance.sh" <<'EOF'
#!/bin/bash
APP_DIR="/opt/egg-sorter/Balance"
dotnet "$APP_DIR/Balance.dll"
EOF

cat > "$BASE_DIR/Sorter.sh" <<'EOF'
#!/bin/bash
APP_DIR="/opt/egg-sorter/Sorter"

# Check if Sorter.dll is already running
if pgrep -f "dotnet $APP_DIR/Sorter.dll" > /dev/null; then
    echo "Sorter is already running."
    exit 0
fi

echo "Starting Sorter..."

dotnet "$APP_DIR/Sorter.dll"
EOF

# Make scripts executable
chmod +x "$BASE_DIR/SmartVision.sh" "$BASE_DIR/Balance.sh" "$BASE_DIR/Sorter.sh"

# Copy wrapper scripts into each PresenceSensor folder for convenience
cp "$BASE_DIR/SmartVision.sh" "$BASE_DIR/PresenceSensor1/"
cp "$BASE_DIR/Balance.sh"    "$BASE_DIR/PresenceSensor2/"
cp "$BASE_DIR/Sorter.sh"     "$BASE_DIR/PresenceSensor3/"

echo "Config files and wrapper scripts created in $BASE_DIR"
