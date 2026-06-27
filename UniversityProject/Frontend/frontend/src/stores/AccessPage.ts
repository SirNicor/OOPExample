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
    function AddAccessPage(roles:any): void {
        localStorage.removeItem("rolesInfo");
        let accessPage : AccessPageAndTypeOperation;
        accessPageAndTypeOperation.value = []; 
        roles.forEach((role : any) => {
            role.typeOperationAccessPage.forEach((oper:any) => {
                accessPage =
                    {
                        TypeOperation: oper.item1,
                        AccessPage: oper.item2
                    }
                if(accessPageAndTypeOperation.value.some(
                    op => op.AccessPage === accessPage.AccessPage && op.TypeOperation === accessPage.TypeOperation
                )) return;
                accessPageAndTypeOperation.value.push(accessPage);
            })
        })
        localStorage.setItem("rolesInfo", JSON.stringify(accessPageAndTypeOperation.value));
        return;
    }

    function ResetForLocalStorageAccessPage(): void {
        if(localStorage.getItem("rolesInfo") === null)
        {
            DeleteAllJWTToken();
            return;
        }
        else
        {
            let allRoles : AccessPageAndTypeOperation[] = JSON.parse(localStorage.getItem("rolesInfo"));
            accessPageAndTypeOperation.value = allRoles;
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