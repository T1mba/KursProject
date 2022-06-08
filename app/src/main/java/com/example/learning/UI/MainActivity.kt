package com.example.learning.UI

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModelProvider
import com.example.learning.Data.Repository.UserRepositoryImpl
import com.example.learning.Data.Storage.SharedPrefUserStorage
import com.example.learning.Data.Storage.UserStorage
import com.example.learning.R
import com.example.learning.domain.models.GetUserNameModel
import com.example.learning.domain.models.saveUserNameParam

import com.example.learning.domain.usecase.GetUserName
import com.example.learning.domain.usecase.SaveUserName
import org.koin.androidx.viewmodel.ext.android.viewModel

class MainActivity : AppCompatActivity() {



// Создание ViewModel В Activity с помощью Koin
    private  val ViewModel by viewModel<MainViewModel>()

    private lateinit var resultText: TextView
    private lateinit var savedNameText: EditText
    private lateinit var savedLastNameText: EditText
    private lateinit var getData:Button
    private lateinit var saveData: Button


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)


        Log.e("TTT", "Activity Created")

        //Подиска и автоматическая отписка от обновления данных во ViewModel
        ViewModel.resultLive.observe(this, Observer {
            resultText.text = it
        })

        resultText = findViewById(R.id.result)
        savedLastNameText =findViewById(R.id.savedLastNameText)
        savedNameText = findViewById(R.id.savedNameText)
        getData = findViewById(R.id.getDataButton)
        saveData = findViewById(R.id.SaveDataButton)
    }

    fun saveDataClick(view: View) {
        val textName = savedNameText.text.toString()
        val lastNameText = savedLastNameText.text.toString()
        ViewModel.Save(textName,lastNameText)

    }
    fun getDataClick(view: View) {

        ViewModel.Load()
    }
}