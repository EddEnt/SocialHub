import { Button, Container, Menu } from 'semantic-ui-react';
import { NavLink } from 'react-router-dom';


export default function NavBar() {    

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item as={NavLink} to='/' header>
                    <img src="logo.jpg" alt="logo" style={{ marginRight: '8px' }} />
                    Social Hub
                </Menu.Item>
                <Menu.Item as={NavLink} to='/activities' name='Activities' />
                <Menu.Item>
                    <Button as={NavLink} to='/createActivity' positive content='Create Activity' />
                </Menu.Item>
            </Container>
        </Menu>
    )
}