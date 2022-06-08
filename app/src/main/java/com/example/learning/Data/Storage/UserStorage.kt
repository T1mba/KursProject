package com.example.learning.Data.Storage

interface UserStorage {

    fun Save(userModel: UserModel): Boolean
    fun Get(): UserModel

}