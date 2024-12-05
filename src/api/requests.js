
const backend = 'https://localhost:7117'

export async function register (username, password) {
    const response = await request(`/register`, {
        method: 'POST',
        body: JSON.stringify({username, password}),
    })
    if (response.token) {
        setToken(response.token)
    }

    return response.token
}

export async function login(username, password){
    const response = await request('/login', {
        method: 'POST',
        body: JSON.stringify({username, password})
    })
    if (response.token) {
        setToken(response.token)
    }

    return response.token  
}

async function request (url, options) {
    const headers = {
        'Content-Type': 'application/json',
        'X-Requested-With': 'XMLHttpRequest',
    }

    //if (token.value) {
        //headers['Authorization'] = 'Bearer ' + token.value
    //}

    const response = await fetch(backend + url, { headers, ...options })

    if (response.ok) {
        console.log(`${response}`)
        console.log("OK")
        return response.json()
    } else if (response.status === 422) {
        const data = await response.json()
        console.log("422")
        throw new ValidationError('validation failed', data.errors)
    } else {
        const data = await response.json();
        console.log(data.error)
        throw new Error(await data.error)
    }
}

class ValidationError {
    message
    errors

    constructor (message, errors) {
        this.message = message
        this.errors = errors
    }
}