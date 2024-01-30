export interface User {
    username: string;
    displayName: string;
    token: string;
    image?: string;
}

export interface UserFormValues {
    emailAddress: string;
    password: string;
    displayName?: string;
    username?: string;
}