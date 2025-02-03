<script setup>
import { ChevronDownIcon, TrashIcon } from "@heroicons/vue/24/outline";
import { ref, computed, onMounted, onUnmounted } from "vue";

const availableOptions = ref([]);
const selectedOption = ref("");
const searchTerm = ref("");
const showList = ref(false);
const boxContainer = ref(null);
const searchInput = ref(null);

const emit = defineEmits([
  "update:AvailableOptions",
  "update:SelectedOption",
]);

function selectOption(option) {
  selectedOption.value = option;
  emit("update:SelectedOption", option);
  searchTerm.value = "";
  showList.value = false;
}

const filteredOptions = computed(() => {
  return availableOptions.value.filter((option) =>
    option.toLowerCase().startsWith(searchTerm.value.toLowerCase())
  );
});

function addOption() {
  if (
    searchTerm.value &&
    !availableOptions.value.includes(searchTerm.value.trim())
  ) {
    availableOptions.value.push(searchTerm.value.trim());
    emit("update:AvailableOptions", availableOptions.value);
    emit("update:SelectedOption", searchTerm.value.trim());
    selectOption(searchTerm.value.trim());
  }
}

function deleteOption(option) {
  availableOptions.value.splice(option, 1);
  selectedOption.value = "";
  emit("update:AvailableOptions", availableOptions.value);
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
  <div ref="boxContainer" class="relative w-full" aria-haspopup="listbox">
    <button
      @click="showList = !showList"
      class="relative w-full rounded-lg border justify-center border-slate-300 bg-white px-3 py-2 dark:border-slate-600 dark:bg-[#4A4A4A]"
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
      class="relative z-10 mt-1 w-full overflow-hidden rounded-lg border border-slate-300 bg-white shadow-md dark:border-slate-600 dark:bg-[#423E37]"
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
        class="w-full rounded-t-lg border-b bg-white px-3 py-2 focus:border-[#949D6A] focus:outline-none dark:border-slate-600 dark:bg-[#4A4A4A]"
        role="searchbox"
      />
      <div class="combo-box-scrollbar max-h-40 overflow-y-auto">
        <div
          v-for="option in filteredOptions"
          :key="option"
          @click="selectOption(option)"
          class="flex items-center cursor-pointer px-3 py-2 hover:bg-[#949D6A] hover:text-white focus:outline-none focus:ring-2 focus:ring-orange-500"
          role="option"
          :aria-selected="selectedOption === option"
          tabindex="0"
          @keypress.enter="selectOption(option)"
        >
          <span>{{ option }}</span>
          <button
            @click.stop="deleteOption(option)"
            class="ml-auto text-red-500 hover:text-red-700"
            aria-label="Option löschen"
          >
            <TrashIcon class="w-5 h-5" />
          </button>
        </div>
      </div>
    </div>
  </div>
</template>