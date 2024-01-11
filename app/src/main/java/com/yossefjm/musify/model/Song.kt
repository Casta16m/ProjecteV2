package com.yossefjm.musify.model

/**
 * Clase que representa una canción
 * Esta clase es responsable de almacenar la información de una canción
 */
data class Song(
    val id: Long, // ID único de la canción
    val title: String,
    val artist: String,
    val songPath: String,
    val coverPath: String,
    var liked: Boolean
    // Puedes agregar más propiedades según sea necesario
) {
}