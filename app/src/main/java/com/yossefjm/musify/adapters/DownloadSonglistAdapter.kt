package com.yossefjm.musify.adapters

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.yossefjm.musify.DownloadActivity
import com.yossefjm.musify.R
import com.yossefjm.musify.holders.DownloadSonglistHolder
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song
import kotlin.reflect.KFunction1

/**
 * Adaptador para la lista de canciones que utiliza un ViewHolder para presentar los elementos
 * de la lista, en este caso, objetos [Playlist]
 * Adapta la clase [Playlist] a la vista [R.layout.playlistitem]
 */
class DownloadSonglistAdapter(
    private var songList: List<Song>,
    private val DownloadSongListOnClickListener: KFunction1<Song, Unit>,
) :
    RecyclerView.Adapter<DownloadSonglistHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DownloadSonglistHolder {
        val inflater = LayoutInflater.from(parent.context)
        val itemView = inflater.inflate(R.layout.songitem, parent, false)
        return DownloadSonglistHolder(itemView)
    }

    override fun onBindViewHolder(holder: DownloadSonglistHolder, position: Int) {
        val currentSonglist = songList[position]
        holder.bind(currentSonglist, DownloadSongListOnClickListener)
    }

    override fun getItemCount(): Int {
        return songList.size
    }

    fun updateList(songlist: List<Song>) {
        songList = songlist
        notifyDataSetChanged()
    }
}