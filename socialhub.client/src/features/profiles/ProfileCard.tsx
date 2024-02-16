import { Profile } from '../../app/models/profile';
import { observer } from 'mobx-react-lite';
import { Card, Icon, Image } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

interface Props {
    profile: Profile
}

export default observer (function ProfileCard({ profile }: Props) {
    return (
        <Card as={Link} to={`profiles/${profile.username}`}>
            <Image src={profile.image || '/user.png'} />

            <Card.Content>
                <Card.Header>{profile.displayName}</Card.Header>
                <Card.Description>profile.bio will go here</Card.Description>
            </Card.Content>
            <Card.Content extra>
                <Icon name='user' />
                20 Hardcoded followers
            </Card.Content>

        </Card>
    )
})