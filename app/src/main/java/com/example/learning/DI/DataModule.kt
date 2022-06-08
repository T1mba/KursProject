package com.example.learning.DI

import com.example.learning.Data.Repository.UserRepositoryImpl
import com.example.learning.Data.Storage.SharedPrefUserStorage
import com.example.learning.Data.Storage.UserStorage
import com.example.learning.domain.Repository.UserRepository
import org.koin.dsl.module



val dataModule = module{
single<UserStorage>{

    SharedPrefUserStorage(context = get())
}
    //get Ищет зависимость для нас в друг модулях DI
    single<UserRepository>{
        UserRepositoryImpl(userStorage = get())
    }


}