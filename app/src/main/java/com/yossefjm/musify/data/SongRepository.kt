package com.yossefjm.musify.data

import android.content.ContentUris
import android.content.Context
import android.net.Uri
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
            MediaStore.Audio.Media._ID,
            MediaStore.Audio.Media.TITLE,
            MediaStore.Audio.Media.ARTIST,
            MediaStore.Audio.Media.DATA,
            MediaStore.Audio.Media.ALBUM_ID // Para obtener la carátula del álbum
        )

        context.contentResolver.query(uri, projection, null, null, null)?.use { cursor ->
            val idColumn = cursor.getColumnIndexOrThrow(MediaStore.Audio.Media._ID)
            val titleColumn = cursor.getColumnIndexOrThrow(MediaStore.Audio.Media.TITLE)
            val artistColumn = cursor.getColumnIndexOrThrow(MediaStore.Audio.Media.ARTIST)
            val filePathColumn = cursor.getColumnIndexOrThrow(MediaStore.Audio.Media.DATA)
            val albumIdColumn = cursor.getColumnIndexOrThrow(MediaStore.Audio.Media.ALBUM_ID)

            while (cursor.moveToNext()) {
                val id = cursor.getLong(idColumn).toString()
                val title = cursor.getString(titleColumn)
                val artist = cursor.getString(artistColumn)
                val filePath = cursor.getString(filePathColumn)
                val albumId = cursor.getLong(albumIdColumn)

                // Obtener la carátula usando el ID del álbum
                val coverPath = getAlbumArtPath(albumId)

                val song = Song(id, title, artist, filePath, coverPath, false)
                if (song.title.subSequence(0, 3) != "AUD") {
                    songs.add(song)
                }
            }
        }

        return songs
    }

    private fun getAlbumArtPath(albumId: Long): String {
        val uri = ContentUris.withAppendedId(
            Uri.parse("content://media/external/audio/albumart"),
            albumId
        )
        return uri.toString()
    }

}