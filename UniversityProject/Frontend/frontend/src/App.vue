<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
const route = useRoute()
const showNavigation = computed(() => route.path !== '/' && route.path !== '/registration')
</script>

<template>
  <div id="app">
    <nav v-if="showNavigation">
    </nav>
    <router-view />
  </div>
</template>

<style scoped></style>
