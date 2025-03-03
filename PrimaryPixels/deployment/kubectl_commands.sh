#!/bin/bash

# Apply namespaces
kubectl apply -f namespace-app.yaml
kubectl apply -f namespace-db.yaml