import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import PwGenerateView from '@/views/PwGenerateView.vue'
import LoginView from '@/views/LoginView.vue'

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
  ],
})

export default router
