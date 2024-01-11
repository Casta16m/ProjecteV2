package com.yossefjm.musify.adapters

import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.yossefjm.musify.R
import com.yossefjm.musify.holders.SongViewHolder
import com.yossefjm.musify.model.Song
import kotlin.reflect.KFunction3


/**
 * Adaptador que contiene la lógica para mostrar una lista de canciones
 * @param songList lista de canciones a mostrar
 */
class SongListAdapter(
    private var songList: List<Song>,
    private val songClickListener: KFunction3<Song, List<Song>, Int, Unit>,
    private val likedSongClickListener: KFunction3<Song, List<Song>, Int, Unit>
) : RecyclerView.Adapter<SongViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): SongViewHolder {
        val inflater = LayoutInflater.from(parent.context)
        val itemView = inflater.inflate(R.layout.songitem, parent, false)
        return SongViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: SongViewHolder, position: Int) {
        val currentSong = songList[position]
        holder.bind(currentSong)

        holder.itemView.setOnClickListener {
            songClickListener(currentSong, songList, position)
        }

        holder.likedSong.setOnClickListener {
            currentSong.liked = !currentSong.liked
            holder.updateLike(currentSong)
            likedSongClickListener(currentSong, songList, position)
        }
    }


    override fun getItemCount(): Int {
        return songList.size
    }

    fun updateList(songs: List<Song>?) {
        if (songs != null) {
            songList = songs
            notifyDataSetChanged()
        }
    }


}