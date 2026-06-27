export function GetCookie(nameSection: string){
    const result: Record<string, string> = {};
    if(!document.cookie) return undefined;
    document.cookie.split(';').forEach(cookie => {
        const [key, value] = cookie.trim().split('=');
        if (key !== undefined && value !== undefined) {
            try {
                result[key] = decodeURIComponent(value);
            } catch {
                result[key] = value;
            }
        }
    });
    return result[nameSection];
}

export function DeleteCookie(name: string) {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/`;
}

export function DeleteAllJWTToken() {
    DeleteCookie('accessJWT');
    DeleteCookie('refreshJWT');
    localStorage.removeItem('accessJWT');
    localStorage.removeItem('refreshJWT');
}

export function setAuthCookie(name: string, value: string) {
    const isSecure = window.location.protocol === 'https:';
    const secureAttr = isSecure ? 'Secure; ' : '';
    const sameSite = isSecure ? 'Strict' : 'Lax';
    const encodedValue = encodeURIComponent(value);
    document.cookie = `${name}=${encodedValue}; path=/; ${secureAttr}SameSite=${sameSite}`;
}


export function SetAllJWTToken(valueAccess: string, valueRefresh: string) {
    setAuthCookie("accessJWT", valueAccess);
    setAuthCookie("refreshJWT", valueRefresh);
}