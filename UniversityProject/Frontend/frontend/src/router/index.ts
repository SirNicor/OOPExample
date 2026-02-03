import { createRouter, createWebHistory } from 'vue-router'
import AuthorisationPage from "@/pages/AuthorisationPage.vue";
import RegistrationPage from "@/pages/RegistrationPage.vue";
import Home from "@/pages/Home.vue";
import StudentPage from "@/pages/StudentPage.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
      {
          path: '/',
          name: 'authorisation',
          component: AuthorisationPage,
          meta: {
              title: 'Authorisation',
              requiresAuth: false
          }
      },
      {
          path: '/registration',
          name: 'registration',
          component: RegistrationPage,
          meta: {
              title: 'Registration',
              requiresAuth: false
          }
      },
      {
          path: '/home',
          name: 'home',
          component: Home,
          meta: {
              title: 'Home',
              requiresAuth: true    
          }
      },
      {
          path: '/student/:sortKey?/:sortOrder?',
          name: 'student',
          component: StudentPage,
          meta: {
              title: 'Student',
              requiresAuth: true
          }
      }
  ],
})

export default router
