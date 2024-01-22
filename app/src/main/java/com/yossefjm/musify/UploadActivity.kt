package com.yossefjm.musify

import android.annotation.SuppressLint
import android.app.Activity
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.provider.OpenableColumns
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.ImageView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.yossefjm.musify.data.ApiServiceSongMongoDB
import com.yossefjm.musify.data.ApiServiceSongSQL
import com.yossefjm.musify.databinding.ActivityUploadBinding
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.io.File

class UploadActivity : AppCompatActivity() {

    // Binding
    private lateinit var binding: ActivityUploadBinding

    // Constante para el selector de documentos
    private var audioFile: File? = null
    private val YOUR_REQUEST_CODE = 123 // Puedes cambiar esto según tus necesidades

    // API Service
    private val apiServiceSongSQL = ApiServiceSongSQL()
    private val apiServiceSongMongo = ApiServiceSongMongoDB()

    @SuppressLint("MissingInflatedId")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityUploadBinding.inflate(layoutInflater)
        setContentView(binding.root)

        var songName = ""
        var genre = ""

        // Configuración del botón "Seleccionar archivo de audio"
        val selectAudioButton: Button = findViewById(R.id.buttonSelectAudio)
        selectAudioButton.setOnClickListener {
            // Abre el selector de documentos para permitir al usuario seleccionar un archivo de audio
            val intent = Intent(Intent.ACTION_OPEN_DOCUMENT)
            intent.addCategory(Intent.CATEGORY_OPENABLE)
            intent.type = "audio/*"
            startActivityForResult(intent, YOUR_REQUEST_CODE)
        }

        // Configuración del botón "Enviar"
        val submitButton: Button = findViewById(R.id.buttonSubmit)
        submitButton.setOnClickListener {
            // Lógica para enviar la información a la API o realizar otras acciones
            songName = findViewById<EditText>(R.id.editTextSongName).text.toString()
            genre = findViewById<EditText>(R.id.editTextGenre).text.toString()

            // val songUid = apiServiceSongSQL.postSong(songName, genre)
            val songUid = "1234"

            // Coroutines
            CoroutineScope(Dispatchers.IO).launch {
                if (audioFile != null) {
                    apiServiceSongMongo.postSongAudio(songUid, audioFile!!)
                }
            }

            Log.d("UploadActivity", "Canción creada: $songName")
            Toast.makeText(this, "Canción creada", Toast.LENGTH_SHORT).show()
        }

        // Configuración del botón de retroceso
        findViewById<ImageView>(R.id.arrowback).setOnClickListener {
            finish()
        }
    }









    // Método para manejar el resultado del selector de documentos
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode == YOUR_REQUEST_CODE && resultCode == Activity.RESULT_OK) {
            data?.data?.let { uri ->
                // Obtener el File correspondiente a la URI
                audioFile = getFileFromUri(uri)
            }
        }
    }

    // Función para obtener un File a partir de una URI
    @SuppressLint("Range")
    private fun getFileFromUri(uri: Uri): File? {
        try {
            val inputStream = contentResolver.openInputStream(uri)
            if (inputStream != null) {
                val file = File(cacheDir, "selected_audio_file.mp3")
                file.outputStream().use { outputStream ->
                    inputStream.copyTo(outputStream)
                }
                Log.d("UploadActivity", "Archivo seleccionado: ${file.name}")
                return file
            } else {
                Log.e("UploadActivity", "¡Error! InputStream es nulo al abrir la URI.")
            }
        } catch (e: Exception) {
            Log.e("UploadActivity", "¡Error! El archivo no se creó correctamente: ${e.message}", e)
        }
        return null
    }

}
