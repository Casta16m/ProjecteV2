package com.yossefjm.musify.model

/**
 * Clase que representa una canción
 * Esta clase es responsable de almacenar la información de una canción
 */
data class Song(
    val uid: String, // ID único de la canción
    val title: String,
    val artist: String,
    var songPath: String,
    val coverPath: String,
    var liked: Boolean
)