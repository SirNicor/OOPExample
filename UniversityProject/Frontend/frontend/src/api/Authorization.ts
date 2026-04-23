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
            debugger;
            return api.post(`Login`, authorization, {
                headers: {
                    'Content-Type': 'application/json'
                }});            
        }
    }