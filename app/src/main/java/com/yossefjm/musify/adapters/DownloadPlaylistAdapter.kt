package com.yossefjm.musify.adapters

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.yossefjm.musify.R
import com.yossefjm.musify.holders.DownloadPlaylistHolder
import com.yossefjm.musify.holders.PlayListViewHolder
import com.yossefjm.musify.model.Playlist

/**
 * Adaptador para la lista de canciones que utiliza un ViewHolder para presentar los elementos
 * de la lista, en este caso, objetos [Playlist]
 * Adapta la clase [Playlist] a la vista [R.layout.playlistitem]
 */
class DownloadPlaylistAdapter(
    private var playlistList: List<Playlist>,
    private val DownloadPLOnClickListener: (Playlist) -> Unit,
) :
    RecyclerView.Adapter<DownloadPlaylistHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DownloadPlaylistHolder {
        val inflater = LayoutInflater.from(parent.context)
        val itemView = inflater.inflate(R.layout.playlistitem, parent, false)
        return DownloadPlaylistHolder(itemView)
    }

    override fun onBindViewHolder(holder: DownloadPlaylistHolder, position: Int) {
        val currentPlaylist = playlistList[position]
        holder.bind(currentPlaylist, DownloadPLOnClickListener)
    }

    override fun getItemCount(): Int {
        return playlistList.size
    }

    fun updateList(playlists: List<Playlist>) {
        playlistList = playlists
        notifyDataSetChanged()
    }
}