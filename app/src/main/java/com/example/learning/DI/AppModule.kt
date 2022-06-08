package com.example.learning.DI

import com.example.learning.UI.MainViewModel
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module

// Правила для создания ViewModel

val appModule = module {

    viewModel<MainViewModel>{
        MainViewModel(
            getUserName = get(),
            saveUserName = get()
        )
    }

}