<script setup>
import HelloWorld from './components/HelloWorld.vue'
</script>

<template>
  <el-header>
    <el-page-header>Авторизация в системе администрирования ВУЗа</el-page-header>
  </el-header>

  <el-main>
    <el-form label = "Авторизация:">
      <el-form-item>
        <el-input v-model = "input" placeholder = "Ввод логина"/>
        <el-input v-model = "input" placeholder = "Ввод телефона"/>
        <el-input v-model = "input" type = "password" placeholder = "Ввод пароля" show-password/>
      </el-form-item>
    </el-form>
  </el-main>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
