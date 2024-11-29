import './assets/main.css'
import { loadSlim } from "@tsparticles/slim"; 
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Particles from "@tsparticles/vue3";

createApp(App).use(router).use(Particles, {
    init: async engine => {
      await loadSlim(engine);
    },
  }).use().mount('#app');
