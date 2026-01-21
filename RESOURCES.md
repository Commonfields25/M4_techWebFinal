# Catalogue de Ressources — M4Webapp

Objectif : centraliser des ressources d'apprentissage variées (Microsoft Learn + plateformes externes) en traitant toutes les sources de façon neutre et équitable. L'application incite à l'utilisation d'outils d'apprentissage divers pour favoriser une concurrence saine et l'accès au savoir.

---

## Principes éditoriaux

- Neutralité : aucune préférence affichée pour une provenance (Microsoft vs externe). La source existe dans les données (`Source`) mais n'est pas mise en avant par défaut.
- Équité : les ressources sont classées et filtrées selon qualité, niveau et surtout gratuité — pas selon la marque.
- Gratuité priorisée : par défaut l'application montre les ressources gratuites (IsFree = true). Les contenus payants (y compris pay-to-cert) sont marqués et exclus par défaut.
- Encouragement à la diversité : promouvoir freeCodeCamp, Khan Academy, LearnCpp, cppreference, SQL interactif, Linux docs, etc., sur un pied d'égalité avec Microsoft Learn.
- Transparence sur les ressources payantes : toute ressource payante doit être marquée `IsFree = false`. Si une ressource propose un contenu gratuit partiel (freemium), indiquer `IsFree = true` pour la partie gratuite et ajouter un champ `Notes` côté Admin pour clarifier le modèle économique.

---

## Comportement de l'application

- Vue par défaut : affiche uniquement les ressources gratuites (filtre `isFree=true`) pour favoriser l'accès des étudiants.
- Tri : les ressources gratuites sont présentées en priorité. L'utilisateur peut enlever le filtre "Gratuit" pour voir l'ensemble.
- Badge affiché : chaque carte montre clairement `Gratuit` ou `Payant`.
- Source : visible dans la fiche détaillée (ou sur demande), mais pas mise en avant dans le listing principal.

---

## Organisation des assets et des données

- Images / logos : `wwwroot/assets/images/` — noms sémantiques (ex: `freecodecamp.svg`, `learncpp.svg`).
- Styles / scripts :
  - CSS global : `wwwroot/css/shared/site.css`, `wwwroot/css/shared/theme.css`
  - CSS page-specific : `wwwroot/css/pages/home-resources.css`, `wwwroot/css/pages/admin-resources.css`
  - JS global : `wwwroot/js/shared/site.js`
  - JS page-specific : `wwwroot/js/pages/home-resources.js`, `wwwroot/js/pages/admin-resources.js`
- Données sources modifiables (import) : `Data/resources.json` — format attendu : tableau d'objets `Resource` (voir `Models/Resource`).
- Base de données (SQLite) : `data/m4webapp.db` (production/dev) ; `SeedData` lit `Data/resources.json` au premier démarrage si présent.

---

## Marquage des ressources payantes / certifications

- `IsFree` (bool) : indique si la ressource est gratuite. Par défaut, seuls les éléments avec `IsFree = true` sont visibles.
- `Source` (string) : origine (ex: `MicrosoftLearn`, `External`, `Custom`) — utile pour rapports ou filtres avancés.
- `ImageUrl` : chemin relatif vers le logo (ex: `/assets/images/freecodecamp.svg`).
- `Notes` (Admin) : champ libre pour préciser si une ressource est partiellement gratuite ou freemium.

Administration :
- L'admin peut importer un `resources.json` ou utiliser l'UI CRUD (si installé) pour marquer une ressource payante ou ajouter des notes.
- Les certifications payantes (ex: examens officiels payants) doivent avoir `IsFree = false` et, si souhaité, `Notes` indiquant la possibilité de suivi gratuit (si disponible).

---

## Guide rapide pour contributeurs / étudiants

- Si vous suggérez une ressource, priorisez les contenus gratuits et pédagogiques.
- Pour une ressource freemium, documentez précisément la partie gratuite et mettez un lien clair vers la partie payante dans `Notes`.
- Pour proposer une ressource : créez une issue ou ouvrez une PR modifiant `Data/resources.json` ou ajoutez via l'Admin (si activé).

---

## Exemple minimal de `Data/resources.json`

```json
[
  {
    "title": "freeCodeCamp",
    "description": "Cours gratuits et exercices pratiques.",
    "category": "Learning Platforms",
    "url": "https://www.freecodecamp.org/",
    "source": "External",
    "imageUrl": "/assets/images/freecodecamp.svg",
    "isFree": true,
    "difficulty": "Beginner",
    "topics": ["Web", "JavaScript"]
  }
]
```

---

## Notes techniques

- Par défaut le contrôleur `Home/Resources` applique `isFree=true`. Un étudiant peut désactiver ce filtre via l'interface pour voir d'autres contenus.
- Le service de recherche (`ISearchService`) ordonne par gratuité d'abord, puis par le critère demandé (titre, difficulté, etc.).
- Les images doivent être optimisées (SVG recommandé) et nommées sémantiquement.

---

Dernière mise à jour : 2026-01-01
