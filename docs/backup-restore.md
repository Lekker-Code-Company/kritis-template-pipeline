### Backup / Restore (catastrophic events) – RTO/RPO responsibility

### Responsibility split

- **Product teams** own:
  - data classification
  - backup configuration (what/when/how long)
  - restore validation (proof it works)
  - RTO/RPO targets and evidence
- **Platform teams** provide:
  - backup tooling (e.g. Velero/K8up)
  - storage primitives (S3/object store, snapshots)
  - standard encryption/key management patterns

### Minimum expectations (audit-ready)

- **RPO** documented per dataset (how much data loss is acceptable).
- **RTO** documented per service (time-to-restore objective).
- **Runbook** with:
  - prerequisite access
  - restore steps
  - validation steps
  - escalation contacts
- **Proof**:
  - scheduled restore tests (e.g. quarterly)
  - evidence artifacts retained (logs/screenshots/IDs)

### Kubernetes patterns (examples)

- Namespace-level backups:
  - manifests (ConfigMaps/Secrets) via GitOps and/or Velero
  - persistent volumes via snapshots or restic
- Database backups:
  - logical dumps + WAL archiving (where appropriate)
  - restore to isolated environment for verification

