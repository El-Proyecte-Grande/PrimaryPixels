#!/bin/bash
eksctl create cluster \
--name primary-p-cluster \
--version 1.31 \
--region eu-west-2 \
--nodegroup-name pp-linux-nodes \
--node-type t2.medium \
--nodes 4