package com.yossefjm.musify

import android.annotation.SuppressLint
import android.os.Bundle
import android.util.Log
import android.widget.ImageView
import android.widget.SearchView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.gson.Gson
import com.yossefjm.musify.adapters.DownloadPlaylistAdapter
import com.yossefjm.musify.adapters.DownloadSonglistAdapter
import com.yossefjm.musify.data.PlaylistRepository
import com.yossefjm.musify.data.SongRepository
import com.yossefjm.musify.databinding.ActivityDownloadBinding
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song
import okhttp3.Call
import okhttp3.Callback
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.Response
import java.io.IOException

class DownloadActivity() : AppCompatActivity() {

    // binding
    private lateinit var binding: ActivityDownloadBinding

    // repository
    private lateinit var playlistRepository: PlaylistRepository
    private lateinit var songRepository: SongRepository

    private lateinit var searchView: SearchView
    private lateinit var rvDownload: RecyclerView

    private lateinit var DLplaylistAdapter: DownloadPlaylistAdapter
    private lateinit var DLsonglistAdapter: DownloadSonglistAdapter

    // tema de bsuqueda
    private var searchType: String = "album"

    @SuppressLint("MissingInflatedId")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityDownloadBinding.inflate(layoutInflater)
        setContentView(binding.root)

        playlistRepository = PlaylistRepository(this.applicationContext)
        songRepository = SongRepository(this.applicationContext)
        playlistRepository.getAllPlaylists()

        searchView = binding.searchView
        rvDownload = binding.rvDownload

        DLplaylistAdapter = DownloadPlaylistAdapter(listOf(), this::DownloadPLOnClickListener)
        DLsonglistAdapter = DownloadSonglistAdapter(listOf(), this::DownloadSLOnClickListener)

        // Configuración del RecyclerView
        rvDownload.layoutManager = LinearLayoutManager(this)
        rvDownload.adapter = DLplaylistAdapter

        setsearchTypeListener()

        // Configuración del SearchView
        searchView.setOnQueryTextListener(object : SearchView.OnQueryTextListener {
            override fun onQueryTextSubmit(query: String?): Boolean {
                // Acciones cuando se envía la búsqueda (lógica de búsqueda aquí)
                // logica de peticion con otra clase, cuando esa clase devuelve un resultado, se actualiza el adapter

                if (query != null) {
                    Toast.makeText(this@DownloadActivity, "Buscando $query", Toast.LENGTH_SHORT).show()
                }

                if (searchType == "album") {
                    // peticion de album
                    val albums = searchAlbums(query!!)
                    DLplaylistAdapter.updateList(albums)

                } else if (searchType == "song") {
                    // peticion de cancion
                    val songs = searchSongs(query!!)
                    DLsonglistAdapter.updateList(songs)
                }

                val playlist = Playlist(12, "playlist1", "descripcion", "", mutableListOf<Song>())
                DLplaylistAdapter.updateList(listOf(playlist))

                return true
            }

            override fun onQueryTextChange(newText: String?): Boolean {
                // Acciones cuando el texto de búsqueda cambia (puedes realizar la lógica de búsqueda aquí)
                // Aquí podrías filtrar los resultados del adaptador según el nuevo texto de búsqueda
                return true
            }
        })

