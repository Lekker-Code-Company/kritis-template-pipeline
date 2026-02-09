### SBOM generation and risk-based prioritization

### SBOM (Software Bill of Materials)

This template generates SBOMs as **build artifacts**:

- **CycloneDX JSON**: for tooling ecosystems and vulnerability management.
- **SPDX JSON**: for compliance/audit interoperability.

SBOM sources:
- filesystem SBOM (source + lockfiles) for early visibility
- image SBOM (recommended) for “what actually runs”

### Risk-based prioritization (for auditability)

Do not treat all findings equally. Prioritize remediation using a simple, explainable model:

1. **Severity** (e.g. `CRITICAL/HIGH`) and exploitability
2. **Exposure** (internet-facing, internal-only, isolated)
3. **Asset criticality** (business impact, safety relevance)
4. **Compensating controls** (WAF, network policy, runtime hardening)

Evidence to keep:
- scan reports (Trivy JSON/SARIF)
- SBOMs (CycloneDX/SPDX)
- decisions: accepted risk / mitigation plan / timeline

