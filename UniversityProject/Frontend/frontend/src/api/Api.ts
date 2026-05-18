    import axios from 'axios';
    import {GetCookie, DeleteAllJWTToken, SetAllJWTToken} from "@/Function/CookiesFunction.ts";
    import router from '@/router';
    import {AuthorizationResponse} from "@/api/Authorization.ts"; 
    
    const api = axios.create({
    baseURL: 'https://localhost:7082',
        timeout: 10000,
        headers: {'X-Custom-Header': 'foobar'}
    });

    api.interceptors.response.use(
        response => {return response;},
        async (error) => {
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
                    console.warn('⚠Login failed with 401');
                    return Promise.reject(error);
                }
                if (url.includes("/resetaccesstoken")) {
                    DeleteAllJWTToken();
                    await router.push('/authorisation');
                    return Promise.reject(error);
                }
                const message = error.response?.data?.message;
                if (message === 'SendRefresh') {
                    try {
                        const refreshToken = GetCookie("refreshJWT");
                        if (refreshToken) {
                            console.log('Refreshing access token...');
                            const response = await AuthorizationResponse.ResetAccesToken(refreshToken);
                            const newAccess = response.data.Accessjwt || response.data.accessjwt;
                            const newRefresh = response.data.Refreshjwt || response.data.refreshjwt;
                            SetAllJWTToken(newAccess, newRefresh);
                            if (config.headers) {
                                config.headers.Authorization = `Bearer ${newAccess}`;
                            }
                            console.log('Token refreshed, retrying request');
                            return api(config);
                        } else {
                            console.warn('No refresh token, redirecting to auth');
                            DeleteAllJWTToken();
                            await router.push('/authorisation');
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
            return Promise.reject(error);
        }
    );
    export default api;