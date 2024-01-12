package com.yossefjm.musify

import android.Manifest
import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Handler
import android.util.Log
import android.view.View
import android.view.animation.AnimationUtils
import android.widget.Button
import android.widget.EditText
import android.widget.LinearLayout
import android.widget.RelativeLayout
import android.widget.SeekBar
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.widget.AppCompatSeekBar
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.snackbar.Snackbar
import com.yossefjm.musify.adapters.PlaylistAdapter
import com.yossefjm.musify.adapters.SongListAdapter
import com.yossefjm.musify.data.PlaylistRepository
import com.yossefjm.musify.data.SongRepository
import com.yossefjm.musify.databinding.ActivityMainBinding
import com.yossefjm.musify.extension.PermissionExtension
import com.yossefjm.musify.extension.toHourMinSecString
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song
import com.yossefjm.musify.utils.MediaPlayerHelper


class MainActivity : AppCompatActivity() {

    // Media player
    private val mediaPlayerH = MediaPlayerHelper()

    // binding
    private lateinit var binding: ActivityMainBinding

    // permision extension
    private val permissionExtension = PermissionExtension(this)

    // permision required
    private val READ_AUDIO_STORAGE = Manifest.permission.READ_MEDIA_AUDIO
    // private val READ_FILE_STORAGE = Manifest.permission.READ_EXTERNAL_STORAGE

    // Adapter
    private lateinit var songAdapter: SongListAdapter
    private lateinit var playlistAdapter: PlaylistAdapter

    // repositories
    private lateinit var songRepository: SongRepository
    private lateinit var playListRepository: PlaylistRepository

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
        songRepository = SongRepository(this)
        playListRepository = PlaylistRepository(this)

        // inicializar las animaciones
        initAnimations()

        // Comprovamos que tenemos permisos
        if (permissionExtension.checkAndRequestPermissions(READ_AUDIO_STORAGE)) {
            // Permission is granted
            Snackbar.make(binding.root, "Permission is granted", Snackbar.LENGTH_LONG).show()
            configAdaptersRW()

            configPlayerListeners()

            val playlists = playListRepository.getAllPlaylists()
            Log.d("MainActivity", "playLists: $playlists")
            playlistAdapter.updateList(playlists)
        } else {
            // Permission is not granted
            Snackbar.make(binding.root, "Permission is not granted, request it", Snackbar.LENGTH_INDEFINITE).show()
        }




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

    private fun configPlayerListeners() {
        binding.compactPlayer.root.setOnClickListener { showHideExpandedView() }
        binding.expandedsong.arrowdown.setOnClickListener { showHideExpandedView() }
        binding.arrowback.setOnClickListener { gobackToPlaylists() }
        binding.createplaylist.setOnClickListener { showCreatePlaylistDialog() }

        val playPauseClickListener = View.OnClickListener {
            if (mediaPlayerH.isPaused) {
                mediaPlayerH.resumePlayback()
                binding.compactPlayer.playPauseButtonMinimized.setImageResource(R.drawable.pause)
                binding.expandedsong.playPauseButton.setImageResource(R.drawable.pause)
            } else {
                mediaPlayerH.pausePlayback()
                binding.compactPlayer.playPauseButtonMinimized.setImageResource(R.drawable.playwhite)
                binding.expandedsong.playPauseButton.setImageResource(R.drawable.playwhite)
            }
        }
        binding.compactPlayer.playPauseButtonMinimized.setOnClickListener(playPauseClickListener)
        binding.expandedsong.playPauseButton.setOnClickListener(playPauseClickListener)

        binding.compactPlayer.nextButtonMinimized.setOnClickListener { mediaPlayerH.playNextSong(); updateSongView(mediaPlayerH.getCurrentSong()) }
        binding.expandedsong.nextButton.setOnClickListener { mediaPlayerH.playNextSong(); updateSongView(mediaPlayerH.getCurrentSong()) }

        binding.compactPlayer.previousButtonMinimized.setOnClickListener { mediaPlayerH.playPrevSong(); updateSongView(mediaPlayerH.getCurrentSong()) }
        binding.expandedsong.previousButton.setOnClickListener { mediaPlayerH.playPrevSong(); updateSongView(mediaPlayerH.getCurrentSong()) }

        configProgressBar(binding.expandedsong.songProgressBar)
    }

