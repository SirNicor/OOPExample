    import axios from 'axios';
    import {GetCookie, DeleteAllJWTToken, SetAllJWTToken} from "@/Function/CookiesFunction.ts";
    import router from '@/router';
    import {AuthorizationResponse} from "@/api/Authorization.ts"; 
    
    const api = axios.create({
    baseURL: 'https://localhost:7082',
        timeout: 10000,
        headers: {'X-Custom-Header': 'foobar'},
        withCredentials: false
    });
    let isRefreshing = false;
    api.interceptors.response.use(
        response => {return response;},
        async (error) => {
            debugger;
            console.error('Axios interceptor error:', {
                url: error.config?.url,
                status: error.response?.status,
                message: error.message
            });
            const { status } = error.response || {};
            const config = error.config;
            if (status === 401) {
                const url = config.url?.toLowerCase() || '';
                if (url.includes("/login")) {
                    return Promise.reject(error);
                }
                if (url.includes("/resetaccesstoken")) {
                    DeleteAllJWTToken();
                    await router.push('/authorisation');
                    return Promise.reject(error);
                }
                const message = error.response?.data?.message;
                if (message.toLowerCase() === 'sendrefresh') {
                    if(isRefreshing) {
                        return Promise.reject(error);
                    }       
                    try {
                        const refreshToken = GetCookie("refreshJWT");
                        if (refreshToken !== undefined) {
                            if(isRefreshing) {
                                return Promise.reject(error);
                            }
                            isRefreshing = true;
                            console.log('Refreshing access token...');
                            const response = await AuthorizationResponse.ResetAccesToken(refreshToken);
                            
                            const newAccess = response.data.Accessjwt || response.data.accessjwt;
                            const newRefresh = response.data.Refreshjwt || response.data.refreshjwt;
                            SetAllJWTToken(newAccess, newRefresh);
                            isRefreshing = false;
                            console.log('Token refreshed, retrying request');
                            config.headers.Authorization = `${newAccess}`;   
                            return api(config);
                        } else {
                            console.warn('No refresh token, redirecting to auth');
                            DeleteAllJWTToken();
                            await router.push('/authorisation');
                            isRefreshing = false;
                            return Promise.reject(error);
                        }
                    } catch (refreshError) {
                        console.error('Token refresh failed:', refreshError);
                        DeleteAllJWTToken();
                        await router.push('/authorisation');
                        return Promise.reject(refreshError);
                    }
                } else {
                    console.warn('401 error, redirecting to auth');
                    DeleteAllJWTToken();
                    await router.push('/authorisation');
                    return Promise.reject(error);
                }
            }
            if (!error.response) {
                console.error(error.message);
            }
            isRefreshing = false;
            return Promise.reject(error);
        }
    );
    export default api;