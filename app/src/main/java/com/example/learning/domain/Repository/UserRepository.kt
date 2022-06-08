package com.example.learning.domain.Repository

import com.example.learning.domain.models.GetUserNameModel
import com.example.learning.domain.models.saveUserNameParam

import com.example.learning.domain.usecase.GetUserName

interface UserRepository {
    fun saveName(saveUserNameParam: saveUserNameParam):Boolean


    fun getName(): GetUserNameModel

}