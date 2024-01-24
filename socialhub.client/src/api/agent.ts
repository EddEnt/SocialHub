import axios, { AxiosError, AxiosResponse } from 'axios';
import { Activity } from '../app/models/activity';
import { toast } from 'react-toastify';

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
    const { data, status, config } = error.response!;
    switch (status) {
        case 400:
            toast.error('Bad Request');
            break;
        case 401:
            toast.error('Unauthorised');
            break;
        case 403:
            toast.error('Forbidden');
            break;
        case 404:
            toast.error('Not Found');
            break;
        case 500:
            toast.error('Server Error');
            break;
        default:
            toast.error('Something unexpected went wrong');
            break;
    }
    return Promise.reject(error);
})

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: object) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const Activities = {
    list: () => requests.get<Activity[]>('/activities'),
    details: (id: string) => requests.get<Activity>(`/activities/${id}`),
    create: (activity: Activity) => axios.post<void>('/activities', activity),
    update: (activity: Activity) => axios.put<void>(`/activities/${activity.id}`, activity),
    delete: (id: string) => axios.delete<void>(`/activities/${id}`)
}

const agent = {
    Activities
}

export default agent;
