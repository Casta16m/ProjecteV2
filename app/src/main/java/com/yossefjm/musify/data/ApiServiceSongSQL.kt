package com.yossefjm.musify.data

import android.util.Log
import com.google.gson.Gson
import com.google.gson.annotations.SerializedName
import com.google.gson.reflect.TypeToken
import com.yossefjm.musify.model.Song
import okhttp3.MediaType
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.RequestBody
import okhttp3.RequestBody.Companion.toRequestBody
import org.json.JSONObject
import java.time.format.DateTimeFormatter
import java.io.File
import java.io.FileOutputStream

data class SongDBSQL(
    @SerializedName("UID")
    val id: String,

    @SerializedName("data")
    val date: String,

    @SerializedName("NomSong")
    val title: String,

    @SerializedName("Genere")
    val genre: String,

    @SerializedName("songs")
    var songs: MutableList<Song>?
)

class ApiServiceSongSQL {

    val IP_ADRESS = "172.23.3.204"

    val destinationPath = "/ruta/del/destino/audio.mp3"

    private val client = OkHttpClient()
    private val gson = Gson()



    // Método para buscar canciones por nombre
    fun makeRequestApiSongsByName(query: String): List<Song> {
        var responseJson = emptyList<Song>()

        try {
            val request = Request.Builder()
                .url("http://${IP_ADRESS}:1443/api/Song/BuscarNom/$query")
                .get()
                .build()


            client.newCall(request).execute().use { response ->
                if (response.isSuccessful) {
                    val responseBody = response.body?.string()
                    responseJson = fromJsonToSongList(responseBody)
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

    // Método para convertir JSON a List<Song>
    private fun fromJsonToSongList(jsonString: String?): List<Song> {
        val formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss.SSSSSSS")

        return gson.fromJson<List<SongDBSQL>>(
            jsonString,
            object : TypeToken<List<SongDBSQL>>() {}.type
        ).map { songDb ->
            Song(
                songDb.id,
                songDb.title,
                "",
                "",
                "", // Debes definir cómo obtener estos valores del JSON
                false
            )
        }
    }

    fun postSong(songName: String, genre: String) : String {
        try {
            // Crear el objeto JSON para la solicitud
            val json = JSONObject()
            json.put("NomSong", songName)
            json.put("Genere", genre)

            // Construir la solicitud HTTP con el cuerpo JSON
            val body = json.toString().toRequestBody("application/json; charset=utf-8".toMediaTypeOrNull())
            val request = Request.Builder()
                .url("http://${IP_ADRESS}:1443/api/Song")
                .post(body)
                .build()

            // Realizar la solicitud y procesar la respuesta
            client.newCall(request).execute().use { response ->
                if (response.isSuccessful) {
                    val responseBody = response.body?.string()
                    val responseJson = fromJsonToSongList(responseBody)
                    Log.d("ApiServiceSQL", "Respuesta exitosa: $responseBody")
                    // Puedes hacer algo con la respuesta JSON, como actualizar la UI o manejarla de otra manera
                } else {
                    Log.d("ApiServiceSQL", "Respuesta no exitosa: ${response.code}")
                }
            }
        } catch (e: Exception) {
            Log.e("ApiServiceSQL", "Error al realizar la solicitud: ${e.message}", e)
        }

        return "songUid"
    }


}
