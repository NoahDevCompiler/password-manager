<script setup>
import { ref } from "vue";
import { ChevronDownIcon } from "@heroicons/vue/24/outline";
import EmojiPicker from "vue3-emoji-picker";
import "vue3-emoji-picker/css";

const emojis = ref(["😀", "😂", "🔥", "❤️"]);
const sequence = ref([""]);
const selectedEmoji = ref(null);
const showPicker = ref(false);

const selectedColors = ref(Array(emojis.value.length).fill("bg-[#4A4A4A]"));

const selectEmoji = (index) => {
  addSequence(index);
  selectedEmoji.value = index;
  selectedColors.value[index] =
    selectedColors.value[index] === "bg-[#4A4A4A]"
      ? "bg-[#25d3ff8a]"
      : "bg-[#4A4A4A]";
};

const changeEmoji = (index, newEmoji) => {
  emojis.value[index] = newEmoji;
};

const togglePicker = () => {
  showPicker.value = !showPicker.value;
};

function addSequence(index) {
  if (!sequence.value.includes(index)) {
    sequence.value.push(index);
    console.log(sequence.value);

  } else {
    sequence.value.splice(index);
    console.log(sequence.value)
  }
}
</script>


<template>
  <div class="flex justify-center items-center h-300">
    <div class="grid grid-cols-2 gap-4 p-4 bg-[#4a4a4a] rounded-lg shadow-md">
      <div
        v-for="(emoji, index) in emojis"
        :key="index"
        class="relative w-64 h-64 flex justify-center items-center rounded-lg cursor-pointer hover:scale-105 transition"
        :class="selectedColors[index]"
        @click="selectEmoji(index)"
      >
        <span class="justify-center text-9xl text-white">{{ emoji }}</span>

        <span
          v-if="selectedEmoji === index"
          class="absolute top-0 right-0 text-white text-xs font-bold rounded-full px-2"
        >
          {{ index + 1 }}
        </span>
        <button @click="togglePicker">
          <ChevronDownIcon
            class="size-6 transition-transform duration-500"
            :class="showPicker ? 'rotate-180' : 'rotate-0 '"
          />
        </button>
      </div>
    </div>
    <EmojiPicker v-if="showPicker"></EmojiPicker>
  </div>
</template>
  

  