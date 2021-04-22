import axios from 'axios';
import config from '../config.js';

const artistService = {
    get,
};

async function get(id) {
    try {
        const res = await axios.get(`${config.apiUrl}/artists?singerId=${id}`);
        return res.data;
    } catch {
        const res = {};
        return res;
    }
}

export default artistService;