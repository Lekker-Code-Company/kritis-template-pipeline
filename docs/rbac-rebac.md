### RBAC / ReBAC (OpenFGA) – what to adopt

### Recommendation

- **Start with RBAC** for coarse-grained access (roles/groups from the IdP).
- Add **ReBAC (e.g. OpenFGA)** for fine-grained object permissions (ownership, sharing, delegation).
- Keep RBAC and ReBAC **separate**: RBAC is “who are you?”, ReBAC is “what can you do to this object?”

### RBAC (roles)

- Source of truth: **Identity Provider** (Keycloak / Azure AD).
- Runtime: JWT claim (commonly `roles`).
- Implementation:
  - `RequireRole("kritis.admin")` for admin actions
  - policies for domain actions (e.g. `documents:write`)

### ReBAC (relationships)

- Source of truth: Relationship tuples (e.g. `user:alice#viewer@document:123`)
- Engine: OpenFGA (Zanzibar model)
- Typical relations:
  - `document` has `owner`, `writer`, `viewer`
  - `viewer = this OR writer` (and optionally `owner`)

### Operational constraints (KRITIS)

- ReBAC must be **highly available** and horizontally scalable.
- Cache decisions carefully and short-lived; authorization is security-critical.
- A failure mode must be explicit:
  - Fail closed for privileged actions
  - For read-only paths you may choose bounded “grace” (must be documented)

