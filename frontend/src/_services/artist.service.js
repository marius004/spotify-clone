import axios from 'axios';
import config from '../config.js';
import { authHeader } from '../_helpers/auth-header.js';

const artistService = {
    getById,
    getArtistName,
    getLikes,
    getPlainArtists,
    createNewArtist,
    updateArtist,
};

function updateArtist(id, name, categoriesId, rating, quote, image) {
    return axios.put(`${config.apiUrl}/artist/${id}`, {
        name,
        categoriesId,
        rating,
        quote,
        image,
    }, {
        headers: authHeader(),
    });
}

function createNewArtist(name, categoriesId, quote, image) {
    return axios.post(`${config.apiUrl}/artists`, {
        name,
        categoriesId,
        quote,
        image,
    }, {
        headers: authHeader(),
    })
}

function getPlainArtists() {
    return axios.get(`${config.apiUrl}/artists/plain`);
}

function getLikes(id) {
    return axios.get(`${config.apiUrl}/artist/likes/${id}`);
}

function getById(id) {
    return axios.get(`${config.apiUrl}/artists?singerId=${id}`);
}

function getArtistName(id) {
    return axios.get(`${config.apiUrl}/artists/name/${id}`);
}

export default artistService;