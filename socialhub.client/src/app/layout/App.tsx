import './styles.css';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import HomePage from '../../features/home/HomePage';

function App() {

    const location = useLocation();

    return (
        <>
            <ToastContainer position='bottom-right' theme="colored" />
            {location.pathname === '/' ? <HomePage /> : (
                <>
                    <NavBar />
                    <Container style={{ marginTop: '7em' }}>
                        <Outlet />
                    </Container>
                </>
            )}
        </>               
        
    );
}

export default observer(App);