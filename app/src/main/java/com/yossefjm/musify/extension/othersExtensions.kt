package com.yossefjm.musify.extension

import android.content.Context
import android.net.wifi.WifiInfo
import android.net.wifi.WifiManager
import android.os.Build
import android.provider.Settings
import android.util.Log
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter


fun Long.toHourMinSecString(): String {
    val hours = (this / (1000 * 60 * 60)) % 24
    val minutes = (this / (1000 * 60)) % 60
    val seconds = (this / 1000) % 60
    if (hours == 0L) return String.format("%02d:%02d", minutes, seconds)
    return String.format("%02d:%02d:%02d", hours, minutes, seconds)
}

fun LocalDateTime.toExtendedIsoString(): String {
    val formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss.SSSSSSS")
    return this.format(formatter)
}

fun getDeviceId(context: Context): String {
    return Settings.Secure.getString(context.contentResolver, Settings.Secure.ANDROID_ID) ?: "unknown"
}
