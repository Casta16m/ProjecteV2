package com.yossefjm.musify.holders

import android.view.View
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.yossefjm.musify.R
import com.yossefjm.musify.model.Playlist

/**
 * ViewHolder que contiene la lógica para mostrar una canción en la lista de reproducción
 * @param itemView vista que contiene los elementos de la lista de reproducción
 */
class PlayListViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
    private val nameTextView: TextView = itemView.findViewById(R.id.playlistTitle)
    private val descriptionTextView: TextView = itemView.findViewById(R.id.playlistDescr)
    private val download: ImageView = itemView.findViewById(R.id.downloadplaylist)
    //  private val coverImageView: TextView = itemView.findViewById(R.id.playListCoverList)


    fun bind(
        playlist: Playlist,
        playlistOnClickListener: (Playlist) -> Unit,
        playlistEditClickListener: (Playlist) -> Unit
    ) {
        nameTextView.text = playlist.name
        descriptionTextView.text = playlist.description
        // coverImageView.setBackgroundResource(playlist.coverPath.toInt())

        download.visibility = View.GONE

        itemView.setOnClickListener {
            playlistOnClickListener(playlist)
        }
        itemView.findViewById<ImageView>(R.id.editplaylist).setOnClickListener {
            playlistEditClickListener(playlist)
        }


        // Puedes agregar más lógica de vinculación según sea necesario
    }


}