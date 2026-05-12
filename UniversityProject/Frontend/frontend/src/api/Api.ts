import axios from 'axios';
import {GetCookie, DeleteAllJWTToken} from "@/Function/CookiesFunction.ts";
import router from '@/router';
import {AuthorizationResponse} from "@/api/Authorization.ts"; 

const api = axios.create({
baseURL: 'https://localhost:7082',
    timeout: 10000,
    headers: {'X-Custom-Header': 'foobar'}
});

api.interceptors.response.use(
    response => response,
    async (error) => {
        const {status} = error.response || {};
        const config = error.config;
        if (status === 401) {
            switch(config.url)
            {
                case "/Login":
                {    
                    router.push(`/registration`);
                    DeleteAllJWTToken();
                    return Promise.reject(error);
                    break;
                }
                case "/ResetAccessToken":
                {
                    DeleteAllJWTToken();
                    router.push(`/authorisation`);
                    return Promise.reject(error);
                    break;
                }
                default:
                {
                    let message = error.response?.data?.message;
                    if(message === `SendRefresh`)
                    {
                        let refreshToken = GetCookie("refreshJWT");
                        if (refreshToken !== undefined) {
                            const response = await AuthorizationResponse.ResetAccesToken(refreshToken);
                            document.cookie = `accessJWT=${response.data.accessjwt}; path=/`;
                            document.cookie = `refreshJWT=${response.data.refreshjwt}; path=/`;
                            const originalUrl = config.url?.toLowerCase() || '';
                            if (config.headers) {
                                config.headers.Authorization = `Bearer ${response.data.accessjwt}`;
                            }
                            return api(config);
                        }
                        else
                        {
                            DeleteAllJWTToken();
                            router.push(`/authorisation`);
                        }
                    }
                    else
                    {
                        DeleteAllJWTToken();
                        router.push('/authorisation');
                    }
                    return Promise.reject(error);
                }
            }
        } else if (status === 403) {
            router.push('/forbidden');
            return Promise.reject(error);
        } else if (status === 404) {
            router.push('/not-found');
            return Promise.reject(error);
        } 
    }
)
export default api;