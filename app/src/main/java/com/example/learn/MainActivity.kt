package com.example.learn

import android.annotation.SuppressLint
import android.content.Context
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.TextView

class MainActivity : AppCompatActivity() {
    private lateinit var resultText: TextView

    private lateinit var inputText: EditText
    private lateinit var getDataButton: Button
    private lateinit var saveDataButton:Button
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        resultText = findViewById(R.id.Result)
        inputText = findViewById(R.id.SavedText)
        getDataButton = findViewById(R.id.getDataButton)
        saveDataButton = findViewById(R.id.SaveButton)

    }

    private fun saveData()
    {
        val saveText = inputText.text.toString()
        val sharedPref = getSharedPreferences("sharedPref", Context.MODE_PRIVATE)
        val editor = sharedPref.edit()
        editor.apply{
            putString("String",saveText)
        }.apply()
            resultText.setText("Save result = true")



    }
    private fun getData()
    {
        val sharedPref = getSharedPreferences("sharedPref", Context.MODE_PRIVATE)
        val savedString = sharedPref.getString("String",null)
        resultText.text = savedString
    }
    fun saveButtonClick(view: View)
    {
        saveData()
    }
    fun getDataClick(view: View)
    {
        getData()
    }
}