package com.yossefjm.musify.data

import com.google.gson.annotations.SerializedName
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.GET
import retrofit2.http.Path
import java.io.IOException

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

interface AlbumApiService {
    @GET("/api/Album/BuscarNom/{query}")
    fun searchAlbums(@Path("query") query: String): Call<List<PlaylistDBSQL>>
}

class ApiServiceAlbum {

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://192.168.0.16:1443/")
        .addConverterFactory(GsonConverterFactory.create())
        .build()

    private val albumApiService: AlbumApiService = retrofit.create(AlbumApiService::class.java)

    // Método para buscar álbumes por nombre
    fun searchAlbums(query: String): List<Playlist> {
        try {
            val response = albumApiService.searchAlbums(query).execute()

            if (response.isSuccessful) {
                val playlistDBList = response.body()
                return fromPlaylistDBListToPlaylistList(playlistDBList)
            } else {
                println("Respuesta no exitosa: ${response.code()}")
            }
        } catch (e: IOException) {
            println("Error al realizar la solicitud: ${e.message}")
        }

        return emptyList()
    }

    private fun fromPlaylistDBListToPlaylistList(playlistDBList: List<PlaylistDBSQL>?): List<Playlist> {
        return playlistDBList?.map { playlistDb ->
            Playlist(
                playlistDb.id,
                playlistDb.name,
                playlistDb.description,
                playlistDb.coverPath,
                MutableList<Song>(0) { Song("0", "0", "0", "0", "0", false) }
            )
        } ?: emptyList()
    }
}
