### Logging upstream (Grafana/Elastic) and retention

### Recommendation

- **Log to stdout/stderr only** from workloads.
- Use **structured JSON logs** where possible.
- Let the platform ship logs to Loki/Elastic (promtail/fluent-bit/filebeat).

### Audit retention duties

You must define and evidence:
- **retention period** per log type (security/audit vs application debug)
- **access control** (who can read)
- **immutability** expectations (audit logs should be append-only)
- **PII masking** and minimization

### Pipeline artifacts to retain

- test results, coverage
- SonarQube report references (project key + analysis ID)
- Trivy reports (filesystem + image)
- SBOMs (CycloneDX/SPDX)
- deployment manifests / helm release metadata (at least version/digest references)

