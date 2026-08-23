import './style.css';
import { Link } from 'react-router-dom';

function Header() {
    return (
        <header>
            <div className='links'>
                <div className='logo'>
                    <h1>OndeFoi</h1>
                </div>
                <nav className='nav'>
                    <Link className='link' to="/dashboard">Dashboard</Link>
                    <Link className='link' to={"/historico"}>Histórico</Link>
                    <Link className='link' to={"/editar"}>Editar</Link>
                </nav>
                <div className='perfil'>
                    <Link className='link' to={"/perfil"}>Perfil</Link>
                </div>
            </div>
        </header>
    );
}

export default Header;