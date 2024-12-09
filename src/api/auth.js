import { ref } from 'vue'

export const STORAGE_KEY_TOKEN = 'session_token'
export const STORAGE_KEY_USERNAME = 'session_username'

const auth = ref(localStorage.getItem(STORAGE_KEY_TOKEN))
const username = ref(localStorage.getItem(STORAGE_KEY_USERNAME))

export function useToken () {
  function setToken (newToken) {
    auth.value = newToken
    localStorage.setItem(STORAGE_KEY_TOKEN, newToken)
  }

  function clearToken () {
    auth.value = ''
    localStorage.removeItem(STORAGE_KEY_TOKEN)
  }
  function setUsername (newUsername) {
    username.value = newUsername
    localStorage.setItem(STORAGE_KEY_USERNAME, newUsername)
  }

  return {
    token: auth,
    username: username,
    setToken,
    clearToken,
    setUsername,
  }
}