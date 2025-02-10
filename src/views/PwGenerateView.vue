<script setup>
import { ref, computed, onMounted, onUnmounted } from "vue";
import Services from "@/components/Tabs/Services.vue";
import Configuration from "@/components/Tabs/Configuration.vue";
import EmojiPicker from "@/components/Tabs/EmojiPicker.vue";
import gsap from "gsap";
import { TextPlugin } from "gsap/TextPlugin";

const passwordName = ref("");
const activeName = ref("first");

const availableOptions = ref([]);
const selectedOption = ref("");

const passwortstate = ref("persists");

const submitPassword = () => {
  animateText();
  window.scroll({
    top: 1000,
    behavior: "smooth",
  });
  console.log(passwordName.value);
  console.log(selectedOption.value);
  console.log(availableOptions.value);
};

function animateText() {
  const element = document.getElementById("scramblepassword");
  const letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  let interval = null;
  const value = "P@ssw0rd";
  let iteration = 0;

  interval = setInterval(() => {
    let scrambled = value
      .split("")
      .map((letter, index) => {
        if (index < iteration) {
          return value[index];
        }
        return letters[Math.floor(Math.random() * 26)];
      })
      .join("");

    element.innerText = scrambled;

    if (iteration >= value.length) {
      clearInterval(interval);
    }
    iteration += 1 / 5;
  }, 30);

  /*gsap.to(textElement, {
    duration: 3,
    text: newText,
    ease: Power1.easeIn,
  });*/
}
</script>

<template>
  <div class="my-10 width-full">
    <div class="space-y-3">
      <h1
        class="text-3xl font-bold tracking-tight flex items-center justify-center mb-10 text-[#E0D8DE]"
      >
        Passwort Generieren
      </h1>
      <div>
        <h2 class="flex justify-center items-center w-full mb-2">Passwort</h2>
        <div
          class="flex rounded-lg shadow-sm items-center justify-center"
          id="textbox"
        >
          <input
            type="text"
            v-model="passwordName"
            @keydown.enter="submitPassword"
            class="py-3 mb-10 block w-96 text-center text-xl border-gray-200 rounded-full dark:bg-[#4A4A4A] dark:text-[#E0D8DE]"
          />
        </div>
      </div>
    </div>

    <div>
      <el-tabs v-model="activeName" class="tabs">
        <el-tab-pane label="Konfiguration" name="first">
          <Configuration />
        </el-tab-pane>
        <el-tab-pane label="Dienst" name="second">
          <Services
            v-model:AvailableOptions="availableOptions"
            v-model:SelectedOption="selectedOption"
          />
        </el-tab-pane>
        <el-tab-pane label="Zusätzliche Sicherung" name="third">
          <p class="break-normal">Wähle Eine Reihenfolge dieser Emotes aus</p>
          <p class="font-source-code my-4 tracking-tight text-white">
            Bitte merke dir diese Sequenz gut, ohne kannst du dein Passwort
            nicht wieder sehen ):
          </p>
          <EmojiPicker />
        </el-tab-pane>
      </el-tabs>
    </div>
    <div
      class="flex flex-col overflow-hidden items-center justify-center my-10"
    >
      <el-radio-group v-model="passwortstate" class="mb-10">
        <el-radio value="persists">Passwort Speichern</el-radio>
        <el-radio value="zerostate">Ohne Speicherung Generieren</el-radio>
      </el-radio-group>
      <button
        class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-full"
        @click="submitPassword"
      >
        Generieren
      </button>
    </div>
    <div class="max-w-sm mx-auto">
      <div
        class="pt-10 justify-center items-center flex text-3xl font-source-code break-words"
      >
        <p>Generiertes Passwort:</p>
      </div>
      <div
        id="text"
        class="justify-center items-center flex text-4xl pt-11 text-red-500 pb-20"
      >
        <p id="scramblepassword"></p>
      </div>
    </div>
  </div>
</template>

<style>
.el-tab-pane {
  width: 100%;
  overflow: visible;
}
.el-tabs .el-tabs__item.is-active {
  font-weight: bold;
  color: #0084ff;
}
.el-tabs .el-tabs__item {
  color: white;
}
</style>