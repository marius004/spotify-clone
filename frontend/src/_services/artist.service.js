import axios from 'axios';
import config from '../config.js';

const artistService = {
    getById,
    getArtistName,
};

function getById(id) {
    return axios.get(`${config.apiUrl}/artists?singerId=${id}`);
}

function getArtistName(id) {
    return axios.get(`${config.apiUrl}/artists/name/${id}`);
}

export default artistService;