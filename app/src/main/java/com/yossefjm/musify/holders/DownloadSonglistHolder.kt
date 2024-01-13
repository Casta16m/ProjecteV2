package com.yossefjm.musify.holders

import android.view.View
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.yossefjm.musify.R
import com.yossefjm.musify.model.Playlist
import com.yossefjm.musify.model.Song

class DownloadSonglistHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
    private val titleTextView: TextView = itemView.findViewById(R.id.songNameMinimized)
    private val artistTextView: TextView = itemView.findViewById(R.id.artistNameMinimized)
    val likedSong: ImageView = itemView.findViewById(R.id.likesong)
    private val download: ImageView = itemView.findViewById(R.id.downloadSong)
//    private val coverImageView: TextView = itemView.findViewById(R.id.songCoverList)

    fun bind(
        song: Song,
        DownloadSongListOnClickListener: (Song) -> Unit) {
        titleTextView.text = song.title
        artistTextView.text = song.artist
        likedSong.visibility = View.GONE
        download.visibility = View.VISIBLE
        download.setOnClickListener {
            DownloadSongListOnClickListener(song)
        }
        // coverImageView.setBackgroundResource(song.coverPath.toInt())
    }


    fun updateLike(song: Song) {
        if (song.liked){
            likedSong.setImageResource(R.drawable.liketrue)
        } else {
            likedSong.setImageResource(R.drawable.likefalse)
        }
    }
}
