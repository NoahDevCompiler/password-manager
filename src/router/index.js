import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import PwGenerateView from '@/views/PwGenerateView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import ManagerView from '@/views/ManagerView.vue'
import { useToken } from '@/api/auth.js'

const { token } = useToken();

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      
    },
    {
      path: '/pwgenerate',
      component: PwGenerateView,
    },
    {
      path: '/login',
      component: LoginView,
    },
    {
      path: '/register',
      component: RegisterView,
    },
    {
      path: '/pwmanager',
      component: ManagerView,
      beforeEnter(to, from, next){
        if(!token.value){
          next({
            path: '/login'
          })
        } else{
          next()
        }
      }
    },
  ],
})

export default router
