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
    private val apiServiceSongMongo = ApiServiceSongMongoDB(this)

    @SuppressLint("MissingInflatedId")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityUploadBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Configuración del botón "Seleccionar archivo de audio"
        val selectAudioButton: Button = findViewById(R.id.buttonSelectAudio)
        selectAudioButton.setOnClickListener {
            openAudioSelector()
        }

        // Configuración del botón "Enviar"
        val submitButton: Button = findViewById(R.id.buttonSubmit)
        submitButton.setOnClickListener {
            if (audioFile == null) {
                Toast.makeText(this, "Selecciona un archivo de audio", Toast.LENGTH_SHORT).show()
                } else if (binding.editTextSongName.text.isEmpty()) {
                Toast.makeText(this, "Ingresa el nombre de la canción", Toast.LENGTH_SHORT).show()
            } else if (binding.editTextGenre.text.isEmpty()) {
                Toast.makeText(this, "Ingresa el género de la canción", Toast.LENGTH_SHORT).show()
            }

            uploadSong()

        }

        // Configuración del botón de retroceso
        findViewById<ImageView>(R.id.arrowback).setOnClickListener {
            finish()
        }
    }

    private fun openAudioSelector() {
        val intent = Intent(Intent.ACTION_OPEN_DOCUMENT)
        intent.addCategory(Intent.CATEGORY_OPENABLE)
        intent.type = "audio/*"
        startActivityForResult(intent, YOUR_REQUEST_CODE)
    }

    private fun uploadSong() {
        val songName = findViewById<EditText>(R.id.editTextSongName).text.toString()
        val genre = findViewById<EditText>(R.id.editTextGenre).text.toString()

        CoroutineScope(Dispatchers.IO).launch {
            try {
                audioFile?.let { file ->
                    val extensio = file.name.split(".").last()
                    Log.d("UploadActivity", "Extensión del archivo: $extensio")
                    val songUid = apiServiceSongSQL.postSongSQL(songName, genre, extensio)
                    if (songUid != "null") {
                        apiServiceSongMongo.postSongAudio(songUid, file)
                    } else {
                       Log.e("UploadActivity", "Error: songUid nulo.")
                    }

                    // val songUid = "songUid123"
                    // apiServiceSongMongo.postSongAudio(songUid, file)

                    runOnUiThread {
                        Log.d("UploadActivity", "Canción creada: $songName")
                        Toast.makeText(this@UploadActivity, "Canción creada", Toast.LENGTH_SHORT).show()
                    }
                } ?: run {
                    Log.e("UploadActivity", "Error: Archivo de audio nulo.")
                }
            } catch (e: Exception) {
                Log.e("UploadActivity", "Error al subir la canción: ${e.message}", e)
            }
        }
    }

    // Método para manejar el resultado del selector de documentos
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode == YOUR_REQUEST_CODE && resultCode == Activity.RESULT_OK) {
            data?.data?.let { uri ->
                audioFile = getFileFromUri(uri)
            }
        }
    }

    // Función para obtener un File a partir de una URI
    private fun getFileFromUri(uri: Uri): File? {
        try {
            contentResolver.openInputStream(uri)?.use { inputStream ->
                val fileName = getFileName(uri)
                val file = File(filesDir, fileName)

                file.outputStream().use { outputStream ->
                    inputStream.copyTo(outputStream)
                }

                Log.d("UploadActivity", "Archivo seleccionado: ${file.name}")
                return file
            }
        } catch (e: Exception) {
            Log.e("UploadActivity", "Error al obtener el archivo: ${e.message}", e)
        }
        return null
    }

    @SuppressLint("Range")
    private fun getFileName(uri: Uri): String {
        val cursor = contentResolver.query(uri, null, null, null, null)
        cursor?.use {
            if (it.moveToFirst()) {
                val displayName = it.getString(it.getColumnIndex(OpenableColumns.DISPLAY_NAME))
                Log.d("UploadActivity", "Nombre del archivo: $displayName")
                return displayName ?: "unknown_file"
            }
        }
        // retorna un nombre de archivo por defecto
        return "file_${System.currentTimeMillis()}"
    }
}
