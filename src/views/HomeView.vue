<script>
import { RouterLink } from "vue-router";
</script>

<template>
  <div class="absolute top-10 w-full h-full flex items-start flex-col">
    <div class="mr-10">
      <h1 class="font-source-code text-9xl my-9">Passwort Management-Tool</h1>
      <p class="text-xl">
        Das ist Mein Passwort-Manager mit einem sicheren
        Passwort-Generierungstool. Bitte beachten Sie, dass das Tool nicht
        regelmäßig gewartet wird. Daher wird empfohlen, generierte Passwörter
        vor der Verwendung auf ihre Sicherheit zu überprüfen.
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
