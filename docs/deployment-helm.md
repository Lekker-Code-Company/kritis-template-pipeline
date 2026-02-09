### Deployment (Helm) – good practices aligned with `mccs-helm-common`

This repository’s example chart (`examples/dotnet-angular/deployment/chart`) is intentionally built as a thin wrapper around `mccs-helm-common` charts:

- `mccs-dot-net`
- `mccs-frontend`

### Baselines you must enforce

- **Labels** (for ownership, selectors, traceability)
  - Use `mccs-common.labels.labels` on workloads
  - Use `mccs-common.labels.selectorLabels` for pod selectors
- **Security context**
  - `runAsNonRoot: true`
  - `readOnlyRootFilesystem: true`
  - `allowPrivilegeEscalation: false`
  - `capabilities.drop: ["ALL"]`
- **Health endpoints**
  - liveness/readiness/startup probes must exist and be cheap
- **Downward API**
  - inject pod identity into env vars for `/ops/info` traceability

### Template testing

The pipeline template runs:
- `helm lint`
- `helm template`

For local development (when Helm is available), follow `mccs-helm-common` guidance:
- add chart dependencies via `file://...` during development
- run `helm dependency build`

