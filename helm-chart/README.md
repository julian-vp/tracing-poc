kubectl create namespace tracing-poc

helm upgrade --install tracing-poc ./helm-chart/ -n tracing-poc