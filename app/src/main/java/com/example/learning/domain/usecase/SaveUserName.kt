package com.example.learning.domain.usecase

import com.example.learning.domain.Repository.UserRepository
import com.example.learning.domain.models.saveUserNameParam

class SaveUserName(private val userRepository: UserRepository) {

    fun execute(param: saveUserNameParam): Boolean
    {
        val oldUserName = userRepository.getName()

        if(oldUserName.firstName == param.name && oldUserName.lastName == param.lastName){
            return true
        }



        val result = userRepository.saveName(saveUserNameParam = param)
        return result


    }
}