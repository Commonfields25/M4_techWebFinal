function resourceManager() {
    return {
        favorites: JSON.parse(localStorage.getItem('favorites') || '[]'),

        toggleFavorite(id) {
            if (this.isFavorite(id)) {
                this.favorites = this.favorites.filter(favId => favId !== id);
            } else {
                this.favorites.push(id);
            }
            localStorage.setItem('favorites', JSON.stringify(this.favorites));
        },

        isFavorite(id) {
            return this.favorites.includes(id);
        }
    }
}
