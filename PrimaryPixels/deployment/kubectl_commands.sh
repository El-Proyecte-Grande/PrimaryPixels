#!/bin/bash

# Apply namespaces
kubectl apply -f namespace-app.yaml
kubectl apply -f namespace-db.yaml

# Apply deployments
kubectl apply -f deployment-frontend.yaml
kubectl apply -f deployment-backend.yaml

# Apply services
kubectl apply -f service-backend.yaml
kubectl apply -f service-frontend.yaml

# Apply ingress
# kubectl apply -f ingress.yaml