import axios from 'axios';
import config from '../config';
import { authHeader } from '../_helpers/auth-header';

const userService = {
    login,
    logout,
    signup,
    getCurrentUser,
    isUserLoggedIn,
    getUserId,
    getSongsLiked,
    getArtistsLiked,
    removeSongLiked,
    addSongLiked,
    updateSongsLikedOnBackend,
    addArtistLiked,
    removeArtistLiked,
};

async function updateSongsLikedOnBackend(id) {

    const data = [id];
    const songsLiked = getSongsLiked();

    if (songsLiked.includes(id)) {
        const res = await axios.put(`${config.apiUrl}/user`, {
            songsLiked: data
        }, {
            headers: authHeader(),
        });
        if (res.status == 401) {
            logout();
            window.location.reload(true);
        }
    } else {
        const res = await axios.put(`${config.apiUrl}/user`, {
            songsUnliked: data
        }, {
            headers: authHeader(),
        });
        if (res.status == 401) {
            logout();
            window.location.reload(true);
        }
    }
}

function removeSongLiked(id) {
    const user = JSON.parse(localStorage.getItem('user'));

    if (!user.songsLiked)
        user.songsLiked = [];

    user.songsLiked = user.songsLiked.filter(sng => sng !== id);
    localStorage.setItem('user', JSON.stringify(user));
}

function addSongLiked(id) {
    const user = getCurrentUser();

    if (!user.songsLiked)
        user.songsLiked = [];

    user.songsLiked = [...user.songsLiked, id];
    localStorage.setItem('user', JSON.stringify(user));
}

function removeArtistLiked(id) {
    const user = getCurrentUser();

    if (!user.artistsLiked)
        user.artistsLiked = [];

    user.artistsLiked = user.artistsLiked.filter(art => art !== id);
    localStorage.setItem('user', JSON.stringify(user));
}

function addArtistLiked(id) {
    const user = getCurrentUser();

    if (!user.artistsLiked)
        user.artistsLiked = [];

    user.artistsLiked = [...user.artistsLiked, id];
    localStorage.setItem('user', JSON.stringify(user));
}

function getArtistsLiked() {
    const user = JSON.parse(localStorage.getItem('user'));
    return user && user.artistsLiked ? user.artistsLiked : [];
}

function getSongsLiked() {
    const user = JSON.parse(localStorage.getItem('user'));
    return user && user.songsLiked ? user.songsLiked : [];
}

function getUserId() {
    const user = JSON.parse(localStorage.getItem('user'));
    return user ? user.id : "";
}

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
            if (user.jwtToken) {
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