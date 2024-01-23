import { Link } from "react-router-dom";
import { Container, Header, Segment, Image, Button } from "semantic-ui-react";

export default function HomePage() {
    return (
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container text>
                <Header as='h1' inverted>
                    <Image rounded size='massive' src='/logo.jpg' alt='logo' style={{ marginBottom: 12 }} />
                    SocialHub
                </Header>
                <Header as='h2' inverted content='Welcome to Social Hub' />
                <Button as={Link} to='/activities' size='huge' inverted>
                    Take me to the Activities!
                </Button>
            </Container>
        </Segment>
    )
}