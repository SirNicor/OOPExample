export interface TableColumn
{
    key? : string;
    dataKey : string;
    title: string;
    width?: number;
    sortable?: boolean;
}
export interface TableData
{
    id: string;
    [key: string]: any;
}

export interface SortType
{
    sortKey: string;
    sortOrder: string;
}