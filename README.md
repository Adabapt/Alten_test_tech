# Alten_test_tech

Le repos contient :

Un back en C# .Net écrit sous une architecture micro services avec un découpage en Domain Driven Design.
Un swagger permet de tester l'API. L'API tourne sous l'adresse http://localhost:3000/
Pour la faire fonctionner, il faut changer la valeur du lien vers la base de données, il s'agit de la variable BddAltenProduct dans le fichier appsettings.json de la solution API.

Le Front est donc en Angular avec le framework PrimeNG. Les deux components créés sont dans le dossier components. Un dossier core contient les modèles et les services utilisés par les components.
Enfin, la configuration de l'API est renseignée dans le dossier configBack.json dans le dossier assets/config.

Le script de la base est également dans de le dépôt. Il n'est consituté que d'une table. Et un utilisateur user_back est nécessaire pour dialoguer depuis le Back.

Ce projet m'a pris 1,5j. Si je devais apporter des axes d'amélioration :
  - Utiliser Entity Framework et les DbContext pour manipuler la base depuis le Back
  - Faire des jeux de tests mais pas le temps
  - De même pour la vérification des saisies côtés Back avant d'envoyer en base
  - Utiliser des subscribe pour rafraîchir les listes de produits dans le Front (implique de créer une routine dans le Back également)
