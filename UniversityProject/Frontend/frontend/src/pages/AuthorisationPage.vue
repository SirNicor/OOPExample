<template>
  <el-form class = "form" ref = "formRef" :model = "formData" :rules = "rules">
    <h1>Авторизация</h1>
    <el-form-item label = "Почта: " class = "registerForm" prop = "Email" label-position="top">
      <el-input placeholder="Введите почту" type = "text" clearable = "true" v-model="formData.Email"/>
    </el-form-item>
    <el-form-item label = "Логин: " class = "registerForm" prop = "Login" label-position="top">
      <el-input placeholder="Введите логин" v-model="formData.Login" clearable/>
    </el-form-item>
    <el-form-item label = "Телефон: " class = "registerForm" prop = "Phone" label-position="top"> 
      <el-input placeholder="Введите телефон" v-model="formData.Phone" clearable/>
    </el-form-item>
    <el-form-item label = "Пароль: " prop = "Password" label-position="top" class = "registerForm">
      <el-input type = "password" show-password placeholder="Введите пароль" v-model="formData.Password" clearable/>
    </el-form-item>
    <el-form-item inline = "true" class = "submit">
      <el-button type = "primary" native-type="submit" @click = "Send">Войти</el-button>
      <el-checkbox type = "primary" v-model="formData.rememberMe" size = "large" class = "rememberMe">Запомнить</el-checkbox>
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
  .rememberMe, .reg
  {
    margin-left: 2%;
  }
  .submit
  {
    margin-top: 40px;
  }
</style>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import type {AuthForm, RegistrationForm} from '@/types/auth.ts'
import {type FormRules, type FormInstance, ElMessage} from "element-plus"
import {AuthorizationResponse} from "@/api/Authorization.ts"
async function Send()
{
  debugger;
  if(!formRef.value) return;
  try {
    await formRef.value.validate();
  } catch (error) {
    ElMessage.error('Пожалуйста,  заполните все поля корректно');
    return;
  }
  let response = await AuthorizationResponse.Login(formData);
  let accessJWT = response.data.accessjwt;
  let refreshJWT = response.data.refreshjwt;
  localStorage.setItem('access_token', accessJWT);
  localStorage.setItem('refresh_token', refreshJWT);
}
const formData = reactive<AuthForm>({
  Login: '',
  Phone: '',
  Email: '',
  Password: '',
  rememberMe: false
})
const formRef = ref<FormInstance>();
const isSubmit = false;
const rules : FormRules<typeof formData> = {
  Email: [{required: true, message: 'обязательно', trigger: ['blur', 'change']},
    {type: 'email', message: 'Некорректный email', trigger: ['blur', 'change']},],
  Login: [{required: true, message: 'обязательно', trigger: ['blur', 'change']},
    {min:3, max:15, required: true, trigger: ['blur', 'change']}],
  Phone: [{required: true, message: 'обязательно', trigger: ['blur', 'change']},
    {pattern: /^[+]?[\d\s\-()]{10,}$/, message: "Некорректный phone", trigger: ['blur', 'change']}],
  Password: [{required: true, message: 'обязательно', trigger: ['blur', 'change']},
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
        if(value == formData.Login)
        {
          callback(new Error('Пароль не может быть логином'));
          return;
        }
        callback();
        return;
      }, trigger: ['blur', 'change']
    }],
}
</script>