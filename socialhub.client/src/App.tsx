import { useEffect, useState } from 'react';
import './App.css';
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';


function App() {

    //State that will be the value, and the function that will update the value
    const [activities, setActivities] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/api/activities')
            .then(response => {
                console.log(response);
                setActivities(response.data);
            })
    }, [])

    return (
        <div>
            <Header as='h2' icon='users' content='Social Hub' />

            <List>
                {activities.map((activity: any) => (
                    <List.Item key={activity.id}>
                        {activity.title}
                    </List.Item>
                ))}
            </List>
        </div>
        
        
        
    )
}

export default App;