    private fun updateSongView(song: Song) {
        binding.compactPlayer.playPauseButtonMinimized.setImageResource(R.drawable.pause)
        binding.expandedsong.playPauseButton.setImageResource(R.drawable.pause)
        binding.compactPlayer.songNameMinimized.text = song.title
        binding.compactPlayer.artistNameMinimized.text = song.artist
        binding.expandedsong.songTitle.text = song.title
        binding.expandedsong.artistName.text = song.artist
    }

    private fun showCreatePlaylistDialog() {
        val dialogBuilder = AlertDialog.Builder(this)
        val inflater = this.layoutInflater
        val dialogView = inflater.inflate(R.layout.dialog_config_playlist, null)

        val titleEditText = dialogView.findViewById<EditText>(R.id.editTextTitle)
        val descriptionEditText = dialogView.findViewById<EditText>(R.id.editTextDescription)
        dialogView.findViewById<Button>(R.id.btnAddSongs).visibility = Button.GONE
        dialogView.findViewById<Button>(R.id.btnDeletePlaylist).visibility = Button.GONE
        dialogView.findViewById<Button>(R.id.btnRemoveSongs).visibility = Button.GONE

        dialogBuilder.setView(dialogView)

        dialogBuilder.setTitle("Create Playlist")
        dialogBuilder.setPositiveButton("Create") { _, _ ->
            // Aquí obtienes los valores ingresados por el usuario
            val title = titleEditText.text.toString()
            val description = descriptionEditText.text.toString()

            // Lógica para crear la nueva lista de reproducción con los valores proporcionados
            playListRepository.addPlaylist(Playlist(playListRepository.getNewId(), title, description, "", mutableListOf()))
            playlistAdapter.updateList(playListRepository.playlistsJson.playlists)
        }

        dialogBuilder.setNegativeButton("Cancel") { _, _ ->
            // Implementa acciones adicionales o simplemente cierra el diálogo
        }

        val createPlaylistDialog = dialogBuilder.create()
        createPlaylistDialog.setCanceledOnTouchOutside(false)
        createPlaylistDialog.show()
    }

    private val handler = Handler()
    private val updateProgressRunnable = object : Runnable {
        override fun run() {
            updateProgressBar()
            handler.postDelayed(this, 100)  // Actualiza cada 100 milisegundos
        }
    }
    private fun configProgressBar(linearProgressBar: AppCompatSeekBar) {
        handler.post(updateProgressRunnable)

        linearProgressBar.setOnSeekBarChangeListener(object : SeekBar.OnSeekBarChangeListener {
            var duration = 0
            var selectedPercentage = 0
            var newPositionInms = 0

            override fun onProgressChanged(seekBar: SeekBar?, progress: Int, fromUser: Boolean) {
                // Aqui puedes hacer algo cuando el usuario cambia el progreso de la barra de búsqueda
                duration = mediaPlayerH.getMediaPlayer()?.duration ?: 1
                selectedPercentage = seekBar?.progress ?: 0
                newPositionInms = (selectedPercentage.toFloat() / 100 * duration.toFloat()).toInt()

                runOnUiThread(Runnable {
                    binding.expandedsong.songProgressTxt.text = newPositionInms.toLong().toHourMinSecString()
                })

            }

            override fun onStartTrackingTouch(seekBar: SeekBar?) {
                // Detén el temporizador cuando el usuario toque la barra de búsqueda
                handler.removeCallbacks(updateProgressRunnable)
            }

            override fun onStopTrackingTouch(seekBar: SeekBar?) {
                // Configura la nueva posición en el MediaPlayer
                mediaPlayerH.getMediaPlayer()?.seekTo(newPositionInms)


                // Reinicia el temporizador
                handler.post(updateProgressRunnable)
            }
        })
    }

