import "./style.css"
import api from "../../services/api.js"
import { useEffect, useState } from "react";

function Dashboard() {
    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState("");
    const [categoriaSelecionada, setCategoriaSelecionada] = useState("");
    const [data, setData] = useState("");

    const [nome, setNome] = useState("");

    const [erros, setErros] = useState({});
    const [categorias, setCategorias] = useState([]);
    const [gastos, setGastos] = useState([]);

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
    }

    async function criaGasto() {

        setErros({});

        try {
            await api.post("/Gastos", {
                descricao,
                valor: Number(valor),
                categoriaId: Number(categoriaSelecionada),
                data
            });
        } catch (erro) {
            setErros(erro.response.data);
        }
    }

    async function getGastos() {
        const gastos = await api.get("/Gastos")
        setGastos(gastos.data);
        console.log(gastos);
    }

    useEffect(() => {
        getCategorias();
        getGastos();
    }, []);

    return (
        <div className="container">
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
                                </tr>
                            </thead>
                            <tbody>
                                {gastos.map(gasto => (
                                    <tr key={gasto.id}>
                                        <td>{new Date(gasto.data).toLocaleDateString("pt-BR", {
                                            day: "2-digit",
                                            month: "2-digit"
                                        })}</td>
                                        <td>{gasto.descricao}</td>
                                        <td>{gasto.categoriaNome}</td>
                                        <td>R$ {gasto.valor}</td>
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
                        </div>

                        <div className="campo">
                            <label htmlFor="valor">R$</label>
                            <input type="number" step={0.01} min={0} name="valor" id="valor" value={valor} onChange={(e) => setValor(e.target.value)} />
                        </div>


                        <div className="categoriaData">
                            <div className="campo">
                                <label htmlFor="categoria">Categoria</label>
                                <select name="categoria" id="categoria" value={categoriaSelecionada} onChange={(e) => setCategoriaSelecionada(e.target.value)} >
                                    <option value="" disabled>Selecione uma categoria</option>
                                    {categorias.map(categoria => (
                                        <option key={categoria.id} value={categoria.id}>{categoria.nome}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="campo">
                                <label htmlFor="data">Data</label>
                                <input type="date" name="data" id="data" value={data} onChange={(e) => setData(e.target.value)} />
                                {erros.data && <p className="erro">{erros.data}</p>}
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
                            {erros.categoria && <p className="erro">{erros.categoria}</p>}
                        </div>

                        <button type="button" onClick={criaCategoria}>Criar</button>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default Dashboard