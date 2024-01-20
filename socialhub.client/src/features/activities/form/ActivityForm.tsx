import React, { ChangeEvent } from 'react';
import { useState } from 'react';
import { Button, Form, Segment } from 'semantic-ui-react';
import { Activity } from '../../../app/models/activity';

interface Props {
    //Props
    activity: Activity | undefined;
    submitting: boolean;

    //Functions
    closeForm: () => void;
    createOrEdit: (activity: Activity) => void;
}

//activity: selectedActivity is a shorthand for activity: Activity | undefined
//Allows us to use the activity variable instead of selectedActivity otherwise we get an error
export default function ActivityForm({ activity: selectedActivity, closeForm, createOrEdit, submitting }: Props) {

    const initialState = selectedActivity ?? {
        //id is added as an empty string because it is a required field in the model, but users will not be able to see it
        id: '',
        title: '',
        date: '',
        description: '',
        category: '',
        city: '',
        venue: ''
    }

    const [activity, setActivityForm] = useState(initialState);

    function handleSubmit() {
        createOrEdit(activity);
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target;
        setActivityForm({ ...activity, [name]: value })
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input placeholder='Title' value={activity.title} name='title' onChange={handleInputChange} />
                <Form.Input type='date' placeholder='Date' value={activity.date} name='date' onChange={handleInputChange} />
                <Form.TextArea placeholder='Description' value={activity.description} name='description' onChange={handleInputChange} />
                <Form.Input placeholder='Category' value={activity.category} name='category' onChange={handleInputChange} />
                <Form.Input placeholder='City' value={activity.city} name='city' onChange={handleInputChange} />
                <Form.Input placeholder='Venue' value={activity.venue} name='venue' onChange={handleInputChange} />

                <Button onClick={closeForm} floated='left' type='button' content='Cancel' />
                <Button loading={submitting} floated='right' positive type='submit' content='Submit' />
            </Form>
        </Segment>
    )
}