<template>
  <el-form size = "large" show-message = "true" class = "form" ref = "formRef" :model = "formData" :rules="rules">
    <h1>Регистрация</h1>
      <el-form-item label = "Почта: " class = "registerForm" required prop = "email" label-position="top">
        <el-input placeholder="Введите почту" type = "text" clearable = "true" v-model="formData.email"/>
      </el-form-item>
      <el-form-item label = "Логин: " class = "registerForm" required prop = "login" label-position="top">
        <el-input placeholder="Введите логин" type = "text" clearable = "true" v-model = "formData.login"/>
      </el-form-item>
      <el-form-item label = "Телефон: " class = "registerForm" required prop = "phone" label-position="top"> 
        <el-input placeholder="Введите телефон" type = "text" clearable = "true" v-model = "formData.phone"/>
      </el-form-item>
    <el-form-item label = "Пароль: " required prop = "password" label-position="top" class = "registerForm">
      <el-input type = "password" show-password = "true" placeholder="Введите пароль" clearable = "true" v-model = "formData.password"/>
    </el-form-item>
    <el-form-item label = "Подтвердите пароль: " required prop = "confirmPassword" label-position="top" class = "registerForm">
      <el-input type = "password" show-password = "true" placeholder="Подтвердите пароль" clearable = "true" v-model = "formData.confirmPassword"/>
    </el-form-item>
    <el-form-item class = "submit">
      <el-button type = "primary" @click = "Submit">Зарегистрироваться</el-button>
    </el-form-item>
  </el-form>
</template>

<style scoped>
  .registerForm
  {
    padding-right: 60px;
    margin-top: 20px;
  }
  .form
  {
    margin-left:25%;
    padding-left: 5px;
    padding-right: 5px;
    margin-right:25%;
    margin-top: 2%;
    border: 3px solid;
  }
  .submit
  {
    margin-top:40px;
  }
</style>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import type {RegistrationForm, ValidationRule} from '@/types/auth.ts'
import type {FormRules, FormInstance} from "element-plus"
import axios, {type AxiosResponse, type AxiosError} from "axios"
import api from "@/Api.ts"
const formData = reactive<RegistrationForm>({
  email: '',
  login: '',
  phone: '',
  password: '',
  confirmPassword: '',
  rememberMe: false
})
const formRef = ref<FormInstance>()
const rules : FormRules<typeof formData> = {
  email: [{required: true, trigger: 'blur'},
    {type: 'email', message: 'Некорректный email', trigger: ['blur', 'change']},],
  login: [{required: true, trigger: 'blur'},
    {min:3, max:15, required: true, trigger: ['blur', 'change']}],
  phone: [{required: true, trigger: 'blur'},
    {pattern: /^[+]?[\d\s\-()]{10,}$/, message: "Некорректный phone", trigger: ['blur', 'change']}],
  password: [{required: true, trigger: 'blur'},
    {
      validator: (rule, value : string, callback) =>
      {
        if (!value) {
          callback() ;
          return;
        }
        if(value.length < 5)
        {
          callback(new Error('Пароль слишком короткий'));
          return;
        }
        if(value.length > 30)
        {
          callback(new Error('Пароль слишком длинный'));
          return;
        }
        if (!value) {
          callback();
          return;
        }
        if(!/[A-ZА-Я]/.test(value))
        {
          callback(new Error('Нет заглавных букв'));
          return;
        }
        if(!/[a-zа-я]/.test(value))
        {
          callback(new Error('Только заглавные буквы'));
          return;
        }
        if(!/[0-9]/.test(value))
        {
          callback(new Error('Нет цифр'));
          return;
        }
        if(!/[@$!%*?&#()_+\-=\[\]{}|;:,.<>\/~]/.test(value))
        {
          callback(new Error('добавьте спец. символы'));
          return;
        }
        if(value == formData.login)
        {
          callback(new Error('Пароль не может быть логином'));
          return;
        }
        callback();
        return;
      }, trigger: ['blur', 'change']
    }],
  confirmPassword: [{required: true, trigger: 'blur'},
    {min:6, max:30, trigger: ['blur', 'change']},
    {
      validator: (rule, value: string, callback) => {
        if(value !== formData.password)
        {
          callback(new Error('Пароли не совпадают'))
        }
        else
        {
          callback();
        }},
      trigger: ['blur', 'change']
    }]
}
const Submit = (data: RegistrationForm) => {
  try {
    const dataToJson = {email: formData.email.trim(), login: formData.login.trim(), phone: formData.phone, password: formData.password, rememberMe: formData.rememberMe};
    api.post('/register', formData)
  }
  catch (error : any) {
    if (error.response) {
      console.log('Ошибка сервера', error.response.status, error.response.data)
    } else if (error.request) {
      console.log('Нет ответа', error.request)
    } else {
      console.log('Ошибка', error.message)
    }
  }
}
</script>