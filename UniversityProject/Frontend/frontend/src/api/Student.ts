import api from "@/api/Api.ts";
import {GetCookie} from "@/Function/CookiesFunction.ts";
import {userAccessPage} from "@/stores/AccessPage.ts";
import {ElMessage} from "element-plus";

export const StudentResponse =
    {
        getStudents(sortKey?: any, sortType?: any, numberPage?: any, filter?: any, count?: any)
        {
            let token = GetCookie("accessJWT");
            if(!userAccessPage().canAccessForAllOperationName("StudentRegister", ["Read", "All"]))
            {
                ElMessage.error('У вас нет доступа к данному действию');
                return;
            }
            return api.get(`Student?sortKey=${sortKey}&sortOrder=${sortType}&page=${numberPage}&count=${count}&filter=${filter}`,{
                headers: {
                    'Authorization': token
                }})
        },
        getCountPage(count: number)
        {
            let token = GetCookie("accessJWT");
            return api.get(`Student/Page/${count}`,{
                headers: {
                    'Authorization': token
                }});
        },
        getStudent(id?: any)
        {
            let token = GetCookie("accessJWT");
            if(!userAccessPage().canAccessForAllOperationName("StudentPage", ["Read", "All"]))
            {
                ElMessage.error('У вас нет доступа к данному действию');
                return;
            }
            return api.get(`Student/${id}`,{
                headers: {
                    'Authorization': token
                }});
        },
        deleteStudent(id?: any)
        {
            let token = GetCookie("accessJWT");
            if(!userAccessPage().canAccessForAllOperationName("StudentPage", ["Delete", "All"]))
            {
                ElMessage.error('У вас нет доступа к данному действию');
                return;
            }
            return api.delete(`Student/${id}`,{
                headers: {
                    'Authorization': token
                }});
        },
        putStudent(id?: any, student?: any)
        {
            let token = GetCookie("accessJWT");
            if(!userAccessPage().canAccessForAllOperationName("StudentPage", ["Update", "All"]))
            {
                ElMessage.error('У вас нет доступа к данному действию');
                return;
            }
            return api.put(`Student/${id}`, student,{
                headers: {
                    'Authorization': token
                }});
        },
        postStudent(student?: any)
        {
            let token = GetCookie("accessJWT");
            if(!userAccessPage().canAccessForAllOperationName("StudentPage", ["Create", "All"]))
            {
                ElMessage.error('У вас нет доступа к данному действию');
                return;
            }
            return api.post(`Student`, student,{
                headers: {
                    'Authorization': token
                }});
        }
    }
