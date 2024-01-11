package com.yossefjm.musify.model

/**
 * Clase que representa una lista de reproducción
 * Esta clase es responsable de almacenar la información de una lista de reproducción
 */
data class Playlist(
    val id: Long,
    var name: String,
    var description: String,
    val coverPath: String,
    var songs: MutableList<Song>
    // Puedes agregar más propiedades según sea necesario
)

