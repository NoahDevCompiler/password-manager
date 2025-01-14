import './assets/main.css'
import { loadSlim } from "@tsparticles/slim";
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Particles from "@tsparticles/vue3";
import ElementPlus from 'element-plus';

import 'element-plus/dist/index.css';

const app = createApp(App);

app.use(router)
app.use(Particles, {
  init: async engine => {
    await loadSlim(engine);
  },
});
app.use(ElementPlus)

app.mount('#app');