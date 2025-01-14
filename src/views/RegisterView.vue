<script setup>
import { ClipboardDocumentCheckIcon } from '@heroicons/vue/24/outline';
import { ref, computed} from 'vue';
import { useRouter } from 'vue-router';
import {register} from '@/api/requests.js'
import { ElMessage, ElMessageBox } from 'element-plus'
import TFASetup from '@/components/TFASetup.vue'

const username = ref("");
const password = ref("");
const password2 = ref("");
const router = useRouter();
const tfasetup = ref(false)

const confirmValidation = async () => {
  if(isPasswordValid.value){
    tfasetup.value = true
    registerUser()
  } else{
    ElMessageBox.confirm(
    'Verwende bitte ein sicheres Passwort ',
    'Min. 12 Zeichen, Verwende Zahlen, Sonderzeichen und Gross-/Kleinbuchstaben',
    {
      confirmButtonText: 'OK',
      type: 'warning',
    }
  )
    .catch(() => {
      ElMessage({
        type: 'info',
        message: 'Delete canceled',
      })
    })
  } 
}

const checkPassword = () => {
  return password.value === password2.value;
};

const isPasswordValid = computed(() => {
  const passwordValue = password.value;
  const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{12,}$/;
  console.log(regex.test(passwordValue))
  return regex.test(passwordValue)
});

async function registerUser() {
  try {
    console.log("huiasdfiuhafiasdfiuhd", password.value);
    await register(username.value, password.value);
    await router.push('/pwmanager');
  } catch (exception) {
    console.error('login error', exception);
    ElMessage.error(`${exception}`);
  }
}
</script>

<template>
  <div class="master relative">

    <h1 class="text-3xl font-bold tracking-tight flex items-center justify-center mb-10 text-[#E0D8DE]">
      Registrierung
    </h1>

    <div class="flex flex-col items-center justify-center lg:py-0">
      <a href="#" class="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-[#E0D8DE]">
        <img class="w-50 h-8 mr-2" src="@/assets/logo.png" alt="logo">
      </a>

      <div class=" bg-white rounded-lg xl:px-7 dark:bg-[#4A4A4A] dark:border-gray-700">
        <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 class="text-xl font-bold leading-tight tracking-tight flex justify-center items-center text-gray-900 md:text-2xl dark:text-white">
            Account Erstellen
          </h1>
          <form class="space-y-4 md:space-y-6" @submit.prevent="confirmValidation">
            <div>
              <label for="email" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">
                Benutzername
              </label>
              <input type="username" name="username" id="email" v-model="username"
                class="bg-gray-50 border border-gray-300 text-[#E0D8DE] text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-[#4A4A4A] dark:placeholder-gray-400"
                placeholder="UnknownUser" required>
            </div>
            <div class="max-w-50">
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

            <button type="submit" 
              :disabled="!checkPassword()"
              :class="{
                'text-gray-400': !checkPassword(),
                'text-white' : checkPassword()
              }"
              class="hover:animate-none animate-bounce w-full bg-blue-500 hover:bg-blue-70 font-medium rounded-lg text-sm px-5 py-2.5 text-center"
              onsubmit="confirmValidation()">
              Account erstellen
            </button>

            <p class="text-sm font-light text-gray-500 dark:text-gray-400">
              Bereits ein Account?
              <RouterLink to  ="/login" class="font-medium text-primary-600 hover:underline dark:text-primary-500">
                Login
              </RouterLink>
            </p>
          </form>         
        </div>       
      </div>
    </div>
    <div v-if="tfasetup" id="TFASetup" class="fixed right-5 top-1/4 w-1/3">
      <TFASetup/>
    </div>
  </div>
</template>
