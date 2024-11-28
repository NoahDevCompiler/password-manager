<script setup>
import { RouterLink, RouterView } from 'vue-router'
import NavBar from './components/NavBar.vue';
</script>


<template>
  <div id="app">
    <NavBar class="navbar"/>
    <nav>
      <RouterLink to="/"/>
    </nav>
    <router-view />
  </div>
</template>

<style scoped>
#app {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: flex-start;
  width: 100%;
  height: 100vh;
}

.navbar {
  width: 100%;
}
</style>

