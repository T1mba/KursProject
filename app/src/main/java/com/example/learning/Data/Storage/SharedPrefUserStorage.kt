package com.example.learning.Data.Storage

import android.content.Context


private const val SHARED_PREFS_NAME = "shared_prefs_name"
private const val KEY_FIRST_NAME = "firstName"
private const val KEY_LAST_NAME = "lastName"

class SharedPrefUserStorage(context: Context): UserStorage{

    private val sharedPreferences = context.getSharedPreferences(SHARED_PREFS_NAME, Context.MODE_PRIVATE)

    override fun Save(userModel: UserModel): Boolean {

        sharedPreferences.edit().putString(KEY_FIRST_NAME,userModel.firstName).apply()
        sharedPreferences.edit().putString(KEY_LAST_NAME,userModel.lastName).apply()

        return true
    }

    override fun Get(): UserModel {

        val firstName = sharedPreferences.getString(KEY_FIRST_NAME, "")?: ""
        val secondName = sharedPreferences.getString(KEY_LAST_NAME, "" )?: ""


        return UserModel(firstName = firstName, lastName = secondName)
    }
}