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
import com.yossefjm.musify.adapters.DownloadPlaylistAdapter
import com.yossefjm.musify.adapters.DownloadSonglistAdapter
import com.yossefjm.musify.data.ApiServiceSongSQL
import com.yossefjm.musify.data.ApiServiceAlbum
import com.yossefjm.musify.data.ApiServiceSongMongoDB
import com.yossefjm.musify.data.PlaylistRepository
import com.yossefjm.musify.data.SongRepository
import com.yossefjm.musify.databinding.ActivityDownloadBinding
import com.yossefjm.musify.extension.toExtendedIsoString
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import java.time.LocalDateTime
import java.time.ZoneId

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

    // api service
    private val apiServiceAlbum = ApiServiceAlbum()
    private val apiServiceSongSQL = ApiServiceSongSQL()
    private val apiServiceSongMongo = ApiServiceSongMongoDB(this)


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

                if (query != null) {
                    Toast.makeText(this@DownloadActivity, "Buscando $query", Toast.LENGTH_SHORT).show()
                }

                if (searchType == "album") {
                    // peticion de album
                    CoroutineScope(Dispatchers.IO).launch {
                        val albums = apiServiceAlbum.searchAlbums(query ?: "")
                        Log.d("albums", albums.toString())
                        withContext(Dispatchers.Main) {
                            DLplaylistAdapter.updateList(albums)
                        }
                    }

                } else if (searchType == "song") {
                    // peticion de cancion
                    CoroutineScope(Dispatchers.IO).launch {
                        val songs = apiServiceSongSQL.requestSongsByName(query ?: "")
                        Log.d("songs", songs.toString())
                        withContext(Dispatchers.Main) {
                            DLsonglistAdapter.updateList(songs)
                        }
                    }
                }

                val currentDateTime = LocalDateTime.now(ZoneId.systemDefault()).toExtendedIsoString()

                val playlist = Playlist(currentDateTime, "playlist1", "descripcion", "", mutableListOf<Song>())
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


    private fun DownloadPLOnClickListener(playlist: Playlist) {
        Toast.makeText(this, "Descargando playlist ${playlist.name}", Toast.LENGTH_SHORT).show()

        playlistRepository.addPlaylist(playlist)
    }

    private fun DownloadSLOnClickListener(song: Song) {
        Toast.makeText(this, "Descargando cancion ${song.title}", Toast.LENGTH_SHORT).show()

        val songPath = apiServiceSongMongo.getSongAudio(song.uid, song.title)
        song.songPath = songPath

        playlistRepository.addSongToAllSongs(song)
    }

    // Función de ejemplo para obtener datos
    private fun getData(): List<String> {
        return listOf("Resultado 1", "Resultado 2", "Resultado 3", "Resultado 4")
    }

}
