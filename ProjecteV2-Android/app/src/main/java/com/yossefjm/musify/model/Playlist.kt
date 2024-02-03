package com.yossefjm.musify.model

import com.yossefjm.musify.DownloadActivity
import java.text.DateFormat
import java.time.LocalDateTime

/**
 * Clase que representa una lista de reproducción
 * Esta clase es responsable de almacenar la información de una lista de reproducción
 */
data class Playlist(
    val id: String,
    var name: String,
    var description: String,
    val coverPath: String?,
    var songs: MutableList<Song>
)

