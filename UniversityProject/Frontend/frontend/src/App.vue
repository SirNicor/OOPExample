
<template>
  <div id="app">
    <el-menu v-if = "showNavigation" mode = "horizontal" router :ellipsis="false">
      <el-menu-item index = "/"><img src = "../public/college-grants.svg" alt = "Главная страница" class = "menu-icon"></el-menu-item>
      <el-menu-item index = "/student" v-if = "accessPage.visibilityPage.studentVisibility">Список студентов</el-menu-item>
      <el-menu-item index = "/administrator" v-if = "accessPage.visibilityPage.adminVisibility">Список администрации</el-menu-item>
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
.menu-icon{
  width: 24px;
  height: 24px;
  vertical-align: middle;
  object-fit: contain; 
}
</style>
<script setup lang="ts">
import {computed, onMounted, ref} from 'vue'
import { useRoute } from 'vue-router'
import {DeleteAllJWTToken} from "@/Function/CookiesFunction.ts";
import type {VisibilityPage} from "@/types/Visibility.ts";
import {userAccessPage} from "@/stores/AccessPage.ts";
const route = useRoute()  
const showNavigation = computed(() => route.path !== '/authorisation' && route.path !== '/registration')
const currentYear = computed(() => new Date().getFullYear())
const accessPage = userAccessPage();
const visibilityPage = ref<VisibilityPage>(
    {
      studentVisibility:false,
      adminVisibility: false,
      deleteCreateVisibility:false,
    }
);
onMounted(() =>
{
  accessPage.ResetForLocalStorageAccessPage();
})
</script>
