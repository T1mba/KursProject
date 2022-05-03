package com.example.learn

import android.util.Log
import androidx.lifecycle.ViewModel

class MainViewModel : ViewModel() {
    init{
        Log.e("AAA","Vm created")
    }


//Вызывается когда связанная с ней активити уничтожается
    override fun onCleared()
    {
        Log.e("AAA","Vm cleared")
        super.onCleared()
    }

}