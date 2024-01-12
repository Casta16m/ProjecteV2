package com.yossefjm.musify.data

import android.content.Context
import android.util.Log
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song
import java.io.File
import java.lang.reflect.Type

class PlaylistRepository(private val context: Context) {

    // Esta class se encarga de obtener las canciones de la memoria externa
    val songRepository = SongRepository(context)

    // Este archivo se encarga de almacenar las listas de reproducción
    // en formato JSON en la memoria interna del dispositivo
    private val jsonFile = File(context.filesDir, "data_playlists.json")

    private val gson = Gson()

    val playlistsJson = readPlaylistsJson()
    private val allSongs = songRepository.getAllSongs()


    fun getAllPlaylists(): MutableList<Playlist> {
        // reescribir la playlist all songs con id 0 en caso de que no exista
        playlistsJson.playlists.removeIf { it.id == 0L }

        playlistsJson.playlists.add(
            Playlist(
                0,
                "All Songs",
                "All Songs Playlist",
                "",
                allSongs
            )
        )

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

        val likedPlaylist = playlistsJson.playlists.find { it.id == 1L }

        if (likedPlaylist == null) {
            playlistsJson.playlists.add(
                Playlist(
                    1,
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
        val likedPlaylist = playlists.find { it.id == 1L }
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
        if (!jsonFile.exists()) {
            Log.i("PlaylistRepository", "No existe el archivo")
            jsonFile.createNewFile()
        }

        val jsonString = jsonFile.readText()
        val type: Type = object : TypeToken<PlaylistsJson>() {}.type
        return gson.fromJson(jsonString, type) ?: PlaylistsJson(mutableListOf())
    }

    private fun writePlaylistsJson(playlistsJson: PlaylistsJson) {
        if (!jsonFile.exists()) {
            Log.i("PlaylistRepository", "No existe el archivo")
            jsonFile.createNewFile()
        }
        val jsonString = gson.toJson(playlistsJson)
        jsonFile.writeText(jsonString)
        Log.i("PlaylistRepository", "writePlaylistsJson: $jsonString")
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

    fun deletePlaylist(playlistId: Long) {
        val playlistToRemove = getPlaylistById(playlistId, playlistsJson)

        if (playlistToRemove != null) {
            playlistsJson.playlists.remove(playlistToRemove)
            writePlaylistsJson(playlistsJson)
        }
    }

    private fun getPlaylistById(playlistId: Long, playlistsJson: PlaylistsJson): Playlist? {
        return playlistsJson.playlists.find { it.id == playlistId }
    }

    /**
     * Esta funcion se encarga de agregar o eliminar una cancion de la playlist liked songs
     * y de marcarla como liked en todas las demas playlists en caso de que se elimine
     * de la playlist liked songs o de desmarcarla en caso de que se agregue
     */
    fun editLikedSong(song: Song) {
        val playlistsJson = readPlaylistsJson()
        val likedPlaylist = playlistsJson.playlists.find { it.id == 1L }
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

    fun getNewId(): Long {
        val playlistsJson = readPlaylistsJson()
        return playlistsJson.playlists.maxByOrNull { it.id }?.id?.plus(1) ?: 0
    }
}

data class PlaylistsJson(val playlists: MutableList<Playlist>)