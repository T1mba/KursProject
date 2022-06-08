package com.example.learning.Data.Repository


import com.example.learning.Data.Storage.UserModel
import com.example.learning.Data.Storage.UserStorage
import com.example.learning.domain.Repository.UserRepository
import com.example.learning.domain.models.GetUserNameModel
import com.example.learning.domain.models.saveUserNameParam

import com.example.learning.domain.usecase.GetUserName


class UserRepositoryImpl(private val userStorage: UserStorage) : UserRepository {

    override fun saveName(saveParam: saveUserNameParam): Boolean {
        val userSave = mapToStorage(saveParam)
        var result = userStorage.Save(userSave)
        return result
    }

    override fun getName(): GetUserNameModel {
        val user = userStorage.Get()

        return mapToDomain(user)

    }
    private fun mapToDomain(userModel:UserModel):GetUserNameModel{
        return GetUserNameModel(firstName = userModel.firstName, lastName = userModel.lastName)
    }
    private fun mapToStorage(param: saveUserNameParam): UserModel{
        return  UserModel(firstName = param.name, lastName =  param.lastName)
    }


}