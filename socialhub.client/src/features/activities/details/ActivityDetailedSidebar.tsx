import { Segment, List, Label, Item, Image } from 'semantic-ui-react'
import { Link } from 'react-router-dom'
import { observer } from 'mobx-react-lite'
import { Activity } from '../../../app/models/activity'

interface Props {
    activitiy: Activity
}

//{attendees.length} {attendees.length === 1 ? 'Person' : 'People'} Going
//Ternary operator to display the number of attendees and the word "Person" or "People" based on the number of attendees

export default observer(function ActivityDetailedSidebar({ activitiy: { attendees, host } }: Props) {

    if (!attendees) return null;

    return (
        <>
            <Segment
                textAlign='center'
                style={{ border: 'none' }}
                attached='top'
                secondary
                inverted
                color='teal'
            >
               
                {attendees.length} {attendees.length === 1 ? 'Person' : 'People'} Going
            </Segment>
            <Segment attached>
                <List relaxed divided>
                    {attendees.map(attendee => (
                        <Item style={{ position: 'relative' }} key={attendee.username}>
                            {attendee.username === host?.username &&
                                <Label
                                    style={{ position: 'absolute' }}
                                    color='orange'
                                    ribbon='right'>
                                    Host
                                </Label>}
                            <Image size='tiny' src={attendee.image || '/user.png'} />

                            <Item.Content verticalAlign='middle'>
                                <Item.Header as='h3'>
                                    <Link to={`/profiles/${attendee.username}`}>{attendee.displayName}</Link>
                                </Item.Header>
                                <Item.Extra style={{ color: 'orange' }}>Following</Item.Extra>
                            </Item.Content>

                        </Item>
                    ))}                    
                </List>
            </Segment>
        </>

    )
})