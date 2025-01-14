<script>
import { RouterLink } from 'vue-router';

</script>

<template>
  <div class="absolute top-10 left-10 w-full h-full flex items-start">
    <div class="mr-10">
      <h1 class="font-source-code text-9xl my-9">Passwort Management-Tool</h1>
      <p class="text-xl">
        Das ist Mein Passwort-Manager mit einem sicheren
        Passwort-Generierungstool. Bitte beachten Sie, dass das Tool nicht
        regelmäßig gewartet wird. Daher wird empfohlen, generierte Passwörter
        vor der Verwendung auf ihre Sicherheit zu überprüfen.
      </p>
    </div>
    <RouterLink to="/register"
      class="bg-blue-500 hover:bg-blue-700 text-8xl text-center font-source-code px-10 py-10 my-auto rounded-full text-white "
    >
      Jetzt Registrieren
    </RouterLink>
  </div>
</template>