<template>
  <el-form class = "form" ref = "formRef" :model = "formData" :rules = "rules">
    <h1>Авторизация</h1>
    <el-form-item label = "Логин: " class = "registerForm" required prop = "login" label-position="top">
      <el-input placeholder="Введите логин" v-model="formData.login" clearable/>
    </el-form-item>
    <el-form-item label = "Телефон: " class = "registerForm" required prop = "phone" label-position="top"> 
      <el-input placeholder="Введите телефон" v-model="formData.phone" clearable/>
    </el-form-item>
    <el-form-item label = "Пароль: " required prop = "password" label-position="top" class = "registerForm">
      <el-input type = "password" show-password placeholder="Введите пароль" v-model="formData.password" clearable/>
    </el-form-item>
    <el-form-item inline = "true" class = "submit">
      <el-button type = "primary" native-type="submit">Войти</el-button>
      <el-checkbox type = "primary" v-model="formData.rememberMe" size = "large" class = "rememberMe">Запомнить</el-checkbox>
      <el-button link tag = "a" href = "/registration" size = "large" native-type="submit" class = "reg">Регистрация</el-button>
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
import type {FormRules, FormInstance} from "element-plus"
const formData = reactive<AuthForm>({
  login: '',
  phone: '',
  password: '',
  rememberMe: false
})
const formRef = ref<FormInstance>();
const isSubmit = false;
const rules : FormRules<AuthForm> = {
  login: [{required: true, trigger: 'blur'},
    {min:3, max:15, required: true, trigger: ['blur', 'change']}],
  phone: [{required: true, trigger: 'blur'},
    {pattern: /^[+]?[\d\s\-()]{10,}$/, message: "Некорректный phone", trigger: ['blur', 'change']}],
  password: [{required: true, trigger: 'blur'},
    {min:6, max:30, trigger: ['blur', 'change']}],
  rememberMe: [],
}
</script>