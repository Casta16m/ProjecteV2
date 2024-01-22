package com.yossefjm.musify.extension

import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import android.content.Context
import android.net.wifi.WifiInfo
import android.net.wifi.WifiManager
import android.os.Build
import android.util.Log


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
fun getMacAddress(context: Context): String {
    val wifiManager = context.getSystemService(Context.WIFI_SERVICE) as WifiManager

    // Asegurarse de que el Wi-Fi esté habilitado
    wifiManager.isWifiEnabled = true

    // Obtener información sobre la conexión Wi-Fi
    val wifiInfo: WifiInfo? = wifiManager.connectionInfo

    // Verificar si la información de Wi-Fi está disponible
    if (wifiInfo != null) {
        val macAddress: String = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.Q) {
            // A partir de Android 10 (API nivel 29), se requiere el permiso ACCESS_FINE_LOCATION
            wifiInfo.macAddress
        } else {
            wifiInfo.bssid // Anterior a Android 10, se utiliza BSSID
        }

        Log.d("DeviceInfo", "Dirección MAC del dispositivo: $macAddress")
        return macAddress
    } else {
        Log.e("DeviceInfo", "No se pudo obtener la información de Wi-Fi.")
        return ""
    }
}

