#!/bin/bash

# delete ingress
# kubectl delete -f ingress.yaml

# delete services
kubectl delete -f service-backend.yaml
kubectl delete -f service-frontend.yaml

# delete deployments
kubectl delete -f deployment-frontend.yaml
kubectl delete -f deployment-backend.yaml

# delete namespaces
kubectl delete -f namespace-app.yaml
kubectl delete -f namespace-db.yaml