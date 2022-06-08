package com.example.learning.DI

import com.example.learning.domain.usecase.GetUserName
import com.example.learning.domain.usecase.SaveUserName
import org.koin.dsl.module

val domainModule = module {
    factory <GetUserName>{
        GetUserName(
            userRepository =  get(),
            context = get()
        )
    }
    factory<SaveUserName>{
       SaveUserName(userRepository = get()
       )
    }
}