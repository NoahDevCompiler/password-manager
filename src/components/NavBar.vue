<script setup>
import { ref, computed } from "vue";
import { RouterLink } from "vue-router";
import { useToken } from "@/api/auth.js";
import {
  Dialog,
  DialogPanel,
  Disclosure,
  DisclosureButton,
  DisclosurePanel,
  Popover,
  PopoverButton,
  PopoverGroup,
  PopoverPanel,
} from "@headlessui/vue";
import {
  LockOpenIcon,
  Bars3Icon,
  ChartPieIcon,
  CursorArrowRaysIcon,
  FingerPrintIcon,
  SquaresPlusIcon,
  CogIcon,
} from "@heroicons/vue/24/outline";
import {
  ChevronDownIcon,
  PhoneIcon,
  PlayCircleIcon,
} from "@heroicons/vue/20/solid";
import { Avatar } from "@element-plus/icons-vue";

const popoverOpen = ref(false);

const { token, clearToken, username } = useToken();

function closePopover() {
  console.log("Popover wird geschlossen");
  popoverOpen.value = false;
}
function openPopover() {
  popoverOpen.value = true;
}

const products = [
  {
    name: "Passwort Verwaltung",
    description: "Verwalte deine erstellten Passwörter",
    href: "/pwmanager",
    icon: LockOpenIcon,
  },
  {
    name: "Passwort Generieren",
    description: "Generiere ein neues Passwort",
    href: "/pwgenerate",
    icon: CursorArrowRaysIcon,
  },
  {
    name: "Sicherheitsbewertung",
    description: "Prüfe wie Sicher deine Passwörter sind",
    href: "#",
    icon: FingerPrintIcon,
  },
  {
    name: "Einstellungen",
    description: "Passe dein Overlay an",
    href: "#",
    icon: CogIcon,
  },
];
const callsToAction = [
  { name: "Versuch es", href: "#", icon: PlayCircleIcon },
  { name: "Kontakt", href: "#", icon: PhoneIcon },
];

const mobileMenuOpen = ref(false);
</script>

