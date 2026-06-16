<template>
  <div class="home-page">
    <el-row :gutter="20">
      <el-col :span="24">
        <el-card class="welcome-card" shadow="hover">
          <h2>Главная страница администрирования ВУЗа</h2>
          <p>{{ currentDate }}</p>
        </el-card>
      </el-col>
    </el-row>

    <el-row :gutter="20" class="navigation-block">
      <el-col :span="24">
        <el-card shadow="never" :body-style="{ padding: '0' }">
          <el-menu mode="vertical" router class="secondary-menu">
            <el-sub-menu index="1">
              <template #title>К данным:</template>
              <el-menu-item index="/student" v-if = "accessPage.visibilityPage.studentVisibility">Студенты</el-menu-item>
              <el-menu-item index="/administrator" v-if = "accessPage.visibilityPage.adminVisibility">Администрация</el-menu-item>
            </el-sub-menu>
            <el-sub-menu index="2" v-if = "accessPage.visibilityPage.deleteCreateVisibility">
              <template #title>Создать:</template>
              <el-menu-item index="/student/undefined" v-if = "accessPage.visibilityPage.studentVisibility">Студенты</el-menu-item>
              <el-menu-item index="/administrator/undefined" v-if = "accessPage.visibilityPage.adminVisibility">Администрация</el-menu-item>
            </el-sub-menu>
            <el-sub-menu index="3">
              <template #title>К дополнительной информации о вузе:</template>
              <el-menu-item index="/student/undefined" v-if = "accessPage.visibilityPage.adminVisibility">Бюджет</el-menu-item>
              <el-menu-item index="/administrator/undefined"></el-menu-item>
            </el-sub-menu>
          </el-menu>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script setup lang = "ts">
import {onMounted, ref} from 'vue'
import {userAccessPage} from "@/stores/AccessPage.js";
import type {VisibilityPage} from "@/types/Visibility.js";
const accessPage = userAccessPage();
const currentDate = ref(
    new Date().toLocaleDateString('ru-RU', {
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    })
)
</script>

<style scoped>
.home-page {
  padding: 20px;
}

.welcome-card {
  margin-bottom: 20px;
  text-align: center;
}

.navigation-block {
  margin-bottom: 20px;
}
</style>