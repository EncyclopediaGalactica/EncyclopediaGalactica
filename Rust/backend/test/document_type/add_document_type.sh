#/bin/bash

# adding
curl \
  -X POST \
  -H "Content-Type: application/json" \
  -d '{"name": "from test script"}' \
  http://localhost:8080/document_type
