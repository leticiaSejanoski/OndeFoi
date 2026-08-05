import axios from 'axios';

const api = axios.create({
    baseURL:'http://localhost:5294/api'
})

export default api