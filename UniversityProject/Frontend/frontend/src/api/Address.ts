import api from "@/api/Api.ts";
import {GetCookie} from "@/Function/CookiesFunction.ts";
import type {CancelToken} from "axios";

export const AddressResponse =
    {
        getSuggest(query: string, cancelToken?: CancelToken)
        {
            let token = GetCookie("accessJWT");
            return api.get(`Address/Suggest/${query}`,{
                headers: {
                    'Authorization': token
                }, cancelToken});
        }
    }