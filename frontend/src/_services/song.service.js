import axios from "axios";
import config from "../config";
import { authHeader } from "../_helpers/auth-header";

const songService = {
    getBySinger,
    getById,
    getSongLikes,
    createNewSong,
    updateSong,
    getAll,
    deleteById,
};

function deleteById(id) {
    return axios.delete(`${config.apiUrl}/song/${id}`, {
        headers: authHeader(),
    });
}

function getAll() {
    return axios.get(`${config.apiUrl}/songs/plain`);
}

function updateSong(songId, name, artistId, audio) {
    return axios.put(`${config.apiUrl}/song/${songId}`, {
        name,
        artistId,
        audio,
    }, {
        headers: authHeader(),
    });
}

function createNewSong(name, artistId, categoriesId, base64Audio) {
    return axios.post(`${config.apiUrl}/songs`, {
        name,
        artistId,
        audio: base64Audio,
        categoriesId
    }, {
        headers: authHeader(),
    });
}

function getSongLikes(id) {
    return axios.get(`${config.apiUrl}/song/likes/${id}`);
}

function getBySinger(singerId) {
    return axios.get(`${config.apiUrl}/songs?artistId=${singerId}`);
}

function getById(id) {
    return axios.get(`${config.apiUrl}/song/${id}`);
}

export default songService;