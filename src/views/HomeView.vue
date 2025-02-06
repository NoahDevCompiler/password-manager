<script>
import { RouterLink } from "vue-router";
</script>

<template>
  <div class="absolute top-10 w-full h-full flex items-start flex-col">
    <div class="mr-10">
      <h1 class="font-source-code text-9xl my-9">Passwort Management-Tool</h1>
      <p class="text-xl">
        I always wanted simple passwords that i can remember, but their just not save, so i came up with the idea of a deterministic password-generator.
        I know its not the most secure solution, so i wouldnt recommend using it to use on account with really critical data. 
        I'm still working on a concept of save deterministic password generation.
      </p>
    </div>
    <RouterLink
      to="/register"
      class="relative group text-8xl text-center font-source-code px-10 py-10 my-auto rounded-full text-white bg-[#423E37] overflow-clip transition-all duration-300 ease-in-out"
    >
      <span class="relative z-10">Jetzt Registrieren</span>

      <div
        class="absolute inset-0 bg-[#949D6A] translate-y-full group-hover:translate-y-0 transition-transform duration-500 ease-in-out"
      ></div>
    </RouterLink>
  </div>
</template>
