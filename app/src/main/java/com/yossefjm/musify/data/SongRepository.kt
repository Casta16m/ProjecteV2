package com.yossefjm.musify.data

import android.content.Context
import android.provider.MediaStore
import com.yossefjm.musify.model.Song

/**
 * Clase que representa un repositorio de canciones
 * Esta clase es responsable de obtener las canciones de la memoria externa
 */
class SongRepository(private val context: Context) {

    fun getAllSongs(): MutableList<Song> {
        val songs = mutableListOf<Song>()

        val uri = MediaStore.Audio.Media.EXTERNAL_CONTENT_URI
        val projection = arrayOf(
            MediaStore.Audio.Media.TITLE,
            MediaStore.Audio.Media.ARTIST,
            MediaStore.Audio.Media.DATA
        )

        context.contentResolver.query(uri, projection, null, null, null)?.use { cursor ->
            val titleColumn = cursor.getColumnIndexOrThrow(MediaStore.Audio.Media.TITLE)
            val artistColumn = cursor.getColumnIndexOrThrow(MediaStore.Audio.Media.ARTIST)
            val filePathColumn = cursor.getColumnIndexOrThrow(MediaStore.Audio.Media.DATA)

            while (cursor.moveToNext()) {
                val title = cursor.getString(titleColumn)
                val artist = cursor.getString(artistColumn)
                val filePath = cursor.getString(filePathColumn)
                val covePath = cursor.getString(filePathColumn)


                val song = Song(0, title, artist, filePath, "", false)
                if (song.title.subSequence(0, 3) != "AUD"){
                    songs.add(song)
                }
            }
        }

        return songs
    }

    // Otras funciones relacionadas con las canciones (agregar, eliminar, etc.)
}