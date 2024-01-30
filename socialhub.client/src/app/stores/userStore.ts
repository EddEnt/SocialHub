import { makeAutoObservable, runInAction } from "mobx";
import { User, UserFormValues } from "../models/user";
import agent from "../../api/agent";
import { store } from "./store";
import { router } from "../../api/router/Routes";

export default class UserStore {

    user: User | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (credentials: UserFormValues) => {
        const user = await agent.Account.login(credentials);
        store.commonStore.setToken(user.token);
        runInAction( () => this.user = user);
        router.navigate('/activities');
        store.modalStore.closeModal();
    }

    register = async (credentials: UserFormValues) => {
        const user = await agent.Account.register(credentials);
        store.commonStore.setToken(user.token);
        runInAction( () => this.user = user);
        router.navigate('/activities');
        store.modalStore.closeModal();
    }

    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
        router.navigate('/');
    }

    getUser = async () => {
        try {
            const user = await agent.Account.current();
            runInAction( () => this.user = user);
        } catch (error) {
            console.log(error);
        }
    }
}


