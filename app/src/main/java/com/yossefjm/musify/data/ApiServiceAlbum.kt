package com.yossefjm.musify.data

import android.util.Log
import com.google.gson.Gson
import com.google.gson.annotations.SerializedName
import com.google.gson.reflect.TypeToken
import com.yossefjm.musify.extension.toExtendedIsoString
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song
import okhttp3.OkHttpClient
import okhttp3.Request
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

data class PlaylistDBSQL(
    @SerializedName("data")
    val id: String,

    @SerializedName("NomAlbum")
    var name: String,

    @SerializedName("ArtistaNom")
    var description: String,

    @SerializedName("coverPath")
    val coverPath: String,

    @SerializedName("songs")
    var songs: MutableList<Song>
)

class ApiServiceAlbum {

    val IP_ADRESS = "172.23.3.204"

    private val client = OkHttpClient()
    private val gson = Gson()

    // Método para buscar álbumes por nombre
    fun searchAlbums(query: String): List<Playlist> {
        var responseJson = emptyList<Playlist>()

        try {
            val request = buildRequestAlbums(query)
            client.newCall(request).execute().use { response ->
                if (response.isSuccessful) {
                    val responseBody = response.body?.string()
                    responseJson = fromJsonToPlaylistList(responseBody)
                    Log.d("ApiServiceSQL", "Respuesta exitosa: $responseBody")
                } else {
                    Log.d("ApiServiceSQL", "Respuesta no exitosa: ${response.code}")
                }
            }
        } catch (e: Exception) {
            Log.e("ApiServiceSQL", "Error al realizar la solicitud: ${e.message}", e)
        }

        return responseJson
    }

    private fun fromJsonToPlaylistList(jsonString: String?): List<Playlist> {
        return gson.fromJson<List<PlaylistDBSQL>>(
            jsonString,
            object : TypeToken<List<PlaylistDBSQL>>() {}.type
        ).map { playlistDb ->
            Playlist(
                playlistDb.id,
                playlistDb.name,
                playlistDb.description,
                playlistDb.coverPath,
                MutableList<Song>(0) { Song("0", "0", "0", "0", "0", false) }
            )
        }
    }

    // Método para construir la solicitud HTTP
    private fun buildRequestAlbums(query: String): Request {
        return Request.Builder()
            .url("http://${IP_ADRESS}:1443/api/Album/BuscarNom/$query")
            .get()
            .build()
    }
}
