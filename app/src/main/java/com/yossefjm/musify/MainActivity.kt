package com.yossefjm.musify

import android.Manifest
import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.animation.AnimationUtils
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.google.android.material.snackbar.Snackbar
import com.yossefjm.musify.adapters.PlaylistAdapter
import com.yossefjm.musify.adapters.SongListAdapter
import com.yossefjm.musify.data.PlaylistRepository
import com.yossefjm.musify.data.SongRepository
import com.yossefjm.musify.databinding.ActivityMainBinding
import com.yossefjm.musify.extension.PermissionExtension
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song


class MainActivity : AppCompatActivity() {


    // binding
    private lateinit var binding: ActivityMainBinding

    // permision extension
    private val permissionExtension = PermissionExtension(this)

    // permision required
    private val READ_AUDIO_STORAGE = Manifest.permission.READ_MEDIA_AUDIO

    // Adapter
    private lateinit var songAdapter: SongListAdapter
    private lateinit var playlistAdapter: PlaylistAdapter

    // Animations
    private lateinit var slideFadeOutAnimation: android.view.animation.Animation
    private lateinit var slideFadeInAnimation: android.view.animation.Animation
    private lateinit var slideUpAnimation: android.view.animation.Animation
    private lateinit var slideDownAnimation: android.view.animation.Animation



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Data access objects
        val songRepository = SongRepository(this)
        val playListRepository = PlaylistRepository(this)

        // inicializar las animaciones
        initAnimations()

        // Comprovamos que tenemos permisos
        if (permissionExtension.checkAndRequestPermissions(READ_AUDIO_STORAGE)) {
            // Permission is granted
            Snackbar.make(binding.root, "Permission is granted", Snackbar.LENGTH_LONG).show()
            configAdaptersRW()

            // configPlayerListeners()
        } else {
            // Permission is not granted
            Snackbar.make(binding.root, "Permission is not granted, request it", Snackbar.LENGTH_INDEFINITE).show()
        }



        val playlists = playListRepository.getAllPlaylists()
        Log.d("MainActivity", "playLists: $playlists")
        playlistAdapter.updateList(playlists)
    }

    private fun initAnimations() {
        slideFadeOutAnimation = AnimationUtils.loadAnimation(this, R.anim.fade_out)
        slideFadeInAnimation = AnimationUtils.loadAnimation(this, R.anim.fade_in)
        slideUpAnimation = AnimationUtils.loadAnimation(this, R.anim.slide_up)
        slideDownAnimation = AnimationUtils.loadAnimation(this, R.anim.slide_down)
    }


    // Configuración de los listeners del reproductor
    private fun configAdaptersRW() {
        // Adapters
        songAdapter = SongListAdapter(emptyList(), ::handleSongClick, ::handleLikeSongClick)
        playlistAdapter = PlaylistAdapter(emptyList(), ::handleOnPlaylistClick, ::handleEditPlaylistClick)


        // Configuración del RecyclerView para mostrar canciones
        binding.rvSongList.layoutManager = LinearLayoutManager(this)
        binding.rvSongList.adapter = songAdapter

        // Configuración del RecyclerView para mostrar listas de reproducción
        binding.rvPlayLists.layoutManager = LinearLayoutManager(this)
        binding.rvPlayLists.adapter = playlistAdapter
    }










    private fun handleOnPlaylistClick(playlist: Playlist) {
        // showSongsList(playlist)
    }

    private fun handleEditPlaylistClick(playlist: Playlist) {
        // showEditPlaylistDialog(playlist)
    }

    private fun handleSongClick(song: Song, songList: List<Song>, position: Int) {
        // songViewModel.clickedSong(songList, position)
    }

    private fun handleLikeSongClick(song: Song, songList: List<Song>, position: Int) {
        // Toast.makeText(this, "Like song ${song.title}", Toast.LENGTH_SHORT).show()
        // playlistViewModel.likedSong(song, songList, position)
    }
}
