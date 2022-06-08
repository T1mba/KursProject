package com.example.learning.domain.usecase

import android.app.AlertDialog
import android.content.Context
import com.example.learning.domain.Repository.UserRepository
import com.example.learning.domain.models.GetUserNameModel


class GetUserName (private val userRepository: UserRepository,private val context: Context){
    fun execute(): GetUserNameModel
    {




        return  userRepository.getName()

    }
}