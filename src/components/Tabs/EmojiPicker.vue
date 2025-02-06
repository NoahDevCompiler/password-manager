<script setup>
import { ref, computed } from "vue";
import { ChevronDownIcon } from "@heroicons/vue/24/outline";
import EmojiPicker from "vue3-emoji-picker";
import "vue3-emoji-picker/css";

const emojis = ref(["😀", "😂", "🔥", "❤️"]);
const sequence = ref([]);
const selectedEmoji = ref(null);
const showPicker = ref(0);
const selectedColors = ref(Array(emojis.value.length).fill("bg-[#4A4A4A]"));

const selectEmoji = (emoji, index) => {
  const existingIndex = sequence.value.findIndex((e) => e.emoji === emoji);

  if (existingIndex === -1) {
    sequence.value.push({
      emoji,
      order: sequence.value.length + 1,
    });
  } else {
    sequence.value.splice(existingIndex, 1);
  }
  sequence.value.forEach((e, i) => {
    e.order = i + 1;
  });
  console.log(sequence.value);

  selectedEmoji.value = index;
  selectedColors.value[index] =
    selectedColors.value[index] === "bg-[#4A4A4A]"
      ? "bg-[#949D6A]"
      : "bg-[#4A4A4A]";
};

const changeEmoji = (index, newEmoji) => {
  const oldEmoji = emojis.value[index];
  console.log("OldEmoji:", oldEmoji);
  emojis.value[index] = newEmoji;
  if (sequence.value[index]) {
    sequence.value[index].emoji = newEmoji;
  }
  showPicker.value = null;
};

const togglePicker = (index) => {
  showPicker.value = showPicker.value === index ? null : index;
};


</script>

<template>
  <div class="flex justify-center items-center h-300">
    <div class="grid grid-cols-2 gap-4 p-4 bg-[#4a4a4a] rounded-lg shadow-md">
      <div
        v-for="(emoji, index) in emojis"
        :key="index"
        class="relative w-64 h-64 flex justify-center items-center rounded-lg cursor-pointer hover:scale-105 transition"
        :class="selectedColors[index]"
        @click="selectEmoji(emoji, index)"
      >
        <span class="justify-center text-9xl text-white">{{ emoji }}</span>

        <span
          class="absolute top-0 right-0 text-white text-xs font-bold rounded-full px-2"
        >
          {{ sequence.find((e) => e.emoji === emoji)?.order }}
        </span>

        <button @click.stop="togglePicker(index)">
          <ChevronDownIcon
            class="size-6 transition-transform duration-500"
            :class="showPicker === index ? 'rotate-180' : 'rotate-0 '"
          />
        </button>

        <EmojiPicker
          v-if="showPicker === index"
          @select="(emoji) => changeEmoji(index, emoji.i)"
          class="absolute"
        />
      </div>
    </div>
    <div>
    </div>
  </div>
</template>
  

  