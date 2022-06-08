package com.example.learning.UI

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import com.example.learning.domain.models.GetUserNameModel
import com.example.learning.domain.models.saveUserNameParam
import com.example.learning.domain.usecase.GetUserName
import com.example.learning.domain.usecase.SaveUserName

class MainViewModel(private val getUserName: GetUserName, private val saveUserName: SaveUserName) : ViewModel() {


      private val resultLiveMutable = MutableLiveData<String>()

    val resultLive:LiveData<String> = resultLiveMutable


    init{
        Log.e("TTT", "VM Created")
    }

// Метод который вызывает когда Activity,связаная с ViewModel уничтожается,пропадает
    override fun onCleared() {
    Log.e("TTT", "VM Created")
        super.onCleared()
    }



    fun Save(name:String,lastName:String ){

        val params = saveUserNameParam(name = name,lastName = lastName)
        val result = saveUserName.execute(param = params)
        resultLiveMutable.value = "Save Result = $result"
    }

    fun Load()
    {
        val userName: GetUserNameModel = getUserName.execute()
        resultLiveMutable.value = "${userName.firstName} ${userName.lastName}"

    }
}