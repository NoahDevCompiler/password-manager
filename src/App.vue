<script setup>
import { RouterLink, RouterView } from "vue-router";
import { ref, onMounted, onBeforeUnmount } from "vue";
import NavBar from "./components/NavBar.vue";
import Particles from "./components/Particles.vue";


let navbarPosition = 0; 
let lastScrollTop = 0; 

const handleScroll = () => {
  const currentScrollTop = document.documentElement.scrollTop;

  if (currentScrollTop > lastScrollTop) {
    navbarPosition = Math.min(-50, navbarPosition - 5);
  } else {
    navbarPosition = Math.max(0, navbarPosition);
  }

  lastScrollTop = currentScrollTop <= 0 ? 0 : currentScrollTop;

  const navbar = document.querySelector(".navbar");
  if (navbar) {
    navbar.style.transform = `translateY(${navbarPosition}px)`;
  }
};

window.addEventListener("scroll", handleScroll);

</script>


<template>
  <div id="app">
    <NavBar class="navbar"
    :style="{ transform: `translateY(${navbarPosition}px)`}"
    />
    <router-view class="content-container"/>
    <div class="particles">
      <Particles />
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
.content-container {
  padding-top: 80px;
  max-width: 800px;
  z-index: 1;
}

.navbar {
  position: fixed;
  z-index: 1000;
  width: 100%;
  height: 80px;
  transition: transform 0.2s ease-out;
}

.particles {
  position: absolute;
  top: 10;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 0;
}
</style>

