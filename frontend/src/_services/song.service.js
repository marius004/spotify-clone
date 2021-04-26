import axios from "axios";
import config from "../config";

const songService = {
    getBySinger,
    getById,
    getSongLikes,
};

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