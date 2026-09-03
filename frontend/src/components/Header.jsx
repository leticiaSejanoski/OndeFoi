import './style.css';
import { Link, NavLink } from 'react-router-dom';

function Header() {
    return (
        <header>
            <div className='links'>
                <div className='logo'>
                    <h1>OndeFoi</h1>
                </div>
                <nav className='nav'>
                    <NavLink className= {({isActive}) => isActive ? 'link active': 'link'}  to="/dashboard">Dashboard</NavLink>
                    <NavLink className= {({isActive}) => isActive ? 'link active': 'link'} to={"/historico"}> Histórico</NavLink>
                    <NavLink className= {({isActive}) => isActive ? 'link active': 'link'} to={"/editar"}>Editar</NavLink>
                </nav>
                <div className='perfil'>
                   <NavLink className= 'link' to="/perfil"><img src="./../../../public/perfil.png" alt="Perfil"/> </NavLink>
                </div>
            </div>
        </header>
    );
}

export default Header;