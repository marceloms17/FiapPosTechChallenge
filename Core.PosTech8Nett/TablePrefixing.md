# Table Naming Prefix Convention

This document defines the prefix standards used in database table names to maintain clear organization by business domain or functional context.

| Prefix    | Meaning                              | Usage Example                                           |
|-----------|--------------------------------------|---------------------------------------------------------|
| `UAC_`    | User Access Control                  | Tables related to users, authentication, and identity.  |
| `CAT_`    | Catalog                              | Tables for game catalog, genres, and classifications.   |
| `GMS_`    | Games                                | Alternative prefix for game-related entities.           |
| `LIB_`    | Library                              | Tables for user-owned or accessed game libraries.       |
| `ENT_`    | Entertainment                        | General media and entertainment-related data.           |
| `APP_`    | Application                          | Generic or shared application tables.                   |
| `FIN_`    | Finance                              | Tables for billing, payments, and transactions.         |
| `ADM_`    | Administration                       | System configuration and administrative control tables. |

---

## Examples

- `UAC_Users`, `UAC_Contact`
- `CAT_Games`, `CAT_GenreTypes`, `CAT_GameGenres`
- `FIN_Transactions`, `FIN_Wallet`
- `ADM_Parameters`, `ADM_Settings`

---

> Use these prefixes consistently when creating new tables to ensure clarity, modularity, and future scalability of the database structure.
