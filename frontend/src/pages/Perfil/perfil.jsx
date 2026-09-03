import './style.css';
import api from '../../services/api';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

function Perfil() {

    const [usuario, setUsuario] = useState([]);

    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    const [editandoPerfil, setEditandoPerfil] = useState(false);

    const [erros, setErros] = useState({});

    const navigate = useNavigate();

    async function getUsuario() {
        const resposta = await api.get("/Usuario");
        setUsuario(resposta.data);
        console.log(resposta.data);
    }


    async function salvarAlteracoes() {

        setErros({});

        const dados = { nome, email };

        if (senha !== "") dados.senha = senha;


        try {
            await api.put("/Usuario",
                dados
            );

            return true;

        } catch (erro) {
            setErros(erro.response.data);

            return false;
        }
    }

    async function logout() {
        localStorage.removeItem("token");
        localStorage.removeItem("refreshToken");
        navigate("/");
    }

    async function excluirConta() {
        await api.delete("/Usuario");

        navigate("/")
    }


    function editarDados(usuario) {
        setNome(usuario.nome)
        setEmail(usuario.email);
        setSenha(usuario.senha);
    }

    useEffect(() => {
        getUsuario()
    }, []);


    return (
        <div className="containerPerfil">
            <div className='divPerfil'>
                <h1>Meu Perfil</h1>
                <div className='infosEditar'>
                    <h2>Informações pessoais</h2>
                    <form action="">
                        {editandoPerfil ? (
                            <>
                                <label htmlFor="nome">Nome</label>
                                <input value={nome} type="text" name="nome" id="nome" onChange={(e) => setNome(e.target.value)} />
                                <p className="erro">{erros.Nome}</p>


                                <label htmlFor="nome">Email</label>
                                <input value={email} type="email" name="email" id="email" onChange={(e) => setEmail(e.target.value)} />
                                <p className="erro">{erros.Email}</p>


                                <label htmlFor="senha">Senha</label>
                                <input type="password" name="senha" id="senha" onChange={(e) => setSenha(e.target.value)} />
                                <p className="erro">{erros.Senha}</p>


                                <div className='botoes'>
                                    <button type="button" onClick={async () => {
                                        const sucesso = await salvarAlteracoes();
                                        if (sucesso)
                                            setEditandoPerfil(false)
                                    }
                                    } >Salvar</button>
                                    <button onClick={() => { setEditandoPerfil(false); setErros({}) }} type="button">Cancelar</button>
                                </div>
                            </>
                        ) : (
                            <>
                                <div className='dadosUsuario'>
                                    <fieldset>
                                        <legend>Nome</legend>
                                        <p>{usuario[0]?.nome}</p>
                                    </fieldset>

                                    <fieldset>
                                        <legend>Email</legend>
                                        <p>{usuario[0]?.email}</p>
                                    </fieldset>

                                    <fieldset>
                                        <legend>Senha</legend>
                                        <p>*************</p>
                                    </fieldset>

                                </div>
                                <button onClick={() => (setEditandoPerfil(true), editarDados(usuario[0]))} type="button" className='btnEditar'>Editar Informações</button>
                            </>
                        )}
                    </form>

                </div>
                <div className='opcoesConta'>
                    <h2>Gerenciar conta</h2>
                    <div className='botoes'>
                        <button className='btnSair' type="button" onClick={logout}>Sair da conta</button>
                        {/* <div className='divBotãoExcluirConta'> */}
                        <button className='btnExcluir' type="button" onClick={() => excluirConta()}>Excluir conta</button>
                        {/* </div> */}
                    </div>
                    {/* <p>Essa ação é irreversível. Seus dados serão removidos permanentemente.</p> */}
                </div>


            </div>
        </div>
    );
}

export default Perfil;