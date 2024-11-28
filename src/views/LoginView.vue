<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const email = ref("");
const password = ref("");
const password2 = ref("");
const router = useRouter();

const checkPassword = () => {
  return password.value === password2.value;
};

async function registerUser() {
  try {
    console.log("huiasdfiuhafiasdfiuhd", password.value);
    await login(email.value, password.value);
    await router.push('/pwmanager');
  } catch (exception) {
    console.error('login error', exception);
    errors.value = exception.errors;
  }
}
</script>

<template>
  <div class="master">
    <h1 class="text-3xl font-bold tracking-tight flex items-center justify-center mb-10 text-[#E0D8DE]">
      Registrierung
    </h1>

    <div class="flex flex-col items-center justify-center lg:py-0">
      <a href="#" class="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-[#E0D8DE]">
        <img class="w-50 h-8 mr-2" src="@/assets/logo.png" alt="logo">
      </a>

      <div class="w-full bg-white rounded-lg md:mt-0 sm:max-w-md xl:p-0 dark:bg-[#423E37] dark:border-gray-700">
        <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Account Erstellen
          </h1>
          <form class="space-y-4 md:space-y-6" @submit.prevent="registerUser">
            <div>
              <label for="email" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
                E-Mail
              </label>
              <input type="email" name="email" id="email" v-model="email"
                class="bg-gray-50 border border-gray-300 text-[#E0D8DE] text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-[#4A4A4A] dark:placeholder-gray-400"
                placeholder="name@company.com" required>
            </div>
            <div>
              <label for="password" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
                Master Passwort
              </label>
              <input type="password" name="password" id="password" placeholder="••••••••" v-model="password"
                class="bg-gray-50 border border-gray-300 text-[#E0D8DE] text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-[#4A4A4A] dark:placeholder-gray-400"
                required>
            </div>
            <div>
              <label for="confirm-password" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
                Bestätige Passwort
              </label>
              <input type="password" name="confirm-password" id="confirm-password" v-model="password2"
                placeholder="••••••••"
                class="bg-gray-50 border border-gray-300 text-[#E0D8DE] text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-[#4A4A4A] dark:placeholder-gray-400"
                required>
            </div>

            <div class="flex items-start">
              <div class="flex items-center h-5">
                <input id="terms" aria-describedby="terms" type="checkbox"
                  class="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-primary-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-primary-600 dark:ring-offset-gray-800"
                  required>
              </div>
              <div class="ml-3 text-sm">
                <label for="terms" class="font-light text-gray-500 dark:text-gray-300">
                  Ich akzeptiere die
                  <a class="font-medium text-primary-600 hover:underline dark:text-primary-500" href="#">
                    Nutzungsbedingungen
                  </a>
                </label>
              </div>
            </div>

            <button type="submit" :disabled="!checkPassword()"
              class="w-full text-[#E0D8DE] hover:focus:ring-4 focus:outline-none font-medium rounded-lg text-sm px-5 py-2.5 text-center">
              Account erstellen
            </button>

            <p class="text-sm font-light text-gray-500 dark:text-gray-400">
              Bereits ein Account?
              <a href="#" class="font-medium text-primary-600 hover:underline dark:text-primary-500">
                Login
              </a>
            </p>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>
