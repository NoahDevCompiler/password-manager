<script setup>
import { ref, computed, onMounted, onUnmounted } from 'vue';
import { ChevronDownIcon, FolderIcon } from '@heroicons/vue/24/outline';


//PasswordInput
const passwordName = ref("");

const submitPassword = () => {
  console.log(passwordName.value);
};

//Combobox
const availableOptions = ref([]);
const selectedOption = ref("");
const searchTerm = ref("");
const showList = ref(false);
const boxContainer = ref(null);
const searchInput = ref(null);

function selectOption(option) {
  selectedOption.value = option;
  searchTerm.value = "";
  showList.value = false;
}

const filteredOptions = computed(() => {
  return availableOptions.value.filter((option) =>
    option.toLowerCase().startsWith(searchTerm.value.toLowerCase()),
  );
});

function addOption() {
  if (
    searchTerm.value &&
    !availableOptions.value.includes(searchTerm.value.trim())
  ) {
    availableOptions.value.push(searchTerm.value.trim());
    selectOption(searchTerm.value.trim());
  }
}

function closeDropdown(event) {
  if (
    !boxContainer.value.contains(event.target) &&
    !searchInput.value.contains(event.target)
  ) {
    searchTerm.value = "";
    showList.value = false;
  }
}

onMounted(() => {
  window.addEventListener("click", closeDropdown);
});

onUnmounted(() => {
  window.removeEventListener("click", closeDropdown);
});
</script>

<template>
  <div class="main">
    <div class="space-y-3">
      <h1 class="text-3xl font-bold tracking-tight text-[#E0D8DE]">
        Passwort Generieren
      </h1>
      <div>
        <div
          class="sm:flex rounded-lg shadow-sm"
          id="textbox"
        >
          <span
            class="py-3 px-4 inline-flex items-center min-w-fit w-full border border-gray-200 bg-gray-50 text-sm text-gray-500 -mt-px -ms-px first:rounded-t-lg last:rounded-b-lg sm:w-auto sm:first:rounded-s-lg sm:mt-0 sm:first:ms-0 sm:first:rounded-se-none sm:last:rounded-es-none sm:last:rounded-e-lg dark:bg-[#423E37] dark:border-neutral-700 dark:text-[#E0D8DE]"
          >
            Name des Passworts
          </span>
          <input
            type="text"
            v-model="passwordName"
            @keydown.enter="submitPassword"
            class="py-3 px-4 pe-11 block w-full border-gray-200 shadow-sm -mt-px -ms-px first:rounded-t-lg last:rounded-b-lg sm:first:rounded-s-lg sm:mt-0 sm:first:ms-0 sm:first:rounded-se-none sm:last:rounded-es-none sm:last:rounded-e-lg text-sm relative focus:z-10 focus:border-blue-500 focus:ring-blue-500 disabled:opacity-50 disabled:pointer-events-none dark:bg-[#4A4A4A] dark:border-neutral-700 dark:text-[#E0D8DE] dark:placeholder-neutral-500 dark:focus:ring-neutral-600"
          />
        </div>
      </div>
    </div>

    <h2 class="my-5 inline-flex items-center w-full">
      Art bzw. Konto hinzufügen
    </h2>

    <div ref="boxContainer" class="relative w-full" aria-haspopup="listbox">

      <button
        @click="showList = !showList"
        class="relative w-full rounded-lg border justify-center border-slate-300 bg-white px-3 py-2 focus:border-0 focus:outline-none focus:ring-2 focus:ring-orange-500 dark:border-slate-600 dark:bg-slate-900"
        aria-expanded="showList"
        aria-controls="option-dropdown"
        aria-labelledby="dropdown-label"
      >
        <span id="dropdown-label">{{
          selectedOption || "Wähle oder Füge eine Option hinzu"
        }}</span>
        <span
          class="absolute right-3 top-1/2 -translate-y-1/2"
          aria-hidden="true"
        >
          <ChevronDownIcon
            class="size-6 transition-transform duration-500"
            :class="showList ? 'rotate-180' : 'rotate-0 '"
          />
        </span>
      </button>


      <div
        v-show="showList"
        class="absolute z-10 mt-1 w-full overflow-hidden rounded-lg border border-slate-300 bg-white shadow-md dark:border-slate-600 dark:bg-slate-900"
        id="genre-dropdown"
        role="listbox"
        aria-labelledby="dropdown-label"
        tabindex="-1"
      >
        <input
          type="text"
          v-model="searchTerm"
          ref="searchInput"
          @keydown.enter.prevent="addOption"
          placeholder="Search Option"
          class="w-full rounded-t-lg border-b bg-white px-3 py-2 focus:border-orange-400 focus:outline-none dark:border-slate-600 dark:bg-slate-900"
          role="searchbox"
        />
        <div class="combo-box-scrollbar max-h-40 overflow-y-auto">
          <div
            v-for="option in filteredOptions"
            :key="option"
            @click="selectOption(option)"
            class="cursor-pointer px-3 py-2 hover:bg-orange-500 hover:text-white focus:outline-none focus:ring-2 focus:ring-orange-500"
            role="option"
            :aria-selected="selectedOption === option"
            tabindex="0"
            @keypress.enter="selectOption(option)"
          >
            {{ option }}
            <button
              @click.stop="deleteOption(option)"
              class=" mx-11 justify-between text-red-500 hover:text-red-700"
              aria-label="Option löschen"
            >
            <FolderIcon class="w-5 h-5" />
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
#textbox{
  margin-top: 20px;
}
</style>