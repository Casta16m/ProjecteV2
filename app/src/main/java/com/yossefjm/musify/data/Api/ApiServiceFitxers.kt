package com.yossefjm.musify.data

import android.content.ContentValues
import android.content.Context
import android.net.Uri
import android.os.Environment
import android.provider.MediaStore
import android.util.Log
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
import retrofit2.http.GET
import retrofit2.http.Multipart
import retrofit2.http.POST
import retrofit2.http.Part
import retrofit2.http.Path
import java.io.File
import java.io.InputStream
import java.util.concurrent.TimeUnit

interface FitxersApiService {

    @Multipart
    @POST("FitxersAPI/v1/Song")
    fun postSongAudio(
        @Part("Uid") songUid: String,
        @Part Audio: MultipartBody.Part
    ): Call<ResponseBody>

    @GET("FitxersAPI/v1/Song/GetAudio/{UID}")
    fun getSongAudio(
        @Path("UID") songUid: String
    ): Call<ResponseBody>

    @Multipart
    @POST("MongoAPI/v1/Historial")
    fun postSongHistorial(
        @Part("uidSong") songUid: String,
        @Part("mac") mac: String,
        @Part("data") data: String
    ): Call<ResponseBody>

}

class ApiServiceFitxers(private val context: Context) {

    private val IP_ADDRESS = "172.23.3.204:5010"

    private val retrofit: Retrofit = Retrofit.Builder()
        .baseUrl("http://$IP_ADDRESS/")
        .addConverterFactory(GsonConverterFactory.create())
        .client(OkHttpClient.Builder().connectTimeout(30, TimeUnit.SECONDS).build())
        .build()

    private val mongoApiService: FitxersApiService = retrofit.create(FitxersApiService::class.java)

    // Método para enviar un archivo de audio a la API
    fun postSongAudio(songUid: String, audioFile: File) {
        try {
            val requestFile = audioFile.asRequestBody("audio/*".toMediaTypeOrNull())
            val audioPart = MultipartBody.Part.createFormData("Audio", audioFile.name, requestFile)

            // Realiza la llamada a la API
            mongoApiService.postSongAudio(songUid, audioPart).enqueue(object : retrofit2.Callback<ResponseBody> {
                override fun onResponse(call: Call<ResponseBody>, response: retrofit2.Response<ResponseBody>) {
                    if (response.isSuccessful) {
                        Log.d("ApiServiceSongMongoDB", "Respuesta exitosa")
                        // Puedes hacer algo con la respuesta JSON, como actualizar la UI o manejarla de otra manera
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
            Log.e("ApiServiceSongMongoDB", "Error al realizar la solicitud: ${e.message}", e)
        }
    }

    /**
     * Método para descargar un archivo de audio de la API
     * @param songUid ID de la canción
     * @param songName Nombre de la canción
     * @return Ruta del archivo de audio
     */
    fun getSongAudio(songUid: String, songName: String) : String {
        var filePath = "undefined"
        val songUidFull = "\"$songUid\""
        Log.d("ApiServiceSongMongoDB", "songUidFull: $songUidFull")
        mongoApiService.getSongAudio(songUidFull).enqueue(object : Callback<ResponseBody> {

            override fun onResponse(call: Call<ResponseBody>, response: Response<ResponseBody>) {
                if (response.isSuccessful) {
                    val responseBody = response.body()
                    if (responseBody != null) {
                        Log.d("ApiServiceSongMongoDB", "Canción descargada exitosamente")
                        filePath = saveToFile(responseBody.byteStream(), songName)
                    } else {
                        Log.d("ApiServiceSongMongoDB", "Cuerpo de respuesta nulo")
                    }
                } else {
                    Log.d("ApiServiceSongMongoDB", "Respuesta no exitosa: ${response.code()}")
                }
            }

            override fun onFailure(call: Call<ResponseBody>, t: Throwable) {
                Log.e("ApiServiceSongMongoDB", "Error al realizar la solicitud: ${t.message}", t)
            }
        })
        return filePath
    }


    private fun saveToFile(inputStream: InputStream, audioName: String): String {
        try {
            // Crear un nuevo archivo en el directorio de música usando MediaStore
            val values = ContentValues().apply {
                put(MediaStore.MediaColumns.DISPLAY_NAME, "$audioName.mp3")
                put(MediaStore.MediaColumns.MIME_TYPE, "audio/mp3")
                put(MediaStore.MediaColumns.RELATIVE_PATH, Environment.DIRECTORY_MUSIC)
            }

            val contentResolver = context.contentResolver
            val audioUri: Uri? = contentResolver.insert(MediaStore.Audio.Media.EXTERNAL_CONTENT_URI, values)

            audioUri?.let {
                val outputStream = contentResolver.openOutputStream(audioUri)

                val buffer = ByteArray(1024)
                var bytesRead: Int

                while (inputStream.read(buffer).also { bytesRead = it } != -1) {
                    outputStream?.write(buffer, 0, bytesRead)
                }

                outputStream?.close()
                inputStream.close()

                Log.d("ApiServiceSongMongoDB", "Archivo guardado exitosamente: $audioUri")
                return audioUri.toString()
            } ?: run {
                Log.e("ApiServiceSongMongoDB", "Error al guardar el archivo")
            }
        } catch (e: Exception) {
            Log.e("ApiServiceSongMongoDB", "Error al guardar el archivo: ${e.message}", e)
        }
        return "undefined"
    }


}
