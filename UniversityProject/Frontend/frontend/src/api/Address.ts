import api from "@/api/Api.ts";
import {GetCookie} from "@/Function/CookiesFunction.ts";

export const AddressResponse =
    {
        getSuggest(query: string)
        {
            let token = GetCookie("accessJWT");
            return api.get(`Address/Suggest/${query}`,{
                headers: {
                    'Authorization': token
                }});
        }
    }