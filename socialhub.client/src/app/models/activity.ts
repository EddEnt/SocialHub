import { Profile } from "./profile"

export interface IActivity {
    id: string
    title: string
    date: Date | null
    description: string
    category: string
    city: string
    venue: string
    hostUsername: string
    isCancelled: boolean
    isGoing: boolean
    isHost: boolean
    host?: Profile
    attendees?: Profile[]
}

//Take all the properties of the Activity interface and populate them with the values of the ActivityFormValues interface
//Extra properties are for our Linter rules
export class Activity implements IActivity {
    constructor(init: ActivityFormValues) {
        this.id = init.id!;
        this.title = init.title;
        this.date = init.date;
        this.description = init.description;
        this.category = init.category;
        this.city = init.city;
        this.venue = init.venue;
    }

    id: string
    title: string
    date: Date | null
    description: string
    category: string
    city: string
    venue: string
    hostUsername: string = ''
    isCancelled: boolean = false
    isGoing: boolean = false
    isHost: boolean = false
    host?: Profile
    attendees?: Profile[]

}

export class ActivityFormValues {
    id?: string = undefined;
    title: string = ' ';
    category: string = ' ';
    description: string = ' ';
    date: Date | null = null;
    city: string = ' ';
    venue: string = ' ';

    constructor(activity?: ActivityFormValues) {

        if (activity) {
            this.id = activity.id;
            this.title = activity.title;
            this.category = activity.category;
            this.description = activity.description;
            this.date = activity.date;
            this.city = activity.city;
            this.venue = activity.venue;
        }
    }
}
