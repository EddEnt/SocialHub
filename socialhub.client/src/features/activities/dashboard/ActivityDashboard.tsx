import React from 'react';
import { Grid } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';
import ActivityList from './ActivityList';
import ActivityDetails from '../details/ActivityDetails';
import ActivityForm from '../form/ActivityForm';

interface Props {
    //Props
    activities: Activity[];
    selectedActivity: Activity | undefined;
    editMode: boolean;
    submitting: boolean;

    //Functions
    selectActivity: (id: string) => void;
    cancelSelectActivity: () => void;
    openForm: (id: string) => void;
    closeForm: () => void;
    createOrEdit: (activity: Activity) => void;
    deleteActivity: (id: string) => void;
    
}

export default function ActivityDashboard({ activities, selectActivity, selectedActivity,
    deleteActivity, cancelSelectActivity, editMode, openForm, closeForm, createOrEdit, submitting }: Props) {
    return (
        <Grid>
            {/* Semantic UI Grid is 16 columns */}

            <Grid.Column width='10'>
                <ActivityList activities={activities}
                    selectActivity={selectActivity}
                    deleteActivity={deleteActivity}
                    submitting={submitting}
                />
            </Grid.Column>

            <Grid.Column width='6'>
                {/* If there are activities, display the first activity */}
                {/* If there are no activities, display nothing */}
                {selectedActivity && !editMode &&
                    <ActivityDetails
                        activity={selectedActivity}
                        cancelSelectActivity={cancelSelectActivity}
                        openForm={openForm}
                    />}
                {editMode &&
                    <ActivityForm
                        closeForm={closeForm}
                        activity={selectedActivity}
                        createOrEdit={createOrEdit}
                        submitting={submitting}
                    />}
            </Grid.Column>
        </Grid>
    )
}
