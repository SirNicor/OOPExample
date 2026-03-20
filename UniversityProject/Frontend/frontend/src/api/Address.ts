import api from "@/api/Api.ts";

export const AddressResponse =
    {
        getSuggest(query: string)
        {
            return api.get(`Address/Suggest/${query}`);
        }
    }