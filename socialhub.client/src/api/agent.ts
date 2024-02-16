import axios, { AxiosError, AxiosResponse } from 'axios';
import { Activity, ActivityFormValues } from '../app/models/activity';
import { toast } from 'react-toastify';
import { router } from './router/Routes';
import { store } from '../app/stores/store';
import { User, UserFormValues } from '../app/models/user';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.response.use(async response => {    
        // Generate random delay between 1000 and 2500 milliseconds
        // This is to simulate a slow network, remove this code for production
        const randomDelay = Math.floor(Math.random() * (2500 - 1000 + 1)) + 1000;
        await sleep(randomDelay);
        return response;

}, (error: AxiosError) => {
    const { data, status, config } = error.response as AxiosResponse;
    switch (status) {

        case 400:

            if (config.method === 'get' && Object.prototype.hasOwnProperty.call(data.errors, 'id')) {
                router.navigate('/not-found');
            }

            if (data.errors) {

                const modalStateErrors = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modalStateErrors.push(data.errors[key])
                        }
                    }
                throw modalStateErrors.flat();
            }
            else {
                toast.error(data);
            }
            
            break;
        case 401:
            toast.error('Unauthorised');
            break;
        case 403:
            toast.error('Forbidden');
            break;
        case 404:
            // If the URL is not found, redirect to the NotFound page
            //Of note, router.navigate() is marked as unstable in the docs, so this may change in the future
            //Is not considered to be normal expected usage of the router, but Router devs are still testing it
            //For now, it works fine
            router.navigate('/not-found');            
            break;
        case 500:
            store.commonStore.setServerError(data);
            router.navigate('/server-error');
            break;
        default:
            toast.error('Something unexpected went wrong');
            break;
    }
    return Promise.reject(error);
})

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
})

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: object) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Activities = {
    list: () => requests.get<Activity[]>('/activities'),
    details: (id: string) => requests.get<Activity>(`/activities/${id}`),
    create: (activity: ActivityFormValues) => requests.post<void>('/activities', activity),
    update: (activity: ActivityFormValues) => requests.put<void>(`/activities/${activity.id}`, activity),
    delete: (id: string) => requests.delete<void>(`/activities/${id}`),
    attend: (id: string) => requests.post<void>(`/activities/${id}/attend`, {})
}

const Account = {
    current: () => requests.get<User>('/account'),
    login: (user: UserFormValues) => requests.post<User>('/account/login', user),
    register: (user: UserFormValues) => requests.post<User>('/account/register', user)

}

const agent = {
    Activities,
    Account
}

export default agent;
