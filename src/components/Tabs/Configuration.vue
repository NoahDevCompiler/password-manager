<script lang="ts" setup>
import { ref } from "vue";

const value1 = ref(12);
</script>
<template>
  <div class="slider w-full overflow-visible">
    <span class="passwordlength">Passwort Länge (Empfohlen: 12)</span>
    <el-slider v-model="value1" :min="8" :max="20" />
  </div>
</template>

<style scoped>

.el-slider .el-slider {
  margin-left: 14px;
}
.slider .passwordlength {
  font-size: 20px;
  line-height: 44px;
  flex: 1;
  overflow: hidden;
  text-overflow: hidden;
}
.slider .passwordlength + .el-slider {
  flex: 0 0 70%;
}
</style>