import React from 'react';
import { Grid } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';
import ActivityList from './ActivityList';
import ActivityDetails from '../details/ActivityDetails';
import ActivityForm from '../form/ActivityForm';

interface Props {
    activities: Activity[];
}

export default function ActivityDashboard({ activities }: Props) {
    return (
        <Grid>
            {/* Semantic UI Grid is 16 columns */}

            <Grid.Column width='10'>
                <ActivityList activities={activities} />
            </Grid.Column>

            <Grid.Column width='6'>
                {/* If there are activities, display the first activity */}
                {/* If there are no activities, display nothing */}
                {activities[0] &&
                    <ActivityDetails activity={activities[0]} />}
                <ActivityForm />
            </Grid.Column>
        </Grid>
    )
}