<template>
  <header class="fixed bg-transparen">
    <nav
      class="mx-auto flex w-full items-center justify-between p-6 lg:px-8"
      aria-label="Global"
    >
      <div class="flex lg:flex-1">
        <a href="#" class="-m-1.5 p-1.5">
          <span class="sr-only">Passwort Manager</span>
          <img class="h-8 w-auto" src="../assets/logo.png" />
        </a>
      </div>
      <div class="flex lg:hidden">
        <button
          type="button"
          class="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-700"
          @click="mobileMenuOpen = true"
        >
          <span class="sr-only">Open main menu</span>
          <Bars3Icon class="size-6" aria-hidden="true" />
        </button>
      </div>
      <PopoverGroup class="hidden lg:flex lg:gap-x-12">
        <Popover class="relative">
          <PopoverButton
            class="flex items-center gap-x-1 text-sm/6 font-semibold text-[#E0D8DE] hover:text-[#949D6A]"
            @click="openPopover"
          >
            Dashboard
            <ChevronDownIcon
              class="size-5 flex-none text-gray-400"
              aria-hidden="true"
            />
          </PopoverButton>

          <transition
            enter-active-class="transition ease-out duration-200"
            enter-from-class="opacity-0 translate-y-1"
            enter-to-class="opacity-100 translate-y-0"
            leave-active-class="transition ease-in duration-150"
            leave-from-class="opacity-100 translate-y-0"
            leave-to-class="opacity-0 translate-y-1"
          >
            <PopoverPanel
              v-if="popoverOpen"
              class="absolute -left-8 top-full z-10 mt-3 w-screen max-w-md overflow-hidden rounded-3xl bg-[#1a1a1a] shadow-lg ring-1 ring-gray-900/5"
            >
              <div class="p-4">
                <div
                  v-for="item in products"
                  :key="item.name"
                  class="group relative flex items-center gap-x-6 rounded-lg p-4 text-sm/6 hover:bg-[#423E37]"
                >
                  <div
                    class="flex size-11 flex-none items-center justify-center rounded-lg bg-gray-50 group-hover:bg-white"
                  >
                    <component
                      :is="item.icon"
                      class="size-6 text-gray-600 group-hover:text-indigo-600"
                      aria-hidden="true"
                    />
                  </div>
                  <div class="flex-auto">
                    <router-link
                      v-if="item.href"
                      :to="item.href"
                      class="block font-semibold text-[#949D6A]"
                      @click="closePopover"
                    >
                      {{ item.name }}
                    </router-link>
                    <p class="mt-1 text-[#E0D8DE]">{{ item.description }}</p>
                  </div>
                </div>
              </div>
              <div
                class="grid grid-cols-2 divide-x divide-gray-900/5 bg-gray-50"
              >
                <a
                  v-for="item in callsToAction"
                  :key="item.name"
                  :href="item.href"
                  class="flex items-center justify-center gap-x-2.5 p-3 text-sm/6 font-semibold text-gray-900 hover:bg-gray-100"
                >
                  <component
                    :is="item.icon"
                    class="size-5 flex-none text-gray-400"
                    aria-hidden="true"
                  />
                  {{ item.name }}
                </a>
              </div>
            </PopoverPanel>
          </transition>
        </Popover>

        <a href="#" class="text-[#E0D8DE] hover:text-[#949D6A] font-semibold"
          >Features</a
        >
        <a href="#" class="text-[#E0D8DE] hover:text-[#949D6A] font-semibold"
          >Marketplace</a
        >
        <a href="#" class="text-[#E0D8DE] hover:text-[#949D6A] font-semibold"
          >Company</a
        >
      </PopoverGroup>
      <div class="hidden lg:flex lg:flex-1 lg:justify-end">
        <template v-if="token">
          <Popover>
            <PopoverButton
              class="flex items-center gap-x-1 text-sm/6 font-semibold text-[#E0D8DE] hover:text-[#949D6A]"
            >
              <el-icon color="#FFFFFF" :size="20">
                <Avatar />
              </el-icon>
              <p>{{ username }}</p>
              <ChevronDownIcon
                class="size-5 flex-none text-gray-400"
                aria-hidden="true"
              />
            </PopoverButton>
            <PopoverPanel
              class="absolute my-3 rounded-3xl bg-[#1a1a1a] shadow-lg ring-1 ring-gray-900/5"
            >
              <div>
                <a href="#" @click="clearToken">Abmelden</a>
              </div>
            </PopoverPanel>
          </Popover>
        </template>
        <template v-else>
          <RouterLink
            to="/login"
            class="text-sm/6 font-semibold text-[#E0D8DE] hover:text-[#949D6A]"
          >
            Log in <span aria-hidden="true">&rarr;</span>
          </RouterLink>
        </template>
      </div>
    </nav>
    <Dialog
      class="lg:hidden"
      @close="mobileMenuOpen = false"
      :open="mobileMenuOpen"
    >
      <div class="fixed inset-0 z-10" />
      <DialogPanel
        class="fixed inset-y-0 right-0 z-10 w-full overflow-y-auto bg-white px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10"
      >
        <div class="flex items-center justify-between">
          <a href="#" class="-m-1.5 p-1.5">
            <span class="sr-only">Your Company</span>
            <img
              class="h-8 w-auto"
              src="https://tailwindui.com/plus/img/logos/mark.svg?color=indigo&shade=600"
              alt=""
            />
          </a>
          <button
            type="button"
            class="-m-2.5 rounded-md p-2.5 text-[#949D6A]"
            @click="mobileMenuOpen = false"
          >
            <span class="sr-only">Close menu</span>
            <XMarkIcon class="size-6" aria-hidden="true" />
          </button>
        </div>
        <div class="mt-6 flow-root">
          <div class="-my-6 divide-y">
            <div class="space-y-2 py-6">
              <Disclosure as="div" class="-mx-3" v-slot="{ open }">
                <DisclosureButton
                  class="flex w-full items-center justify-between rounded-lg py-2 pl-3 pr-3.5 text-base/7 font-semibold text-[#E0D8DE] hover:[#949D6A]"
                >
                  Product
                  <ChevronDownIcon
                    :class="[open ? 'rotate-180' : '', 'size-5 flex-none']"
                    aria-hidden="true"
                  />
                </DisclosureButton>
                <DisclosurePanel class="mt-2 space-y-2">
                  <DisclosureButton
                    v-for="item in [...products, ...callsToAction]"
                    :key="item.name"
                    as="a"
                    :href="item.href"
                    class="block rounded-lg py-2 pl-6 pr-3 text-sm/7 font-semibold text-[#ffffff] hover:text-[#949D6A]"
                    >{{ item.name }}</DisclosureButton
                  >
                </DisclosurePanel>
              </Disclosure>
            </div>
            <div class="py-6">
              <RouterLink
                to="/login"
                class="-mx-3 block rounded-lg px-3 py-2.5 text-base/7 font-semibold text-[#E0D8DE] hover:bg-gray-50"
                >Log in</RouterLink
              >
            </div>
          </div>
        </div>
      </DialogPanel>
    </Dialog>
  </header>
</template>

