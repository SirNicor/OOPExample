import api from "@/api/Api.ts";

export const StudentResponse =
    {
        getStudents(sortKey?: any, sortType?: any, numberPage?: any, filter?: any, count?: any)
        {
            return api.get(`Student?sortKey=${sortKey}&sortOrder=${sortType}&page=${numberPage}&count=${count}&filter=${filter}`)
        },
        getCountPage(count: number)
        {
            return api.get(`Student/Page/${count}`);
        },
        getStudent(id?: any)
        {
            return api.get(`Student/${id}`);
        },
        deleteStudent(id?: any)
        {
            return api.delete(`Student/${id}`);
        },
        putStudent(id?: any, student?: any)
        {
            return api.put(`Student/${id}`, student);
        },
        postStudent(student?: any)
        {
            return api.post(`Student`, student);
        }
    }
