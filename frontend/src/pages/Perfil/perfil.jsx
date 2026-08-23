import './style.css';
import api from '../../services/api';
import { useState, useEffect } from 'react';

function Perfil() {


    return (
        <div className="containerPerfil">
            <div className='divPerfil'>
                <h1>Meu Perfil</h1>
                    <div className='infosEditar'>
                        <h2>Informações pessoais</h2>
                        <form action="">
                            <label htmlFor="nome">Nome</label>
                            <input type="text" name="nome" id="nome" />

                            <label htmlFor="nome">Email</label>
                            <input type="email" name="email" id="email" />

                            <label htmlFor="senha">Senha</label>
                            <input type="password" name="senha" id="senha" />

                            <button type="button">Editar Informações</button>
                        </form>

                    </div>
                    <div className='opcoesConta'>
                        <h2>Gerenciar conta</h2>
                        <div className='botoesConta'>
                            <button className='btnSair' type="button">Sair da conta</button>
                            {/* <div className='divBotãoExcluirConta'> */}
                                <button className='btnExcluir' type="button">Excluir conta</button>
                            {/* </div> */}
                        </div>
                                {/* <p>Essa ação é irreversível. Seus dados serão removidos permanentemente.</p> */}
                    </div>

               
            </div>
        </div>
    );
}

export default Perfil;