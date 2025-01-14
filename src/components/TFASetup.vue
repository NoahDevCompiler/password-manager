<script setup>
import { ref, onMounted } from "vue";

const qrCodeUrl = ref('');
const code = ref('');
const error = ref('');

async function fetchQRCode() {
  try {
    const response = await setup2FA();
    qrCodeUrl.value = response.qrCodeUrl; 
  } catch (exception) {
    error.value = 'Fehler beim Abrufen des QR-Codes';
    console.error(exception);
  }
}

async function verifyCode() {
  try {
    await verify2FA(code.value); 
    $emit('2fa-verified');
  } catch (exception) {
    error.value = 'Ungültiger 2FA-Code';
    console.error(exception);
  }
}

fetchQRCode();

const inputs = ref([]);
const isButtonActive = ref(false);

onMounted(() => {
  inputs.value = document.querySelectorAll("input[type='number']");
});
function handleKeyUp(event, index) {
  const currentInput = event.target;
  const prevInput = inputs.value[index - 1];

  if (event.key === "Backspace") {
    currentInput.value = "";

    if (prevInput) {
      prevInput.disabled = false;
      prevInput.focus();
    }
  }
}

function handleInput(event, index) {
  const currentInput = event.target;
  const nextInput = inputs.value[index + 1];
  const prevInput = inputs.value[index - 1];

  if (currentInput.value.length > 1) {
    currentInput.value = currentInput.value.slice(1);
  }
  if (event.key === "Backspace") {
    currentInput.value = "";
  }
  if (nextInput && currentInput.value) {
    nextInput.disabled = false;
    nextInput.focus();
  }

  if (index == 5) {
    isButtonActive.value = true
  }
  else isButtonActive.value = false
}

function handleFormSubmit(event) {
  event.preventDefault();

  const code = inputs.value.map((input) => input.value).join("");
  console.log("Code:", code);
}
</script>

<template>
  <div class="items-center justify-center flex flex-col">
    <div>
        <h1 class="text-3xl text-center">Für die sichere Verwendung brauchst du eine Mehrfach-Authentifizierung</h1>
        <h1 class="text-xl text-center my-10">Scanne diesen QR-Code mit der Google-Authenticator App und bestätige mit dem Code</h1>
    </div>
    <div class="container">
      <img class="my-10 mx-auto" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAKsAAACrAQAAAAAxk1G0AAAC0ElEQVR4nOWXPa6jMBSFL3LhDjZgydtw5y2RDUDYAGzJnbeB5A2EzoXFnWMgyRRTvEszIw2KlJcvkeX7d859xH96Vvq/8YvokdcHq9njD7UFauR44zIQjYF6zS9tmggix8EM3u7OkOct8xTpcQuPUeFTS4qDWu7i3qstr4Rr5sT3MJfeqSWsYyiDK+M3eAGu+Y7m+/qtDD/HeHbHcz3V7t4u34YQ4J2oi2bQhYinULpsjwvKMNdkp5nWQaOJ0q7PKGV4i6Vj03DamJqcdlrPHhThl7MTr01Uszbk8CsEKse6tF7trjSBl4BvrrNFeItgdssHyHYn08nx7hEldWxxtdrOHgmTY62eGk1sOsyWw9lnlFJsSK/j0TVdVqjeeAMfA4ELIr7eo6+vKEUY771be18eGROpng5dIMac0xLXVlvOihlTdUYpwy8kOJcGF3RpdvZF5ci3DDOABltbJAmV1+kaVxHORGhDZJ3qNz29oxRhNA4rpBxaBYCbnsGL8KGa0DwoVpXeLV79LcOM9CTUCmYChZgYWRfjGiWGEiOFhBEUq866FGMaprA+QpqCevqE1w3MsNaYzuBa/AQSfgMjQ5m6UMij8mmCI93B6EFYq3166jIq/9ZvCd6dgoFs8DRMJ5YFHC/HWxXv8oBRZ+iWepI5FEKGXz4hyjGbXleZgQlckybBqDY8DfsCw6K9nd+Vl2GNmqeFaahiU5XvBkaJEOJQE1a6uraUI0oZRoJnh/MsM+YSgndWXoZ3X11ow6kY64h8X1HKMHYENq0uvYZ8YsT5dBIRru7qCrYe0jwTFOvcwmQYDwwWa9QYj7M/EibBAL2HqdbjYUcTthg5PvZBjDi2sCqcOPsODrCgBGGAiqMZ4dXvhVWI0UHZdBnr5KeR5bgu7NAG5mBaqlYgxohSI1toZySJ0T6f1fnnuJYrY//CNpeWrJZ6TTH+C/+n/eP4Fz9CWAw6qp2qAAAAAElFTkSuQmCC" width="400" height="300">
      <h4>One-Time Password</h4>
      <form @submit="handleFormSubmit">
        <div class="form-group">
          <input
            v-for="(input, index) in 6"
            :key="index"
            type="number"
            ref="input"
            :disabled="index !== 0"
            @input="handleInput($event, index)"
            @keyup="handleKeyUp($event, index)"
          />
        </div>

        <button :class="isButtonActive ? 'bg-[#4035d6e9]' : 'bg-gray-500'">
          Verifizieren
        </button>
      </form>
    </div>
  </div>
</template>

<style scoped>
.container {
  width: 400px;
  height: 400px;
  padding: 30px;
  border-radius: 12px;
  text-align: center;
  background: #1a1a1a;
}

.container form {
  width: 100%;
}
.container form .form-group {
  width: 100%;
  margin: 30px 0 36px;
  display: flex;
  justify-content: center;
}
.container form input {
  width: 50px;
  height: 50px;
  background: white;
  margin: 0 8px;
  font-size: 25px;
  text-align: center;
  border-radius: 3px;
  color: transparent;
  text-shadow: 0 0 0 #000;
  transition: 0.1s ease-in-out;
}
.container form input::-webkit-inner-spin-button {
  display: none;
}
.container form input:focus {
  outline-offset: 3px;
  outline: 2px solid #4135d6;
}
.container form button {
  cursor: pointer;
  color: #fff;
  width: 100%;
  padding: 12px 0;
  font-size: 18px;
  border-radius: 8px;
  letter-spacing: 0.5;
  pointer-events: none;
  opacity: 0.8;
  transition: 0.3s;
}
.container form button.active {
  opacity: 1;
  pointer-events: auto;
  color: white;
}

.container form button:hover {
  background: #4045d6e9;

}
</style>