    private fun updateProgressBar() {
        val duration = mediaPlayerH.getMediaPlayer()?.duration ?: 1
        val currentPosition = mediaPlayerH.getMediaPlayer()?.currentPosition ?: 0
        val progressPercentage = currentPosition.toFloat() / duration.toFloat() * 100

        runOnUiThread {
            binding.expandedsong.songDurationTxt.text = duration.toLong().toHourMinSecString()
            binding.expandedsong.songProgressBar.progress = progressPercentage.toInt()
            binding.expandedsong.songProgressTxt.text = currentPosition.toLong().toHourMinSecString()
            // linearProgressBar.progress = progressPercentage.toInt()
            binding.expandedsong.songProgressBar.progress = progressPercentage.toInt()
        }
    }

    private fun handleOnPlaylistClick(playlist: Playlist) {
        showSongsList(playlist)
    }

    private fun handleEditPlaylistClick(playlist: Playlist) {
        showEditPlaylistDialog(playlist)
    }

    private fun handleSongClick(song: Song, songList: List<Song>, position: Int) {
        mediaPlayerH.playSong(songList, position)
        binding.compactPlayer.playPauseButtonMinimized.setImageResource(R.drawable.pause)
        binding.expandedsong.playPauseButton.setImageResource(R.drawable.pause)
        binding.compactPlayer.songNameMinimized.text = song.title
        binding.compactPlayer.artistNameMinimized.text = song.artist
        binding.expandedsong.songTitle.text = song.title
        binding.expandedsong.artistName.text = song.artist
    }

    private fun handleLikeSongClick(song: Song, songList: List<Song>, position: Int) {
        playListRepository.editLikedSong(song)
        playlistAdapter.updateList(playListRepository.playlistsJson.playlists)
    }

    private fun showHideExpandedView() {
        val expandedPlayerLayout = findViewById<RelativeLayout>(R.id.expandedsong)
        val songListScreen = findViewById<RecyclerView>(R.id.rvSongList)
        val playListScreen = findViewById<RecyclerView>(R.id.rvPlayLists)

        if (expandedPlayerLayout.visibility == View.VISIBLE) {
            Log.d("MainActivity", "showHideExpandedView: visible")
            expandedPlayerLayout.visibility = View.GONE
            expandedPlayerLayout.startAnimation(slideDownAnimation)

            songListScreen.startAnimation(slideFadeOutAnimation)
            songListScreen.visibility = View.GONE
            playListScreen.startAnimation(slideFadeOutAnimation)
            playListScreen.visibility = View.VISIBLE

        } else {
            Log.d("MainActivity", "showHideExpandedView: gone")
            expandedPlayerLayout.startAnimation(slideUpAnimation)
            expandedPlayerLayout.visibility = View.VISIBLE
            songListScreen.startAnimation(slideFadeOutAnimation)
            songListScreen.visibility = View.GONE
            playListScreen.startAnimation(slideFadeOutAnimation)
            playListScreen.visibility = View.GONE
        }
    }

    private fun showEditPlaylistDialog(playlist: Playlist){
        var alertDialog: AlertDialog
        alertDialog = AlertDialog.Builder(this).create()
        val dialogBuilder = AlertDialog.Builder(this)
        val inflater = layoutInflater
        val dialogView = inflater.inflate(R.layout.dialog_config_playlist, null)
        dialogBuilder.setView(dialogView)

        val editTextTitle = dialogView.findViewById<EditText>(R.id.editTextTitle)
        val editTextDescription = dialogView.findViewById<EditText>(R.id.editTextDescription)
        val btnAddSongs = dialogView.findViewById<Button>(R.id.btnAddSongs)
        val btnRemoveSongs = dialogView.findViewById<Button>(R.id.btnRemoveSongs)
        val btnDeletePlaylist = dialogView.findViewById<Button>(R.id.btnDeletePlaylist)

        editTextTitle.setText(playlist.name)
        editTextDescription.setText(playlist.description)

        btnAddSongs.setOnClickListener {
            // Aquí puedes implementar la lógica para agregar canciones a la lista de reproducción
            showAddSongsDialog(playlist)
            alertDialog.dismiss()

        }

        btnRemoveSongs.setOnClickListener {
            // Aquí puedes implementar la lógica para quitar canciones de la lista de reproducción
            showRemoveSongsDialog(playlist)
            alertDialog.dismiss()

        }

        btnDeletePlaylist.setOnClickListener {
            // Aquí puedes implementar la lógica para eliminar la lista de reproducción
            deletePlaylist(playlist)
            alertDialog.dismiss()

        }

        dialogBuilder.setOnDismissListener { dialog ->
            // Aquí puedes implementar la lógica para actualizar la lista de reproducción
            playlist.name = editTextTitle.text.toString()
            playlist.description = editTextDescription.text.toString()
        }

        alertDialog = dialogBuilder.create()
        alertDialog.show()
    }

