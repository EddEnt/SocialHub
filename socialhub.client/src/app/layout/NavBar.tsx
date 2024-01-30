import { Button, Container, Dropdown, Image, Menu } from 'semantic-ui-react';
import { Link, NavLink } from 'react-router-dom';
import { useStore } from '../stores/store';
import { observer } from 'mobx-react-lite';


export default observer(function NavBar() {

    const { userStore: { logout, user } } = useStore();

    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item as={NavLink} to='/' header>
                    <img src="/logo.jpg" alt="logo" style={{ marginRight: '8px' }} />
                    Social Hub
                </Menu.Item>
                <Menu.Item as={NavLink} to='/activities' name='Activities' />
                <Menu.Item as={NavLink} to='/errors' name='Errors' />
                <Menu.Item>
                    <Button as={NavLink} to='/createActivity' positive content='Create Activity' />
                </Menu.Item>
                <Menu.Item position='right'>
                    <Image src={user?.image || '/user.png'} avatar spaced='right' />
                    <Dropdown pointing='top left' text={user?.displayName}>
                        <Dropdown.Menu>
                            <Dropdown.Item as={Link} to={`/profile/${user?.username}`} text='My Profile' icon='user' />
                            <Dropdown.Item onClick={logout} text='Logout' icon='sign-out' />
                        </Dropdown.Menu>
                    </Dropdown>
                </Menu.Item>
            </Container>
        </Menu>
    )
})