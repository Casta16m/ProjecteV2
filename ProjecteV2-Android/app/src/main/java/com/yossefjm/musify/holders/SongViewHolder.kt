package com.yossefjm.musify.holders

import android.view.View
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.yossefjm.musify.R
import com.yossefjm.musify.model.Song

class SongViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
    private val titleTextView: TextView = itemView.findViewById(R.id.songNameMinimized)
    private val artistTextView: TextView = itemView.findViewById(R.id.artistNameMinimized)
    val likedSong: ImageView = itemView.findViewById(R.id.likesong)
    private val coverImageView: ImageView = itemView.findViewById(R.id.songCover)

    fun bind(song: Song) {
        titleTextView.text = song.title
        artistTextView.text = song.artist
        if (song.liked){
            likedSong.setImageResource(R.drawable.liketrue)
        } else {
            likedSong.setImageResource(R.drawable.likefalse)
        }

        // coverImageView.setImageResource(song.coverPath.toInt())
        // coverImageView.setBackgroundResource(song.coverPath.toInt())

        // Puedes agregar más lógica de vinculación según sea necesario
    }


    fun updateLike(song: Song) {
        if (song.liked){
            likedSong.setImageResource(R.drawable.liketrue)
        } else {
            likedSong.setImageResource(R.drawable.likefalse)
        }
    }
}
