<script setup>
import { RouterLink, RouterView } from 'vue-router'
import NavBar from './components/NavBar.vue';
import Particles from './components/Particles.vue'
</script>


<template>
  <div id="app">
    <NavBar class="navbar"/>
    <nav>
      <RouterLink to="/"/>
    </nav>
    <router-view class="content-container" />
    <div class="particles">
        <Particles/>
      </div>
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
.content-container{
  max-width: 800px;
  min-height: 100vh;
  z-index: 1;
  position:relative;
}
.navbar {
  z-index: 2;
  position:relative;
  width: 100%;
}
.particles
{
  position: absolute;
  top: 10;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 0;
}
</style>

