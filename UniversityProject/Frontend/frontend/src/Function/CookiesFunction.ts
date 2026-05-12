export function GetCookie(nameSection: string){
    const result: Record<string, string> = {};
    if(!document.cookie) return undefined;
    document.cookie.split(';').forEach(cookie => {
        const [key, value] = cookie.trim().split('=');
        if (key !== undefined && value !== undefined) {
            result[key] = value;
        }
    });
    debugger;
    return result[nameSection];
}

export function DeleteCookie(name: string) {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/`;
}

export function DeleteAllJWTToken() {
    DeleteCookie('accessJWT');
    DeleteCookie('refreshJWT');
}