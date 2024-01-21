import React from 'react';
import { Button, Card, CardContent, Image } from 'semantic-ui-react';
import { useStore } from '../../../app/stores/store';
import LoadingComponent from '../../../app/layout/LoadingComponents';


export default function ActivityDetails() {

    const { activityStore } = useStore();
    const { selectedActivity: activity, openForm, cancelSelectedActivity } = activityStore;

    if (!activity) return <LoadingComponent />;

    return (
        <Card fluid>
            <Image src={`categoryImages/${activity.category}.jpg`} />
            <CardContent>
                <Card.Header> {activity.title} </Card.Header>
                <Card.Meta>
                    <span>{activity.date}</span>
                </Card.Meta>
                <Card.Description> {activity.description} </Card.Description>             
            </CardContent>
            <Card.Content extra>
                <Button.Group widths='2'>
                    <Button onClick={cancelSelectedActivity} basic color='grey' content='Cancel' />
                    <Button onClick={() => openForm(activity.id)} basic color='blue' content='Edit' />
                </Button.Group>
            </Card.Content>                
        </Card>
    )
}

