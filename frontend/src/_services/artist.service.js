import axios from 'axios';
import config from '../config.js';

const artistService = {
    getById,
};

async function getById(id) {
    return await axios.get(`${config.apiUrl}/artists?singerId=${id}`);
}

export default artistService;