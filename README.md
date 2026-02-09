### Recommendation

Use this repository as the **single source of truth** for CI/CD best-practice templates for KRITIS workloads (NIS-2 / BSI oriented).

It contains:
- **Reusable Azure DevOps pipeline templates** (SonarQube, Trivy, SBOM, Docker, Helm, traceability).
- **Documentation** to make audit evidence repeatable.
- A **fully working example** app (`examples/dotnet-angular/`) that consumes the templates in-repo.

### Why this exists (what it prevents)

- **Copy/paste pipelines drift**: every app slowly becomes unique, insecure, and unmaintainable.
- **“Security later” becomes never**: vuln scanning, SBOMs, and quality gates must be default.
- **Broken audit chain**: requirements → commit → build → image → deployment → pod must be machine-verifiable.

### What you get (high signal)

- **RBAC + ReBAC-ready**: baseline RBAC policy patterns plus optional ReBAC (OpenFGA) wiring in example.
- **SBOM generation**: CycloneDX + SPDX artifacts (filesystem and/or image) published by pipeline.
- **Risk-based prioritization**: documented approach to prioritize remediation by severity + exposure + criticality.
- **Traceability chain**: pipeline variables become OCI labels + Helm annotations + pod env → `/ops/info` endpoint.
- **Backup/restore**: runbooks + RTO/RPO responsibility split (product vs platform).
- **Logging upstream + retention**: stdout-first structured logs; upstream to Grafana/Elastic with retention guidance.

### Repository layout

```
kritis-template-pipeline/
├── templates/                         # reusable Azure DevOps YAML templates
│   ├── steps/
│   └── stages/
├── docs/                              # audit-oriented docs and runbooks
└── examples/
    └── dotnet-angular/                # full stack sample consuming templates
```

### Getting started

#### Use templates from another repo (recommended)

In your app repo `azure-pipelines.yaml`:

```yaml
resources:
  repositories:
    - repository: kritisTemplates
      type: git
      name: <ADO_PROJECT>/kritis-template-pipeline
      ref: refs/heads/main

stages:
  - template: templates/stages/dotnet-angular-ci.yml@kritisTemplates
    parameters:
      backendPath: backend
      frontendPath: frontend
      enableSonar: true
      enableTrivy: true
      enableSbom: true
```

#### Run the in-repo example locally

```powershell
cd .\examples\dotnet-angular\backend
dotnet build
dotnet test

cd ..\frontend
npm ci
npm test
npm run build
```

### Compliance notes (non-negotiable)

- **main branch**: protect it; require PRs; prefer rebase; block direct pushes.
- **No secrets in Git**: pipelines must load from secret stores (ADO variable groups, KeyVault, etc.).
- **Exceptions**: `.trivyignore` requires a justification comment for each suppressed CVE.
