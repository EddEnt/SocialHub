import { useEffect, useState } from 'react';
import './styles.css';
import { Container, Loader } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import { v4 as uuid } from 'uuid';
import agent from '../../api/agent';
import LoadingComponent from './LoadingComponents';


function App() {

    //State that will be the value, and the function that will update the value
    const [activities, setActivities] = useState<Activity[]>([]);

    //When selecting an activity, it will be either undefined, by default, or an Activity
    const [selectedActivity, setSelectedActivity] =
        useState<Activity | undefined>(undefined);
    const [editMode, setEditMode] = useState(false);
    const [loading, setLoading] = useState(true);
    const [submitting, setSubmitting] = useState(false);

    //UseEffect for our API calls
    useEffect(() => {
        agent.Activities.list().then(response => {
            let activities: Activity[] = [];
            response.forEach(activity => {
                activity.date = activity.date.split('T')[0];
                activities.push(activity);
            })
            console.log(response);
            setActivities(response);
            setLoading(false);
        })
    }, [])

    function handleSelectActivity(id: string) {
        //Find the activity with the id that matches the id passed in
        //x is the activity object
        setSelectedActivity(activities.find(x => x.id === id));
    }

    function handleCancelSelectActivity() {
        setSelectedActivity(undefined);
    }

    function handleFormOpen(id?: string) {
        id ? handleSelectActivity(id) : handleCancelSelectActivity();
        setEditMode(true);
    }

    function handleFormClose() {
        setEditMode(false);
    }

    //Function to create or edit an activity
    function handleCreateOrEditActivity(activity: Activity) {
        setSubmitting(true);

        //If the activity has an id, we know it is an existing activity
        //If it does not have an id, we know it is a new activity
        if (activity.id) {
            agent.Activities.update(activity).then(() => {
                setActivities([...activities.filter(x => x.id !== activity.id), activity])
                setSelectedActivity(activity);
                setEditMode(false);
                setSubmitting(false);
            })
        }
        else {
            activity.id = uuid();
            agent.Activities.create(activity).then(() => {
                setActivities([...activities, activity]);
                setSelectedActivity(activity);
                setEditMode(false);
                setSubmitting(false);
            })
        }
    }

    function handleDeleteActivity(id: string) {
        setSubmitting(true);
        agent.Activities.delete(id).then(() => {
            setActivities([...activities.filter(x => x.id !== id)])
            setSubmitting(false);
        })
    }

    if (loading) return <LoadingComponent content='Loading...' />

    return (
        <>
            <NavBar openForm={handleFormOpen} />
            <Container style={{ marginTop: '7em' }}>
                <ActivityDashboard
                    activities={activities}
                    selectedActivity={selectedActivity}
                    selectActivity={handleSelectActivity}
                    cancelSelectActivity={handleCancelSelectActivity}
                    editMode={editMode}
                    openForm={handleFormOpen}
                    closeForm={handleFormClose}
                    createOrEdit={handleCreateOrEditActivity}
                    deleteActivity={handleDeleteActivity}
                    submitting={submitting}
                />
            </Container>
        </>
        
        
        
    )
}

export default App;