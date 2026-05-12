import api from "@/api/Api.ts";

export const AuthorizationResponse =
    {
        ResetAccesToken(token: string)
        {
            return api.get(`ResetAccessToken`,{
                headers: {
                    'Authorization': token
                }});
        },
        Login(authorization: any)
        {
            return api.post(`Login`, authorization);            
        },
        CheckAccessToken(accessToken: string)
        {
            return api.get(`CheckAccessToken`, {
                headers: {
                    'Authorization': accessToken
                }
            })
        }
    }