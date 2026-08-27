import "./style.css"
import api from "../../services/api.js"
import GraficoCategorias from "../../components/GraficoCategorias.jsx";
import { useEffect, useState } from "react";


function Dashboard() {
    //cria gasto
    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState("");
    const [categoriaSelecionada, setCategoriaSelecionada] = useState("");
    const [data, setData] = useState(null);

    //cria categoria
    const [nome, setNome] = useState("");

    //outras funcionalidades
    const [categorias, setCategorias] = useState([]);
    const [gastosAgrupados, setGastosAgrupados] = useState([]);

    const [dadosDashboard, setDadosDashboard] = useState([]);
    const [editandoRenda, setEditandoRenda] = useState(false);
    const [renda, setRenda] = useState("");

    const [erros, setErros] = useState({});

    async function getCategorias() {
        const categorias = await api.get("/Categorias");
        setCategorias(categorias.data);
    }

    async function criaCategoria() {

        setErros({});

        try {
            await api.post("/Categorias", {
                nome
            });
        } catch (erros) {
            setErros(erros.response.data);
        }

        setNome("");
        getCategorias();
    }


    async function criaGasto() {

        setErros({});


        try {
            await api.post("/Gastos", {
                data,
                descricao,
                valor: Number(valor),
                categoriaId: Number(categoriaSelecionada)
            });

            setData(null);
            setDescricao("");
            setValor("");
            setCategoriaSelecionada("");

            getGastosAgrupados();

        } catch (erro) {
            setErros(erro.response.data);
        }
        // resumo();
    }

    // async function getGastos() {
    //     const gastos = await api.get("/Gastos");
    //     setGastos(gastos.data);
    //     console.log(gastos);
    // }

    async function getGastosAgrupados() {
        const resposta = await api.get("/Gastos/historico");
        setGastosAgrupados(resposta.data);
        console.log(resposta.data)
    }

    async function resumo() {
        const dados = await api.get("/Dashboard");
        setDadosDashboard(dados.data.dado);
        setRenda(dados.data.dado);
    }

    async function salvarRenda() {

        setErros({});

        try {
            await api.put("/Usuario/renda", {
                renda: Number(renda)
            });
            setEditandoRenda(false);
            resumo();
        } catch (erros) {
            setErros(erros.response.data)
        }
    }

    async function excluirGasto(id) {
        await api.delete(`/Gastos/${id}`);

        getGastosAgrupados();
        resumo();
    }

    useEffect(() => {
        getCategorias();
        getGastosAgrupados();
        resumo();
    }, []);

    return (
        <div className="container">

            <div className="bloco1">
                <div className="renda">
                    <h1>Renda</h1>
                    {editandoRenda ? (
                        <input type="number" name="renda" id="renda" value={renda} onChange={(e) => setRenda(e.target.value)} onKeyDown={(e) => {
                            if (e.key === "Enter") {
                                salvarRenda();
                            }
                        }}
                        />
                    ) : (<h2 onClick={() => {
                        setRenda(dadosDashboard.renda);
                        setEditandoRenda(true);
                    }}>
                        R$ {dadosDashboard.renda}</h2>)
                    }
                </div>

                <div className="totalGasto">
                    <h1>Total dos gastos</h1>
                    <h2>R$ {dadosDashboard.totalGastosMesAtual}</h2>
                </div>

                <div className="saldo">
                    <h1>Saldo atual</h1>
                    <h2>R$ {dadosDashboard.saldo}</h2>
                </div>
            </div>

            <div className="bloco2">
                <div className="divGrafico">
                    <div className="grafico">
                        <h1>Visão Geral Por Categoria (Mês)</h1>
                        <GraficoCategorias />
                    </div>
                </div>

                <div className="divGasto">
                    <div className="gastosInfo">
                        <h1>Últimos gastos</h1>
                        <div className="tabelaGasto">
                            <table>
                                <thead>
                                    <tr>
                                        <th>Data</th>
                                        <th>Descrição</th>
                                        <th>Categoria</th>
                                        <th>Valor</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {gastosAgrupados[0]?.gastos.map(gasto => (
                                        <tr key={gasto.id}>
                                            <td>{new Date(gasto.data).toLocaleDateString("pt-BR", {
                                                day: "2-digit",
                                                month: "2-digit"
                                            })}</td>
                                            <td>{gasto.descricao}</td>
                                            <td>{gasto.categoriaNome}</td>
                                            <td>R$ {gasto.valor}</td>
                                            <td><img className="iconExcluirGasto" onClick={() => excluirGasto(gasto.id)} src="./../../../public/excluir.png" alt="exluir" /></td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>

                <div className="bloco3">
                    <div className="formGasto">
                        <form action="">
                            <h1>+ Adicionar Novo Gasto</h1>

                            <div className="campo">
                                <label htmlFor="desc">Descrição</label>
                                <input placeholder="Ex: Lanche" type="text" name="desc" id="desc" value={descricao} onChange={(e) => setDescricao(e.target.value)} />
                                <p className="erro">{erros.Descricao}</p>
                            </div>

                            <div className="campo">
                                <label htmlFor="valor">R$</label>
                                <input type="number" step={0.01} min={0} name="valor" id="valor" value={valor} onChange={(e) => setValor(e.target.value)} />
                                <p className="erro">{erros.Valor}</p>
                            </div>


                            <div className="categoriaData">
                                <div className="campo campoDataCategoria" >
                                    <label htmlFor="categoria">Categoria</label>
                                    <select name="categoria" id="categoria" value={categoriaSelecionada} onChange={(e) => setCategoriaSelecionada(e.target.value)} >
                                        <option value="" disabled>Selecione uma categoria</option>
                                        {categorias.map(categoria => (
                                            <option key={categoria.id} value={categoria.id}>{categoria.nome}</option>
                                        ))}
                                    </select>
                                    <p className="erro">{erros.CategoriaId}</p>
                                </div>
                                <div className="campo campoDataCategoria">
                                    <label htmlFor="data">Data</label>
                                    <input type="date" name="data" id="data" value={data ?? ""} onChange={(e) => setData(e.target.value || null)} />
                                    <p className="erro">{erros.Data}</p>
                                </div>
                            </div>

                            <button type="button" onClick={criaGasto}>Salvar Gasto</button>
                        </form>
                    </div>

                    <div className="formCategoria">
                        <form action="">
                            <h1>+ Criar Categoria</h1>

                            <div className="campo">
                                <label htmlFor="nome" >Nome</label>
                                <input placeholder="Ex: Transporte" type="text" name="nome" id="nome" value={nome} onChange={(e) => setNome(e.target.value)} />
                                <p className="erro">{erros.Nome || erros.Categoria}</p>
                            </div>

                            <button type="button" onClick={criaCategoria}>Criar</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Dashboard