package com.yossefjm.musify.adapters

import android.content.Context
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.yossefjm.musify.R
import com.yossefjm.musify.holders.PlayListViewHolder
import com.yossefjm.musify.model.Playlist
import kotlin.reflect.KFunction3

/**
 * Adaptador para la lista de canciones que utiliza un ViewHolder para presentar los elementos
 * de la lista, en este caso, objetos [Playlist]
 * Adapta la clase [Playlist] a la vista [R.layout.playlistitem]
 */
class PlaylistAdapter(
    private var playlistList: List<Playlist>,
    private val playlistOnClickListener: (Playlist) -> Unit,
    private val playlistEditClickListener: (Playlist) -> Unit
) :
    RecyclerView.Adapter<PlayListViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PlayListViewHolder {
        val inflater = LayoutInflater.from(parent.context)
        val itemView = inflater.inflate(R.layout.playlistitem, parent, false)
        return PlayListViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: PlayListViewHolder, position: Int) {
        val currentPlaylist = playlistList[position]
        holder.bind(currentPlaylist, playlistOnClickListener, playlistEditClickListener)

    }

    override fun getItemCount(): Int {
        return playlistList.size
    }

    fun updateList(playlists: List<Playlist>?) {
        if (playlists != null) {
            playlistList = playlists
        }
        notifyDataSetChanged()

    }
}


