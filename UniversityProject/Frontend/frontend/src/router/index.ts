import { createRouter, createWebHistory } from 'vue-router'
import {AuthorizationResponse} from "@/api/Authorization.ts"
import AuthorisationPage from "@/pages/AuthorisationPage.vue";
import RegistrationPage from "@/pages/RegistrationPage.vue";
import MainPage from "@/pages/MainPage.vue";
import StudentRegistry from "@/pages/StudentRegistry.vue";
import StudentPage from "@/pages/StudentPage.vue";
import AdministratorRegistry from "@/pages/AdministratorRegistry.vue";  
import AccountPage from "@/pages/AccountPage.vue";
import {GetCookie} from "@/Function/CookiesFunction.ts"

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
      {
          path: '/',
          name: 'main',
          component: MainPage,
          meta:
              {
                  title: "MainPage",
                  requiresAuth: true,
              } 
      },
      {
          path: '/authorisation',
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
          path: '/student',   
          name: 'student',
          component: StudentRegistry,
          meta: {
              title: 'Student',
              requiresAuth: true
          }
      },
      {
          path: `/student/:index`,
          name: 'studentPage',
          component: StudentPage,
          meta: {
              title: 'Student',
              requiresAuth: true
          }
      },
      {
          path: '/administrator',
          name: 'administrator',
          component: AdministratorRegistry,
          meta: {
              title: 'administrator',
              requiresAuth: true
          }
      },
      {
          path: '/account',
          name: 'accountPage',
          component: AdministratorRegistry,
          meta: {
              title: 'account',
              requiresAuth: true
          }
      },
  ],
})

router.beforeEach(async (to, from, next) => {
    if(to.path === `/authorisation` || to.path === `/registration`) {
        next();
        return;
    }
    let accessJWT = GetCookie('accessJWT');
    debugger;
    if(accessJWT === undefined)
    {
        next('/authorisation');
        return;
    }
    try {
        await AuthorizationResponse.CheckAccessToken(accessJWT);
        next();
        return;
    }
    catch
    {
        next('/authorisation');
    }
}) 

export default router
