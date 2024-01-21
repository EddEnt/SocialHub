import { useEffect } from 'react';
import './styles.css';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import LoadingComponent from './LoadingComponents';
import { useStore } from '../stores/store';


function App() {

    const {activityStore} = useStore();

    //UseEffect for our API calls
    useEffect(() => {
        activityStore.loadActivities();
    }, [activityStore])

    if (activityStore.loadingInitial) return <LoadingComponent content='Loading...' />

    return (
        <>
            <NavBar />
            <Container style={{ marginTop: '7em' }}>               
                <ActivityDashboard />
            </Container>
        </>
        
        
        
    )
}

export default App;