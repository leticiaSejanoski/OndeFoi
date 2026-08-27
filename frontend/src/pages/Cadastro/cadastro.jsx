
import './style.css';
import api from "../../services/api.js";
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';


function Cadastrar() {

    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    const [erros, setErros] = useState({});

    const navigate = useNavigate();

    async function fazerCadastro() {

        setErros({});

        try {
            await api.post("/Usuario/cadastro", {
                nome,
                email,
                senha
            });

            const login = await api.post("/Usuario/login", {
                email,
                senha
            });

            localStorage.setItem("token", login.data.token);
            navigate("/dashboard");

        } catch (erro) {
            setErros(erro.response.data);
        }
    }
    return (
        <div className='cadastro'>
            <form>
                <h1>Cadastrar-se</h1>
                <div className='campoCadastro'>
                    <input placeholder='Nome' type="text" value={nome} onChange={(e) => setNome(e.target.value)} />
                    <p className="erroCadastro">{erros.Nome}</p>
                </div>

                <div className='campoCadastro'>
                    <input placeholder='Email' type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
                    <p className="erroCadastro">{erros.Email}</p>
                </div>

                <div className='campoCadastro'>
                    <input placeholder='Senha' type="password" value={senha} onChange={(e) => setSenha(e.target.value)} />
                    <p className="erroCadastro">{erros.Senha}</p>
                </div>
                <button type="button" onClick={fazerCadastro}>Cadastrar</button>

            </form>
        </div>
    )

}

export default Cadastrar;
