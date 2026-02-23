
<template>
  <div id="app">
    <el-menu v-if = "showNavigation" mode = "horizontal" router>
      <el-menu-item index = "/">На главную</el-menu-item>
      <el-sub-menu index = "1">
        <template #title>К данным: </template>
        <el-menu-item index = "/student">Студенты</el-menu-item>
        <el-menu-item index = "/administrator">Администрация</el-menu-item>
      </el-sub-menu>
      <el-sub-menu index = "2">
        <template #title>Создать: </template>
        <el-menu-item index = "/student/undefined">Студенты</el-menu-item>
        <el-menu-item index = "/administrator/undefined">Администрация</el-menu-item>
      </el-sub-menu>
      <el-menu-item index = "/account">Личный кабинет</el-menu-item>
    </el-menu>
    <nav v-if="showNavigation">
    </nav>
    <router-view />
    <el-footer class="footer" v-if = "showNavigation">
      <el-table>
        <el-table-column></el-table-column>
      </el-table>
    </el-footer>
  </div>
</template>

<style scoped>
.el-menu--horizontal > .el-menu-item:last-child {
  
}
.footer{
  vertical-align: bottom;
}
</style>
<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
const route = useRoute()
const showNavigation = computed(() => route.path !== '/authorisation' && route.path !== '/registration')
</script>
