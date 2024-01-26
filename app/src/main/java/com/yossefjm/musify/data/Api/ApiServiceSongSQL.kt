package com.yossefjm.musify.data

import android.util.Log
import com.google.gson.annotations.SerializedName
import com.yossefjm.musify.model.Song
import retrofit2.Call
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query
import java.io.IOException

data class SongDBSQL(
    @SerializedName("UID")
    val uid: String,

    @SerializedName("data")
    val date: String,

    @SerializedName("NomSong")
    val title: String,

    @SerializedName("Genere")
    val genre: String,

    @SerializedName("songs")
    var songs: MutableList<Song>?
)

data class SongPost(
    val nomSong: String,
    val genere: String,
    val SongExtensio: String
)


interface SongApiServiceSQL {
    @GET("/api/Song/BuscarNom/{query}")
    fun searchSongsByName(@Path("query") query: String): Call<List<SongDBSQL>>

    @POST("/api/Song")
    fun postSong(@Body song: SongPost): Call<SongDBSQL>
}

class ApiServiceSongSQL {
    private val IP_ADDRESS = "192.168.0.16:5010"

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://${IP_ADDRESS}/")
        .addConverterFactory(GsonConverterFactory.create())
        .build()

    private val songApiServiceSQL: SongApiServiceSQL = retrofit.create(SongApiServiceSQL::class.java)

    // Método para buscar canciones por nombre
    fun requestSongsByName(query: String): List<Song> {
        try {
            val response = songApiServiceSQL.searchSongsByName(query).execute()

            if (response.isSuccessful) {
                val songDBList = response.body()
                return fromSongDBListToSongList(songDBList)
            } else {
                Log.d("ApiServiceSQL", "Respuesta no exitosa: ${response.code()}")
            }
        } catch (e: IOException) {
            Log.e("ApiServiceSQL", "Error al realizar la solicitud: ${e.message}", e)
        }

        return emptyList()
    }

    // Método para convertir SongDBSQL a Song
    private fun fromSongDBListToSongList(songDBList: List<SongDBSQL>?): List<Song> {
        return songDBList?.map { songDb ->
            Song(
                songDb.uid,
                songDb.title,
                "",
                "",
                "", // Debes definir cómo obtener estos valores del SongDBSQL
                false
            )
        } ?: emptyList()
    }

    // Método para convertir SongDBSQL a Song
    private fun fromSongDBToSong(songDB: SongDBSQL): Song {
        return Song(
            songDB.uid,
            songDB.title,
            "",
            "",
            "", // Debes definir cómo obtener estos valores del SongDBSQL
            false
        )

    }

    fun postSongSQL(songName: String, genre: String, extensio: String): String {
        var responseJson: Song? = null

        try {
            val songPost = SongPost(songName, genre, extensio)
            val response = songApiServiceSQL.postSong(songPost).execute()

            if (response.isSuccessful) {
                val responseBody = response.body()
                responseJson = fromSongDBToSong(responseBody?: SongDBSQL("","","","",null))
                Log.d("ApiServiceSQL", "Respuesta exitosa: $responseBody")

            } else {
                Log.d("ApiServiceSQL", "Respuesta no exitosa: ${response.code()}")
            }
        } catch (e: Exception) {
            Log.e("ApiServiceSQL", "Error al realizar la solicitud: ${e.message}", e)
        }

        if (responseJson != null) {
            return responseJson.uid
        } else {
            return "null"
        }
    }
}
