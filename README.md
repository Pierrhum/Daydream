#<center>**DAYDREAMING**</center>

## Comment utiliser Git et GitHub ?
### Création de votre branche :
--> Toujours créer votre branche à partir de la branche "dev".
--> Nommer vos branches en français avec un nom qui rends facile la compréhension de la tâche réalisé sur cette branche en minuscule.
--> Si le nom à plusieurs mots, écrire de la manière suivante : nom-de-la-tache.


:warning: ***! UNE SEULE TÂCHE PAR BRANCHE POUR EVITER LES PULL REQUESTS À RALLONGE !*** :warning: 

### Comment bien push votre code sur la branche ?
--> Utiliser des noms explicites lorsque vous "pushez" vos changements.
--> Description et nom du push en Français.

### Comment bien faire les pull-requests ?
--> Toujours dirigés les pull-requests vers la branche "dev" (sauf la branche "dev" qui elle sera dirigé vers "hotfix" et la branche "hotfix" qui sera dirigé vers la branche "main") .
--> La P.R doit être validé par au moins une personne.
--> Si P.R n'est pas valide laisser une note sur la P.R pour expliquer ce qu'il ne va pas.
--> Supprimer la branche après validation de la P.R pour ne pas polluer le git.

### Cas spécial pour les graphistes :
--> Comme pour les developpeurs, faire une branche par feature à partir de la branche "graphisme". 
--> Pas besoin de passer par la branche "hotfix".

### Descendance des branches Git :
***MAIN*** -> ***HOTFIX*** -> ***DEV*** ou ***BRANCHE GRAPHISME*** -> ***BRANCHE DE LA FEATURE*** 

