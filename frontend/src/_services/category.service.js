import axios from "axios";
import config from "../config";

export function getCategories() {
    return axios.get(`${config.apiUrl}/songCategories`);
}