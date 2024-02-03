package com.yossefjm.musify.extension

import android.content.Context
import android.content.pm.PackageManager
import android.util.Log
import android.view.View
import androidx.activity.result.contract.ActivityResultContracts
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import com.google.android.material.snackbar.Snackbar

class PermissionExtension(private val activity: AppCompatActivity) {

    companion object {
        // const val permission = Manifest.permission.READ_MEDIA_AUDIO
    }

    /**
     * Para solicitar permisos de forma sencilla
     */
    private val requestPermissionLauncher =
        activity.registerForActivityResult(
            ActivityResultContracts.RequestPermission()
        ) { isGranted: Boolean ->
            if (isGranted) {
                Log.i("Permission: ", "Granted")
                // Acciones a realizar si se otorga el permiso (puedes agregar aquí si es necesario)
            } else {
                Log.i("Permission: ", "Denied")
                // Acciones a realizar si se deniega el permiso (puedes agregar aquí si es necesario)
            }
        }


    /**
     * Para verificar si el permiso ya está otorgado
     */
    fun Context.hasReadMediaAudioPermission(permission: String): Boolean {
        return ContextCompat.checkSelfPermission(
            this,
            permission
        ) == PackageManager.PERMISSION_GRANTED
    }

    /**
     * Para mostrar un Snackbar con la explicación del permiso y la opción para solicitarlo
     */
    fun View.showSnackbar(
        msg: String,
        length: Int,
        actionMessage: CharSequence? = null,
        action: ((View) -> Unit)? = null
    ) {
        val snackbar = Snackbar.make(this, msg, length)

        actionMessage?.let {
            snackbar.setAction(it) { action?.invoke(this) }.show()
        } ?: snackbar.show()
    }

    /**
     * Para verificar si el permiso ya está otorgado, y solicitarlo si no lo está
     */
    fun checkAndRequestPermissions(permission: String): Boolean {
        return if (activity.hasReadMediaAudioPermission(permission)) {
            // Permiso ya otorgado
            true
        } else {
            if (ActivityCompat.shouldShowRequestPermissionRationale(
                    activity,
                    permission
                )
            ) {
                // Explicación necesaria, mostrar Snackbar con explicación y opción para solicitar permiso
                activity.findViewById<View>(android.R.id.content).showSnackbar(
                    "Permission required",
                    Snackbar.LENGTH_INDEFINITE,
                    "OK"
                ) {
                    requestPermissionLauncher.launch(permission)
                }
            } else {
                // Solicitar el permiso directamente si no está otorgado
                requestPermissionLauncher.launch(permission)
            }
            false
        }
    }

    /**
     * Para manejar la respuesta de la solicitud de permisos
     */
    fun onRequestPermissionsResult(requestCode: Int, grantResults: IntArray) {
        /*
        when (requestCode) {
            // Puedes manejar otros códigos de solicitud de permisos aquí si es necesario
        }
         */
    }
}
