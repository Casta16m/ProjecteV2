package com.yossefjm.musify.data

import android.content.Context
import android.util.Log
import com.google.gson.Gson
import com.google.gson.JsonSyntaxException
import com.google.gson.reflect.TypeToken
import com.yossefjm.musify.extension.toExtendedIsoString
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song
import java.io.File
import java.lang.reflect.Type
import java.text.DateFormat
import java.time.LocalDateTime
import java.time.ZoneId

class PlaylistRepository(private val context: Context) {

    // Esta class se encarga de obtener las canciones de la memoria externa
    val songRepository = SongRepository(context)

    // Este archivo se encarga de almacenar las listas de reproducción
    // en formato JSON en la memoria interna del dispositivo
    private val jsonFile = File(context.filesDir, "data_playlists.json")

    private val gson = Gson()

    var playlistsJson = readPlaylistsJson()
    private val allSongs = songRepository.getAllSongs()


    fun getAllPlaylists(): MutableList<Playlist> {
        playlistsJson = readPlaylistsJson()

        // reescribir la playlist all songs con id 0 en caso de que no exista
        val nullLocalDateTime = LocalDateTime.of(1, 1, 1, 1, 1).toExtendedIsoString()
        playlistsJson.playlists.removeIf { it.id == nullLocalDateTime }

        playlistsJson.playlists.add(
            Playlist(
                LocalDateTime.of(1, 1, 1, 1, 1).toExtendedIsoString(),
                "All Songs",
                "All Songs Playlist",
                "",
                allSongs
            )
        )
        /*
                // Filtrar canciones que no existen
                playlistsJson.playlists.forEach { playlist ->
                    playlist.songs = playlist.songs.filter { song ->
                        allSongs.any { it.id == song.id }
                    }.toMutableList()
                }

                // Eliminar canciones inexistentes en el JSON
                playlistsJson.playlists.forEach { playlist ->
                    val songsToRemove = playlist.songs.filterNot { song ->
                        allSongs.any { it.id == song.id }
                    }
                    playlist.songs.removeAll(songsToRemove)
                }
                */

        val likedPlaylist = playlistsJson.playlists.find { it.id == LocalDateTime.of(2, 2, 2, 2, 2).toExtendedIsoString() }

        if (likedPlaylist == null) {
            playlistsJson.playlists.add(
                Playlist(
                    LocalDateTime.of(2, 2, 2, 2, 2).toExtendedIsoString(),
                    "Liked Songs",
                    "Liked Songs Playlist",
                    "",
                    mutableListOf()
                )
            )
        }

        likesSongs(playlistsJson.playlists, allSongs)

        writePlaylistsJson(playlistsJson)
        return playlistsJson.playlists
    }

    /**
     * Esta funcion busca todas las canciones que estan en la playlist liked songs
     * y las marca como liked en esa lista y en todas las demas
     */
    private fun likesSongs(playlists: MutableList<Playlist>, allSongs: MutableList<Song>) {
        val likedPlaylist = playlists.find { it.id == LocalDateTime.of(1, 1, 1, 1, 1).toExtendedIsoString() }
        val likedSongs = likedPlaylist?.songs
        if (likedSongs != null) {
            likedSongs.forEach { likedSong ->
                allSongs.find { it.id == likedSong.id }?.liked = true
                playlists.forEach { playlist ->
                    playlist.songs.find { it.id == likedSong.id }?.liked = true
                }
            }
        }

    }

    private fun readPlaylistsJson(): PlaylistsJson {
        try {
            if (!jsonFile.exists()) {
                Log.i("PlaylistRepository", "No existe el archivo")
                jsonFile.createNewFile()
            }

            val jsonString = jsonFile.readText()
            val type: Type = object : TypeToken<PlaylistsJson>() {}.type
            Log.d("PlaylistRepository", "readPlaylistsJson: $jsonString")
            return gson.fromJson(jsonString, type) ?: PlaylistsJson(mutableListOf())
        } catch (e: JsonSyntaxException) {
            Log.e("PlaylistRepository", "Error al analizar JSON: ${e.message}", e)
            throw e
        } catch (e: Exception) {
            Log.e("PlaylistRepository", "Error general: ${e.message}", e)
            throw e
        }
    }


    private fun writePlaylistsJson(playlistsJson: PlaylistsJson) {
        if (!jsonFile.exists()) {
            Log.i("PlaylistRepository", "No existe el archivo")
            jsonFile.createNewFile()
        }
        val jsonString = gson.toJson(playlistsJson)
        Log.i("PlaylistRepository", "writePlaylistsJson: $jsonString")
        jsonFile.writeText(jsonString)
    }

    fun addPlaylist(playlist: Playlist) {
        playlistsJson.playlists.add(playlist)
        writePlaylistsJson(playlistsJson)
    }

    fun updatePlaylist(playlist: Playlist) {
        val existingPlaylist = getPlaylistById(playlist.id, playlistsJson)

        if (existingPlaylist != null) {
            val index = playlistsJson.playlists.indexOf(existingPlaylist)
            playlistsJson.playlists[index] = playlist
            writePlaylistsJson(playlistsJson)
        }
    }

    fun deletePlaylist(playlistId: String) {
        val playlistToRemove = getPlaylistById(playlistId, playlistsJson)

        if (playlistToRemove != null) {
            playlistsJson.playlists.remove(playlistToRemove)
            writePlaylistsJson(playlistsJson)
        }
    }

    private fun getPlaylistById(playlistId: String, playlistsJson: PlaylistsJson): Playlist? {
        return playlistsJson.playlists.find { it.id == playlistId }
    }

    /**
     * Esta funcion se encarga de agregar o eliminar una cancion de la playlist liked songs
     * y de marcarla como liked en todas las demas playlists en caso de que se elimine
     * de la playlist liked songs o de desmarcarla en caso de que se agregue
     */
    fun editLikedSong(song: Song) {
        val playlistsJson = readPlaylistsJson()
        val likedPlaylist = playlistsJson.playlists.find { it.id == LocalDateTime.of(1, 1, 1, 1, 1).toExtendedIsoString() }
        val likedSongs = likedPlaylist?.songs
        if (likedSongs != null) {
            if (song.liked) {
                likedSongs.add(song)
                playlistsJson.playlists.forEach { playlist ->
                    playlist.songs.find { it.id == song.id }?.liked = true
                }
            } else {
                likedSongs.remove(song)
                playlistsJson.playlists.forEach { playlist ->
                    playlist.songs.find { it.id == song.id }?.liked = false
                }
            }
        }
        writePlaylistsJson(playlistsJson)
    }

    fun getNewId(): LocalDateTime {
        val currentDateTime = LocalDateTime.now(ZoneId.systemDefault())
        return currentDateTime
    }

    fun addSongToAllSongs(song: Song) {
        val playlistsJson = readPlaylistsJson()
        val allSongsPlaylist = playlistsJson.playlists.find { it.id == LocalDateTime.of(1, 1, 1, 1, 1).toExtendedIsoString() }
        val allSongs = allSongsPlaylist?.songs
        if (allSongs != null) {
            allSongs.add(song)
            Log.d("PlaylistRepository", "addSongToAllSongs: ${allSongs.size}")
        }
        writePlaylistsJson(playlistsJson)
    }
}

data class PlaylistsJson(val playlists: MutableList<Playlist>)