        findViewById<ImageView>(R.id.arrowback).setOnClickListener {
            // En algún lugar de DownloadActivity cuando desees cerrarla
            finish()
        }
    }

    private fun setsearchTypeListener() {
        binding.albumsnav.setBackgroundColor(resources.getColor(R.color.navyBlue))
        binding.albumsnav.setTextColor(resources.getColor(R.color.lightBlue))
        binding.songsnav.setBackgroundColor(resources.getColor(R.color.lightBlue))
        binding.songsnav.setTextColor(resources.getColor(R.color.navyBlue))

        binding.albumsnav.setOnClickListener {
            searchType = "album"
            rvDownload.adapter = DLplaylistAdapter
            binding.albumsnav.setBackgroundColor(resources.getColor(R.color.navyBlue))
            binding.albumsnav.setTextColor(resources.getColor(R.color.lightBlue))
            binding.songsnav.setBackgroundColor(resources.getColor(R.color.lightBlue))
            binding.songsnav.setTextColor(resources.getColor(R.color.navyBlue))
            binding.albumsnav.textScaleX = 1.2f
            binding.songsnav.textScaleX = 1f
            Toast.makeText(this, "Buscando albumes", Toast.LENGTH_SHORT).show()
        }
        binding.songsnav.setOnClickListener {
            searchType = "song"
            rvDownload.adapter = DLsonglistAdapter
            binding.albumsnav.setBackgroundColor(resources.getColor(R.color.lightBlue))
            binding.albumsnav.setTextColor(resources.getColor(R.color.navyBlue))
            binding.songsnav.setBackgroundColor(resources.getColor(R.color.navyBlue))
            binding.songsnav.setTextColor(resources.getColor(R.color.lightBlue))
            binding.albumsnav.textScaleX = 1f
            binding.songsnav.textScaleX = 1.2f
            Toast.makeText(this, "Buscando canciones", Toast.LENGTH_SHORT).show()
        }
    }


    private fun searchAlbums(query: String) : List<Playlist> {
        var responseBodyJson = ""
        val client = OkHttpClient()

        val request = Request.Builder()
            .url("https://spotify23.p.rapidapi.com/search/?q=${query}&type=albums&offset=0&limit=5&numberOfTopResults=5")
            .get()
            .addHeader("X-RapidAPI-Key", "b41889c768mshcb21a8b91f7ede0p194fe6jsnd5b30d47f3a3")
            .addHeader("X-RapidAPI-Host", "spotify23.p.rapidapi.com")
            .build()

        client.newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                // Manejar la falla de la solicitud
                e.printStackTrace()
            }

            override fun onResponse(call: Call, response: Response) {
                // Manejar la respuesta exitosa
                if (response.isSuccessful) {
                    val responseBody = response.body?.string()
                    // Puedes trabajar con el cuerpo de la respuesta aquí
                    if (responseBody != null) {
                        Log.d("response", responseBody)
                        responseBodyJson = responseBody
                    }
                } else {
                    // Manejar respuesta no exitosa
                    Log.d("response", "no exitosa")
                }
            }
        })

        return fromJsonToPlaylist(responseBodyJson)
    }

    fun fromJsonToPlaylist(json: String): List<Playlist> {
        // aqui se deberia parsear el json y devolver una lista de playlist


        return listOf(Playlist(12, "playlist1", "descripcion", "", mutableListOf<Song>()))
    }

    private fun searchSongs(query: String): List<Song> {
        var responseBodyJson = ""
        val client = OkHttpClient()

        val request = Request.Builder()
            .url("https://spotify23.p.rapidapi.com/search/?q=${query}&type=tracks&offset=0&limit=5&numberOfTopResults=5")
            .get()
            .addHeader("X-RapidAPI-Key", "b41889c768mshcb21a8b91f7ede0p194fe6jsnd5b30d47f3a3")
            .addHeader("X-RapidAPI-Host", "spotify23.p.rapidapi.com")
            .build()

        client.newCall(request).enqueue(object : Callback {
            override fun onFailure(call: Call, e: IOException) {
                // Manejar la falla de la solicitud
                e.printStackTrace()
            }

            override fun onResponse(call: Call, response: Response) {
                // Manejar la respuesta exitosa
                if (response.isSuccessful) {
                    val responseBody = response.body?.string()
                    // Puedes trabajar con el cuerpo de la respuesta aquí
                    if (responseBody != null) {
                        Log.d("response", responseBody)
                        responseBodyJson = responseBody
                    }
                } else {
                    // Manejar respuesta no exitosa
                    Log.d("response", "no exitosa")
                }
            }
        })

        return fromJsonToSongs(responseBodyJson)
    }

    fun fromJsonToSongs(json: String): List<Song> {
        // aqui se deberia parsear el json y devolver una lista de playlist


        return listOf(Song(12, "song1", "descripcion", "", "", false))
    }

    private fun DownloadPLOnClickListener(playlist: Playlist) {
        Toast.makeText(this, "Descargando playlist ${playlist.name}", Toast.LENGTH_SHORT).show()

        playlistRepository.addPlaylist(playlist)
    }

    private fun DownloadSLOnClickListener(song: Song) {
        Toast.makeText(this, "Descargando cancion ${song.title}", Toast.LENGTH_SHORT).show()
        // peticionDeAudio(song)

        // songRepository.addSong(song)
    }

    // Función de ejemplo para obtener datos
    private fun getData(): List<String> {
        return listOf("Resultado 1", "Resultado 2", "Resultado 3", "Resultado 4")
    }

}
