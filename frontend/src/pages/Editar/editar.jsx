import "./style.css";
import api from "../../services/api";
import { useEffect, useState } from "react";

function Editar() {

    const [gastos, setGastos] = useState([]);
    const [categorias, setCategorias] = useState([]);

    const [data, setData] = useState("");
    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState(0);
    const [categoriaSelecionada, setCategoriaSelecionada] = useState("");

    const [nome, setNome] = useState("");

    const [gastoEditando, setGastoEditando] = useState(null);
    const [categoriaEditando, setCategoriaEditando] = useState(null);

    const [erros, setErros] = useState({});

    async function getGastos() {
        const gastos = await api.get("/Gastos");
        setGastos(gastos.data);
    }

    async function getCategorias() {
        const categorias = await api.get("/Categorias");
        setCategorias(categorias.data);
    }


    async function salvarGasto(id) {

          setErros({});

        try {
            await api.put(`/Gastos/${id}`, {
                data,
                descricao,
                valor: Number(valor),
                categoriaId: Number(categoriaSelecionada)
            })
        } catch (erro) {
            setErros(erro.response.data)
        }

        getGastos();
    }

    async function salvarCategoria(id) {

        setErros({});

        try {
            await api.put(`/Categorias/${id}`, {
                nome
            });
        } catch (erro) {
            setErros(erro.response.data);
        }
        getCategorias();
    }

    async function excluirCategoria(id) {
        await api.delete(`/Categorias/${id}`);
        getCategorias();
    }

    async function excluirGasto(id) {
        await api.delete(`/Gastos/${id}`);
        getGastos();
    }

    function editarGasto(gasto) {
        setData(gasto.data);
        setDescricao(gasto.descricao);
        setCategoriaSelecionada(gasto.categoriaId);
        setValor(gasto.valor)
    }

    function editarCategoria(categoria) {
        setNome(categoria.nome);
    }

    useEffect(() => {
        getGastos();
        getCategorias();
    }, []);



    return (
        <div className="containerEditar">
            <div className="editarBloco1">
                <div className="editarGastos">
                    <h1>Gastos</h1>
                    <div className="tabelaGasto">
                        <table>
                            <thead>
                                <tr>
                                    <th className="colunaDado">Data</th>
                                    <th className="colunaDado">Descrição</th>
                                    <th className="colunaDado">Categoria</th>
                                    <th className="colunaDado">Valor</th>
                                    <th className="colunaIcone"></th>
                                    <th className="colunaIcone"></th>
                                </tr>
                            </thead>
                            <tbody>
                                {gastos.map(gasto => (
                                    <tr key={gasto.id}>
                                        {gastoEditando === gasto.id ? (
                                            <>
                                                <td><input type="date" name="data" id="data" value={data} onChange={(e) => setData(e.target.value)} /></td>
                                                <td><input type="text" value={descricao} onChange={(e) => (setDescricao(gasto.descricao),
                                                    setDescricao(e.target.value))} /></td>
                                                <td>
                                                    <select name="categorias" value={categoriaSelecionada} onChange={(e) => (
                                                        setCategoriaSelecionada(e.target.value)
                                                    )} >
                                                        {categorias.map(categoria =>
                                                            <option key={categoria.id} value={categoria.id}>{categoria.nome}</option>
                                                        )}
                                                    </select>
                                                </td>
                                                <td><input type="number" value={valor} onChange={(e) => (setValor(e.target.value))} /></td>

                                                <td><button type="button" onClick={() => (salvarGasto(gasto.id), setGastoEditando(null))}>Salvar</button></td>
                                                <td><button type="button" onClick={() => setGastoEditando(null)}>Cancelar</button></td>
                                            </>
                                        ) : (
                                            <>
                                                <td className="colunaDado">{new Date(gasto.data).toLocaleDateString("pt-BR", {
                                                    day: "2-digit",
                                                    month: "2-digit"
                                                })}</td>
                                                <td className="colunaDado">{gasto.descricao}</td>
                                                <td className="colunaDado">{gasto.categoriaNome}</td>
                                                <td className="colunaDado">R$ {gasto.valor}</td>
                                                <td className="colunaIcone"><img className="btnEditarExcluir" src="./../../../public/editar.png" alt="Editar-gasto" onClick={() => (editarGasto(gasto), setGastoEditando(gasto.id))} /></td>
                                                <td className="colunaIcone"><img className="btnEditarExcluir" src="./../../../public/desperdicio.png" alt="Excluir-gasto" onClick={() => excluirGasto(gasto.id)} />
                                                </td>

                                            </>
                                        )}
                                    </tr>
                                ))}

                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div className="editarBloco2">

                <div className="editarCategorias">
                    <div className="TituloTabela">
                        <h1>Categorias</h1>
                        <p className="erro">{erros.categoria}</p>
                    </div>
                    <div className="tabelaCategoria">
                        <table>
                            <thead>
                                <tr>
                                    <th>Nome</th>
                                    <th className="colunaIcone"></th>
                                    <th className="colunaIcone"></th>
                                </tr>
                            </thead>
                            <tbody>
                                {categorias.map(categoria => (
                                    <tr key={categoria.id}>
                                        {categoriaEditando === categoria.id ? (
                                            <>
                                                <td><input type="text" value={nome} onChange={(e) => setNome(e.target.value)} /></td>
                                                <td><button type="button" onClick={()=>(salvarCategoria(categoria.id), setCategoriaEditando(null))}>Salvar</button></td>
                                                <td><button type="button" onClick={()=>setCategoriaEditando(null)}>Cancelar</button></td>
                                            </>
                                        ) : (
                                            <>
                                                <td>{categoria.nome}</td>
                                                <td className="colunaIcone"><img className="btnEditarExcluir" src="./../../../public/editar.png" alt="Editar-gasto" onClick={() => (editarCategoria(categoria), setCategoriaEditando(categoria.id))} /></td>
                                                <td className="colunaIcone"><img className="btnEditarExcluir" src="./../../../public/desperdicio.png" alt="Excluir-gasto" onClick={() => (
                                                    excluirCategoria(categoria.id)
                                                )} /></td>
                                            </>
                                        )}
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    );
}

export default Editar;