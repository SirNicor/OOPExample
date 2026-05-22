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
import {userAccessPage} from "@/stores/AccessPage.ts";
import {ElMessage} from "element-plus";

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
          name: 'StudentRegister',
          component: StudentRegistry,
          meta: {
              title: 'Student',
              requiresAuth: true
          }
      },
      {
          path: `/student/:index`,
          name: 'StudentPage',
          component: StudentPage,
          meta: {
              title: 'Student',
              requiresAuth: true
          }
      },
      {
          path: '/administrator',
          name: 'AdministratorRegister',
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

    if (to.path === '/authorisation' || to.path === '/registration') {
        next();
        return;
    }
    const accessJWT = GetCookie('accessJWT');
    
    if (accessJWT === undefined) {
        next('/authorisation');
        return;
    }
    try {
        const response = await AuthorizationResponse.CheckAccessToken(accessJWT);
        if(to.path === `/` || to.path === `/account`)
        {
            next();
        }
        else
        {
            if(userAccessPage().canAccessForAllOperationName(typeof to.name === "string" ? to.name : "", ["Read", "All"]))
            {
                next();
                return;
            }
            ElMessage.error("Доступ запрещен");
            next(false);
        }
    } catch (error: any) {
        console.error('Token validation FAILED:', {
            error: error.message,
            status: error.response?.status,
            data: error.response?.data
        });
        next('/authorisation');
    }
});

export default router
