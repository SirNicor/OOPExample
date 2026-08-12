import api from "@/api/Api.ts";
import type {CancelToken} from "axios";

export const AuthorizationResponse =
    {
        ResetAccesToken(token: string)
        {
            return api.get(`/ResetAccessToken`,{
                headers: {
                    'Authorization': token
                }});
        },
        Login(authorization: any, cancelToken?: CancelToken)
        {
            return api.post(`/Login`, {authorization, cancelToken});            
        },
        CheckAccessToken(accessToken: string)
        {
            return api.get(`/CheckAccessToken`, {
                headers: {
                    'Authorization': accessToken
                }
            })
        }
    }