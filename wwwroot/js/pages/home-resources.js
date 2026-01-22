function resourceManager() {
    return {
        favorites: [],

        /* Initialisation du composant */
        init() {
            const stored = localStorage.getItem('favorites');
            this.favorites = stored ? JSON.parse(stored) : [];
        },

        /* Ajouter / retirer un favori */
        toggleFavorite(id) {
            if (this.isFavorite(id)) {
                this.favorites = this.favorites.filter(favId => favId !== id);
            } else {
                this.favorites.push(id);
            }

            this.persist();
        },

        /* Vérifier si une ressource est en favori */
        isFavorite(id) {
            return this.favorites.includes(id);
        },

        /* Sauvegarde centralisée */
        persist() {
            localStorage.setItem('favorites', JSON.stringify(this.favorites));
        }
    };
}
