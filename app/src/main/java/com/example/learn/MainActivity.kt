package com.example.learn

import android.annotation.SuppressLint
import android.content.Context
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.lifecycle.ViewModelProvider

class MainActivity : AppCompatActivity() {
        private lateinit var resultText:TextView
        private lateinit var enterText:EditText
        private lateinit var saveDataButton:Button
        private lateinit var getDataButton:Button
        private lateinit var vm:MainViewModel
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        resultText = findViewById(R.id.resultText)
        enterText = findViewById(R.id.savedText)
        saveDataButton = findViewById(R.id.saveDataButton)
        getDataButton = findViewById(R.id.getDataButton)
        vm = ViewModelProvider(this).get(MainViewModel::class.java)
        Log.d("AAA", "Activity Created")
    }

    private fun saveData(){
        val savedText = enterText.text.toString()
        val sharPref = getSharedPreferences("Prefs",Context.MODE_PRIVATE)
        val editor = sharPref.edit()
        editor.apply{
           putString("String_Key",savedText)
        }.apply()
        resultText.text = "Save Result = true"
    }
    private fun getData(){
        val sharPref = getSharedPreferences("Prefs",Context.MODE_PRIVATE)
        val sharedString = sharPref.getString("String_Key",null)
        resultText.text = sharedString
    }
    fun saveDataButtonClick(view: View) {
        saveData()
    }
    fun getDataButtonClick(view: View) {
        getData()
    }


}