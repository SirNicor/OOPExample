import { defineStore } from 'pinia'
import {ref, computed, reactive} from 'vue'
import type {VisibilityPage} from "@/types/Visibility.ts";
import {DeleteAllJWTToken} from "@/Function/CookiesFunction.ts";

interface AccessPageAndTypeOperation {
    TypeOperation: string,
    AccessPage: string
}

export const userAccessPage = defineStore('AccessPage', ()=>
{
    const accessPageAndTypeOperation = ref<AccessPageAndTypeOperation[]>([]);
    const visibilityPage = computed<VisibilityPage>(()=> {
        debugger;
        let studentVisibility = canAccessForAllOperationName("StudentPage", ["Read", "All"]) || canAccessForAllOperationName("StudentRegistry", ["Read", "All"])
        let adminVisibility = canAccessForAllOperationName("AdministratorPage", ["Read", "All"]) || canAccessForAllOperationName("AdministratorRegister", ["Read", "All"])
        let deleteCreateVisibility = accessPageAndTypeOperation.value.some(op => op.TypeOperation === "Create" || op.TypeOperation === "All")
        return{
            studentVisibility,
            adminVisibility,
            deleteCreateVisibility
        };
    })   
    function canAccess(pageName: string, operationName: string)
    {
        if(accessPageAndTypeOperation.value === undefined)
        {
            return false;   
        }
        return accessPageAndTypeOperation.value.some(operation => operation.AccessPage === pageName && operation.TypeOperation === operationName);
        
    }
    function canAccessForAllOperationName(pageName: string, operationNames: string[]) : boolean
    {
        if (!accessPageAndTypeOperation.value || operationNames.length === 0) {
            return false;
        }
        return operationNames.some(operationName =>
            {
                return accessPageAndTypeOperation.value.some(operation => operation.AccessPage === pageName && operation.TypeOperation === operationName);
            }
        )
    }
    function AddAccessPage(pageName: string, operationName: string): void {
        if(accessPageAndTypeOperation.value === undefined)
        {
            let accessPage : AccessPageAndTypeOperation[] =
                [{
                    TypeOperation: operationName,
                    AccessPage: pageName
                }];
            accessPageAndTypeOperation.value = accessPage;
            localStorage.setItem(pageName, operationName);
        }
        let access: AccessPageAndTypeOperation =
            {
                TypeOperation: operationName,
                AccessPage: pageName
            }
        if(accessPageAndTypeOperation.value.some(
            op => op.AccessPage === pageName && op.TypeOperation === operationName
        )) return;
        localStorage.setItem(pageName, operationName);
        accessPageAndTypeOperation.value.push(access);
        return;
    }

    function ResetForLocalStorageAccessPage(): void {
        if(localStorage.length === 0)
        {
            DeleteAllJWTToken();
            return;
        }
        for(let i = 0; i<localStorage.length; i++)
        {
            let key = localStorage.key(i);
            if(key === null) continue;
            let item = localStorage.getItem(key);
            if(item === null) continue;
            let access: AccessPageAndTypeOperation =
                {
                    TypeOperation: item,
                    AccessPage: key
                }
            if(accessPageAndTypeOperation.value.some(
                op => op.AccessPage === key && op.TypeOperation === item
            )) continue;
            accessPageAndTypeOperation.value.push(access);
        }
        return;
    }

    return {
        accessPageAndTypeOperation,
        canAccess,
        AddAccessPage,
        canAccessForAllOperationName,
        visibilityPage,
        ResetForLocalStorageAccessPage,
    };
}
)