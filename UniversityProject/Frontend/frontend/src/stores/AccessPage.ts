import { defineStore } from 'pinia'
import {ref, computed, reactive} from 'vue'
import {forEach} from "lodash";

interface AccessPageAndTypeOperation {
    TypeOperation: string,
    AccessPage: string
}

export const userAccessPage = defineStore('AccessPage', ()=>
{
    const accessPageAndTypeOperation = ref<AccessPageAndTypeOperation[]>();
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
                return accessPageAndTypeOperation.value!.some(operation => operation.AccessPage === pageName && operation.TypeOperation === operationName);
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
        }
        let access: AccessPageAndTypeOperation =
            {
                TypeOperation: operationName,
                AccessPage: pageName
            }
        if(accessPageAndTypeOperation.value.some(
            op => op.AccessPage === pageName && op.TypeOperation === operationName
        )) return;
        accessPageAndTypeOperation.value.push(access);
        return;
    }

    return {
        accessPageAndTypeOperation,
        canAccess,
        AddAccessPage,
        canAccessForAllOperationName
    };
}
)