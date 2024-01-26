package com.yossefjm.musify.data

import android.content.ContentValues
import android.content.Context
import android.net.Uri
import android.os.Environment
import android.provider.MediaStore
import android.util.Log
import com.google.gson.annotations.SerializedName
import com.yossefjm.musify.model.Song
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import okhttp3.MultipartBody
import okhttp3.OkHttpClient
import okhttp3.RequestBody.Companion.asRequestBody
import okhttp3.ResponseBody
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Multipart
import retrofit2.http.POST
import retrofit2.http.Part
import retrofit2.http.Path
import java.io.File
import java.io.InputStream
import java.util.concurrent.TimeUnit

data class songToHistorial(
    @SerializedName("uidSong")
    val uidSong: String,

    @SerializedName("mac")
    val mac: String,

    @SerializedName("data")
    val data: String
)


interface HistorialApiService {


    @POST("MongoApi/v1/Historial")
    fun postSongHistorial(@Body songToHistorial: songToHistorial ): Call<ResponseBody>

}

class ApiServiceHistorial(private val context: Context) {

    private val IP_ADDRESS = "172.23.3.204:5001"

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://$IP_ADDRESS/")
        .addConverterFactory(GsonConverterFactory.create())
        .client(OkHttpClient.Builder().connectTimeout(30, TimeUnit.SECONDS).build())
        .build()

    private val ApiServiceHistorial: HistorialApiService = retrofit.create(HistorialApiService::class.java)

    var lastSong = ""

    /**
     * Método para hacer un post en el historial
     * @param songUid ID de la canción
     * @param mac Nombre de la mac
     * @param data Nombre de la data
     */
    fun postSongHistorial(songUid: String, mac: String, data: String) {
        val songToHistorial = songToHistorial(songUid, mac, data)
        if (lastSong == songUid) {
            Log.d("ApiServiceSongMongoDB", "No se ha cambiado de canción")
            return
        }

        try {
            ApiServiceHistorial.postSongHistorial(songToHistorial).enqueue(object : retrofit2.Callback<ResponseBody> {
                override fun onResponse(call: Call<ResponseBody>, response: retrofit2.Response<ResponseBody>) {
                    if (response.isSuccessful) {
                        Log.d("ApiServiceSongMongoDB", "Respuesta exitosa")
                        // Puedes hacer algo con la respuesta JSON, como actualizar la UI o manejarla de otra manera
                        lastSong = songUid;
                    } else {
                        Log.d("ApiServiceSongMongoDB", "Respuesta no exitosa: ${response.code()}")
                        Log.d("ApiServiceSongMongoDB", "Respuesta no exitosa: ${response.message()}")
                        Log.d("ApiServiceSongMongoDB", "Respuesta no exitosa: ${response.body()}")
                    }
                }

                override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
                    Log.e("ApiServiceSongMongoDB", "Error al realizar la solicitud onFailure: ${t.message}", t)
                }
            })


        } catch (e: Exception) {
            Log.e("ApiServiceSongMongoDB", "Error al realizar la solicitud del Historial : ${e.message}", e)
        }
    }

}
