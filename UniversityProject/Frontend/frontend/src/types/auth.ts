export interface AuthForm {
    phone: string;
    login: string;
    password: string;
    rememberMe: boolean;
}

export interface RegistrationForm extends AuthForm {
    email: string;
    confirmPassword: string;
}

export interface ValidationRule
{
    required?: boolean;
    minLength?: number;
    maxLength?: number;
    message: string;
    trigger?: "blur" | "change" | "input" | ['blur', 'change'];
}