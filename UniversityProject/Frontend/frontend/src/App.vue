
<template>
  <div id="app">
    <el-menu v-if = "showNavigation" mode = "horizontal" router :ellipsis="false">
      <el-menu-item index = "/">На главную</el-menu-item>
      <el-sub-menu index = "1">
        <template #title>К данным: </template>
        <el-menu-item index = "/student">Студенты</el-menu-item>
        <el-menu-item index = "/administrator">Администрация</el-menu-item>
      </el-sub-menu>
      <el-menu-item index = "/account">Личный кабинет</el-menu-item>
    </el-menu>
    <nav v-if="showNavigation">
    </nav>
    <div class = "main-content">
      <router-view />
    </div>
    <el-footer class="app-footer">
      <el-row justify="space-between" align="middle">
        <el-col :span="12">
          <span>© {{ currentYear }} Университет. Все права защищены.</span>
        </el-col>
        <el-col :span="12" class="footer-links">
          <el-link href="#" :underline="false">Политика конфиденциальности</el-link>
          <el-divider direction="vertical" />
          <el-link href="#" :underline="false">Контакты</el-link>
        </el-col>
      </el-row>
    </el-footer>
  </div>
</template>

<style scoped>
.el-menu--horizontal > .el-menu-item:last-child {
  right: 90%;
}
.app-footer {
  background-color: #f5f7fa;
  border-top: 1px solid #e4e7ed;
  font-size: 14px;
  color: #606266;
  width: 100%;
}

.footer-links {
  text-align: right;
}

.el-link {
  font-size: 14px;
}
</style>
<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
const route = useRoute()
const showNavigation = computed(() => route.path !== '/authorisation' && route.path !== '/registration')
const currentYear = computed(() => new Date().getFullYear())
</script>
