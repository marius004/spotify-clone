import axios from "axios";
import config from "../config";

const songService = {
    getBySinger,
};

function getBySinger(singerId) {
    return axios.get(`${config.apiUrl}/songs?artistId=${singerId}`);
}

export default songService;