import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:7082',
    timeout: 10000, 
    headers: {'X-Custom-Header': 'foobar'}
});

export default api;