    private fun showAddSongsDialog(playlist: Playlist) {
        val allSongs = songRepository.getAllSongs()
        val selectedSongs = mutableListOf<Song>()

        // Convierte la lista de canciones en un array de CharSequence para usarlo en el cuadro de diálogo
        val songNames = allSongs.map { it.title }.toTypedArray()
        val checkedItems = BooleanArray(allSongs.size) { false }

        val builder = AlertDialog.Builder(this)
        builder.setTitle("Agregar canciones")
            .setMultiChoiceItems(songNames, checkedItems) { _, which, isChecked ->
                if (isChecked) {
                    selectedSongs.add(allSongs[which])
                } else {
                    selectedSongs.remove(allSongs[which])
                }
            }
            .setPositiveButton("Agregar") { _, _ ->
                playlist.songs.addAll(selectedSongs)
                // Esto agrega la lista de reproducción a la base de datos
                playListRepository.updatePlaylist(playlist)
                playlistAdapter.updateList(playListRepository.playlistsJson.playlists)

            }
            .setNegativeButton("Cancelar") { dialog, _ ->
                dialog.dismiss()
            }

        builder.create().show()
    }

    private fun showRemoveSongsDialog(playlist: Playlist) {
        val selectedSongs = mutableListOf<Song>()

        // Convierte la lista de canciones en un array de CharSequence para usarlo en el cuadro de diálogo
        val songNames = playlist.songs.map { it.title }.toTypedArray()
        val checkedItems = BooleanArray(playlist.songs.size) { false }

        val builder = AlertDialog.Builder(this)
        builder.setTitle("Quitar canciones")
            .setMultiChoiceItems(songNames, checkedItems) { _, which, isChecked ->
                if (isChecked) {
                    selectedSongs.add(playlist.songs[which])
                } else {
                    selectedSongs.remove(playlist.songs[which])
                }
            }
            .setPositiveButton("Quitar") { _, _ ->
                playlist.songs.removeAll(selectedSongs)
                // Esto agrega la lista de reproducción a la base de datos
                playListRepository.updatePlaylist(playlist)
                playlistAdapter.updateList(playListRepository.playlistsJson.playlists)
            }
            .setNegativeButton("Cancelar") { dialog, _ ->
                dialog.dismiss()
            }

        builder.create().show()
    }

    private fun deletePlaylist(playlist: Playlist) {
        playListRepository.deletePlaylist(playlist.id)
        playlistAdapter.updateList(playListRepository.playlistsJson.playlists)
    }

    private fun showSongsList(playlist: Playlist){
        binding.rvPlayLists.startAnimation(slideFadeOutAnimation)
        binding.rvPlayLists.visibility = View.GONE

        binding.rvSongList.startAnimation(slideFadeInAnimation)
        binding.rvSongList.visibility = View.VISIBLE

        binding.title.text = playlist.name
        binding.createplaylist.visibility = View.GONE

        songAdapter.updateList(playlist.songs)
    }

    private fun gobackToPlaylists(){
        binding.rvPlayLists.startAnimation(slideFadeInAnimation)
        binding.rvPlayLists.visibility = View.VISIBLE

        binding.rvSongList.startAnimation(slideFadeOutAnimation)
        binding.rvSongList.visibility = View.GONE

        binding.title.text = "Playlists"
        binding.createplaylist.visibility = View.VISIBLE
    }

}
