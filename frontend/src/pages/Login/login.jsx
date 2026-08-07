import './style.css'

import api from "../../services/api.js";
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';

function Login() {

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    const [erros, setErros] = useState({});

    const navigate = useNavigate();

    async function fazerLogin() {

        setErros({})

        try {
            const resposta = await api.post("/Usuario/login", {
                email,
                senha
            });

            localStorage.setItem("token", resposta.data.token);
            navigate("/dashboard");

        } catch (erro) {
            setErros(erro.response.data);
        }

    }
    return (
        <div className='login'>
            <form>
                <h1>Login</h1>
                <input placeholder='Email' type="email" name="email" value={email} onChange={(e) => setEmail(e.target.value)} />

                <input placeholder='Senha' type="password" name="senha" value={senha} onChange={(e) => setSenha(e.target.value)} />

                {erros.geral && <p className='erro'>{erros.geral}</p>}

                <button type='button' onClick={fazerLogin}>Entrar</button>

                <Link className='linkLogin' to={"/cadastro"}>
                    Não possui uma conta?
                </Link>

            </form>

        </div>

    );
}

export default Login