package com.yossefjm.musify.utils

import android.media.MediaPlayer
import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import com.yossefjm.musify.model.Song
import java.util.Timer
import java.util.TimerTask

class MediaPlayerHelper {

    private var mediaPlayer: MediaPlayer? = null
    var isPaused: Boolean = false

    private var currentPlaylist: List<Song>? = null
    private var currentIndex: Int = 0
    private val _currentSongLiveData = MutableLiveData<Song?>()
    val currentSongLiveData: LiveData<Song?> get() = _currentSongLiveData


    fun playPlaylist(playlist: List<Song>, startIndex: Int = 0) {
        if (mediaPlayer == null) {
            mediaPlayer = MediaPlayer()
        }

        currentPlaylist = playlist
        currentIndex = startIndex
        playCurrentSong()
    }

    private fun playCurrentSong() {
        // este if es para evitar que se reproduzca una canción que no existe
        if (currentIndex in 0 until (currentPlaylist?.size ?: 0)) {
            val currentSong = currentPlaylist?.get(currentIndex)
            try {
                mediaPlayer?.apply {
                    reset()

                    if (currentSong != null) {
                        setDataSource(currentSong.songPath)
                    }
                    prepareAsync()

                    setOnPreparedListener {
                        // Cuando la preparación está completa, comenzar la reproducción
                        start()
                        // Notificar al ViewModel que la canción ha cambiado
                        _currentSongLiveData.postValue(currentSong)
                    }

                    setOnCompletionListener {
                        if (currentPosition / 1000 == duration / 1000) {
                            playNextSong()
                        }
                    }
                }
            } catch (e: Exception) {
                // Manejar la excepción, por ejemplo, mostrar un mensaje de error
                if (currentSong != null) {
                    Log.d("MediaPlayerError", "Error al reproducir la canción: ${currentSong.songPath}")
                }
                e.printStackTrace()
            }
        }
    }


    fun pausePlayback() {
        mediaPlayer?.apply {
            if (isPlaying) {
                pause()
                isPaused = true
            }
        }
    }

    fun resumePlayback() {
        mediaPlayer?.apply {
            if (isPaused) {
                start()
                isPaused = false
            }
        }
    }

    fun stopPlayback() {
        mediaPlayer?.apply {
            if (isPlaying || isPaused) {
                stop()
                reset()
                currentPlaylist = null
                currentIndex = 0
                isPaused = false
            }
        }
    }

    fun releaseMediaPlayer() {
        mediaPlayer?.release()
        mediaPlayer = null
        currentPlaylist = null
        currentIndex = 0
        isPaused = false
    }

    fun playPrevSong() {
        if (currentIndex > 0) {
            currentIndex--
            playCurrentSong()
        } else {
            currentIndex = currentPlaylist?.size?.minus(1) ?: 0
            playCurrentSong()
        }
    }

    fun playNextSong() {
        if (currentIndex < (currentPlaylist?.size?.minus(1) ?: 0)) {
            currentIndex++
            //playCurrentSong()
            // Notificar al ViewModel que la canción ha cambiado
            // viewModel.notifySongChanged(currentPlaylist?.get(currentIndex))
        } else {
            // La lista de reproducción ha llegado al final
            currentIndex = 0
            //playCurrentSong()
        }
    }

    fun getMediaPlayer(): MediaPlayer? {
        return mediaPlayer
    }

    fun getCurrentSong(): Song {
        return currentPlaylist?.get(currentIndex) ?: Song("0", "", "", "", "", false)
    }

    fun playSong(songList: List<Song>, position: Int) {
        if (mediaPlayer == null) {
            mediaPlayer = MediaPlayer()
        }

        currentPlaylist = songList
        currentIndex = position
        Log.d("MediaPlayer SongClick", "Song getCurrentSong playsong ${currentIndex}")
        playCurrentSong()
    }

}

