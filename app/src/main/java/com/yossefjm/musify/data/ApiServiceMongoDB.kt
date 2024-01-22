package com.yossefjm.musify.data

import android.util.Base64
import android.util.Log
import android.webkit.MimeTypeMap
import com.google.gson.Gson
import com.google.gson.annotations.SerializedName
import okhttp3.Call
import okhttp3.Callback
import okhttp3.OkHttpClient
import okhttp3.MediaType
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import okhttp3.MultipartBody
import okhttp3.Request
import okhttp3.RequestBody.Companion.asRequestBody
import okhttp3.RequestBody.Companion.toRequestBody
import okhttp3.Response
import org.json.JSONObject
import java.io.File
import java.io.FileOutputStream
import java.io.IOException
import java.util.concurrent.TimeUnit
import kotlin.math.log

data class SongDBMongo(
    @SerializedName("ObjectId")
    val oid: String,

    @SerializedName("uid")
    val uid: String,

    @SerializedName("audio")
    val audio: Byte
)

class ApiServiceSongMongoDB {

    val IP_ADRESS = "192.168.0.16:5010"

    val destinationPath = "/ruta/del/destino/audio.mp3"

    private val client = OkHttpClient().newBuilder()
        .connectTimeout(15, TimeUnit.SECONDS)
        .readTimeout(15, TimeUnit.SECONDS)
        .build()

    private val gson = Gson()


    // Método para descargar canciones por género
    fun makeRequestApiSongDownload(songUid: String): String {
        var responseBody = ""
        var statusCode = ""

        try {
            val request = Request.Builder()
                .url("http://${IP_ADRESS}/FitxersAPI/v1/Song/GetAudio/$songUid")
                .get()
                .build()
            Log.d("ApiServiceMongo", "Get para : $request")

            client.newCall(request).execute().use { response ->
                statusCode = response.code.toString()
                if (response.isSuccessful) {
                    responseBody = response.body?.string().toString()
                    Log.d("ApiServiceMongo", "Respuesta exitosa: $responseBody")


                } else {
                    Log.d("ApiServiceMongo", "Respuesta no exitosa: ${response.code}")
                }
            }
        } catch (e: Exception) {
            Log.e("ApiServiceMongo", "Error al realizar la solicitud: ${e.message}", e)
        }

        return statusCode
    }


    // Método para construir la solicitud HTTP
    private fun saveToFile(inputStream: java.io.InputStream) {
        val file = File(destinationPath)
        val outputStream = FileOutputStream(file)
        val buffer = ByteArray(4096)

        var bytesRead: Int
        while (inputStream.read(buffer).also { bytesRead = it } != -1) {
            outputStream.write(buffer, 0, bytesRead)
        }

        outputStream.flush()
        outputStream.close()
        inputStream.close()
    }


    // Método para subir un audio a la base de datos MongoDB en formato Byte
    fun postSongAudio(songUid: String, audioFile: File) {
        try {
            // Crear el objeto JSON para la solicitud
            val json = JSONObject()
            json.put("Uid", songUid)

            // Construir el cuerpo de la solicitud con MultipartBody
            val requestBody = MultipartBody.Builder()
                .setType(MultipartBody.FORM)
                .addFormDataPart("Uid", songUid)
                .addFormDataPart("Audio", audioFile.name, audioFile.asRequestBody(getMediaType(audioFile)))
                .build()

            // Construir la solicitud HTTP con el cuerpo MultipartBody
            val request = Request.Builder()
                .url("http://$IP_ADRESS/FitxersAPI/v1/Song")
                .post(requestBody)
                .build()

            // Realizar la solicitud y procesar la respuesta
            OkHttpClient().newCall(request).enqueue(object : Callback {
                override fun onFailure(call: Call, e: IOException) {
                    Log.e("ApiServiceSongMongoDB", "Error al realizar la solicitud: ${e.message}", e)
                }

                override fun onResponse(call: Call, response: Response) {
                    if (response.isSuccessful) {
                        val responseBody = response.body?.string()
                        Log.d("ApiServiceSongMongoDB", "Respuesta exitosa: $responseBody")
                        // Puedes hacer algo con la respuesta JSON, como actualizar la UI o manejarla de otra manera
                    } else {
                        Log.d("ApiServiceSongMongoDB", "Respuesta no exitosa: ${response.code}")
                    }
                }
            })
        } catch (e: Exception) {
            Log.e("ApiServiceSongMongoDB", "Error al realizar la solicitud: ${e.message}", e)
        }
    }

    // Método para convertir un archivo a un tipo de medio
    private fun getMediaType(file: File): MediaType? {
        val extension = file.extension.toLowerCase()
        return extension.toMediaTypeOrNull()
    }


    fun getSongAudio(): String {
        try {


            // Construir la solicitud HTTP con el cuerpo MultipartBody
            val request = Request.Builder()
                .url("http://${IP_ADRESS}/FitxersAPI/v1/Song")
                .get()
                .build()


            // Realizar la solicitud y procesar la respuesta
            client.newCall(request).execute().use { response ->
                if (response.isSuccessful) {
                    val responseBody = response.body?.string()
                    Log.d("ApiServiceSQL", "Respuesta exitosa: $responseBody")
                    return responseBody.toString()
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
