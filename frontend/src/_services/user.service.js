import axios from 'axios';
import config from '../config';

const userService = {
    login,
    logout,
    signup,
    getCurrentUser,
    isUserLoggedIn,
};

function isUserLoggedIn() {
    const user = JSON.parse(localStorage.getItem('user'));
    return user ? true : false;
}

function getCurrentUser() {
    const user = JSON.parse(localStorage.getItem('user'));
    return user;
}

function login(username, password) {

    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };

    return fetch(`${config.apiUrl}/users/authenticate`, requestOptions)
        .then(handleResponse)
        .then(user => {
            // login successful if there's a jwt token in the response
            if (user.token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
            }

            return user;
        });
}

async function signup(email, username, password) {

    try {
        const res = await axios.post(`${config.apiUrl}/users`, {
            email,
            username,
            password
        });

        const user = res.data;

        if (user && user.jwtToken)
            localStorage.setItem('user', JSON.stringify(user));

        return user;

    } catch (err) {
        return {
            error: "Username or Email already taken"
        }
    }
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api
                logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}

function logout() {
    localStorage.removeItem('user');
}

export default userService;