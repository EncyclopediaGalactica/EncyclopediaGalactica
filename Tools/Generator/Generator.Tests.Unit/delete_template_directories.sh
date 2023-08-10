PLATFORM="net7.0"

echo "\n"
echo "==================================="
echo "=== Pre-build events executions ==="
echo "==================================="
echo "\n"

E2E_DIR="bin/Debug/"$PLATFORM"/e2e"
echo "Delete " $E2E_DIR
rm -rf bin/Debug/$PLATFORM/e2e

DTO_DIR="bin/Debug/"$PLATFORM"/Dto"
echo "Delete " $DTO_DIR
rm -rf $DTO_DIR

DTO_TESTS_DIR="bin/Debug/"$PLATFORM"/DtoTests"
echo "Delete " $DTO_TESTS_DIR
rm -rf bin/Debug/$PLATFORM/DtoTests

echo "\n"
echo "======================================="
echo "=== Pre-build events executions end ==="
echo "======================================="
echo "\n"
