<script setup>
import { ClipboardDocumentCheckIcon } from "@heroicons/vue/24/outline";
import { ref } from "vue";
import { login } from "@/api/requests.js";
import TFASetup from "@/components/TFASetup.vue";
import { useRouter } from "vue-router";

const router = useRouter();
const username = ref("");
const password = ref("");
const errors = ref({
  username: "",
  password: "",
});

async function loginUser() {
  try {
    console.log("huiasdfiuhafiasdfiuhd", password.value);
    await login(username.value, password.value);
    await router.push("/pwmanager");
  } catch (exception) {
    const errorMessage = exception.message;
    errors.value = errorMessage;
    console.error("Unerwarteter Fehler:", exception);
    console.log(errors.password, errors.username);
    console.log(errorMessage);
  }
}
</script>

<template>
  <div class="master">
    <div class="absolute right-0 w-1/6">
      <el-alert title="Falsches Passwort" type="error" show-icon />
    </div>
    <h1
      class="text-3xl font-bold tracking-tight flex items-center justify-center mb-10 text-[#E0D8DE]"
    >
      Anmelden
    </h1>

    <div class="flex flex-col items-center">
      <RouterLink
        to="/"
        class="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-[#E0D8DE]"
      >
        <img class="w-50 h-8 mr-2" src="@/assets/logo.png" alt="logo" />
      </RouterLink>

      <div
        class="bg-white rounded-lg xl:px-5 dark:bg-[#4A4A4A] dark:border-gray-700 w-96 h-96 "
      >
        <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1
            class="text-xl font-bold leading-tight items-center justify-center flex tracking-tight text-gray-900 md:text-2xl dark:text-white"
          >
            Anmelden
          </h1>
          <form class="space-y-4 md:space-y-6" @submit.prevent="loginUser">
            <div>
              <label
                for="email"
                class="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Benutzername
              </label>
              <el-input
                type="username"
                name="username"
                v-model="username"
                class="text-[#ffffff] "
                placeholder="UnknownUser"
                required
              />
            </div>
            <div>
              <label
                for="password"
                class="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
              >
                Master Passwort
              </label>
              <el-input
                type="password"
                name="password"
                placeholder="••••••••"
                v-model="password"
                class="text-[#E0D8DE]"
                required
                show-password
              />
            </div>
            <button
              type="submit"
              class="hover:animate-none text-white animate-bounce w-full bg-blue-500 hover:bg-blue-70 font-medium rounded-lg text-sm px-5 py-2.5 text-center"
            >
              Login
            </button>
            <p class="text-sm font-light text-gray-500 dark:text-gray-400">
              Noch kein Account?
              <RouterLink
                to="/register"
                class="font-medium text-primary-600 hover:underline dark:text-primary-500"
              >
                Registrieren
              </RouterLink>
            </p>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.el-input :deep(.el-input__inner){
    height: 40px;
    color: azure;
}

.el-input :deep(.el-input__wrapper){
    align-items: center;
    background-color: transparent;


}
</style>