### Traceability (kausale Kette) – Requirements → Commit → Deployment Pod

This template enforces **machine-verifiable** evidence for the causal chain.

### Target evidence

- **Requirement ID**: a stable identifier from your backlog (e.g. `REQ-1234`, or ADO work item ID).
- **Commit SHA**: `Build.SourceVersion`.
- **Build ID**: `Build.BuildId`.
- **Container image digest**: immutable reference (preferred over tags).
- **Helm release + chart version**.
- **Pod identity**: `metadata.uid` (unique) and `metadata.name` (operational).

### Implementation pattern used here

- **Pipeline** sets variables:
  - `GIT_SHA=$(Build.SourceVersion)`
  - `BUILD_ID=$(Build.BuildId)`
  - `REQUIREMENT_ID` (derived from PR title/branch naming or explicitly set)
- **Docker build** injects:
  - OCI labels: `org.opencontainers.image.revision`, `org.opencontainers.image.source`, `org.opencontainers.image.created`
  - env vars (optional): `GIT_SHA`, `BUILD_ID`, `REQUIREMENT_ID`
- **Helm chart** injects:
  - pod annotations and labels for `gitSha`, `buildId`, `requirementId`
  - Downward API env vars: `POD_NAME`, `POD_UID`, `POD_NAMESPACE`, `NODE_NAME`
- **Application** exposes `/ops/info` returning:
  - `gitSha`, `buildId`, `requirementId`, `pod.uid/name`

### Audit checklist

- [ ] Each deployment artifact references an immutable **image digest**
- [ ] Pod template contains traceability labels/annotations
- [ ] Application reports build metadata at runtime (`/ops/info`)
- [ ] Pipeline retains artifacts (SBOMs, scan results, test reports) per retention policy

