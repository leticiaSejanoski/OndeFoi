import './style.css';
import api from '../../services/api';
import { useEffect, useState } from 'react';

function Historico() {

    const [grupoGastos, setGrupoGastos] = useState([]);
    const meses = [
        "Janeiro",
        "Fevereiro",
        "Março",
        "Abriu",
        "Maio",
        "Junho",
        "Julho",
        "Agosto",
        "Setembro",
        "Outubro",
        "Novembro",
        "Dezembro"
    ];

    async function getGastosHistorico() {
        const resposta = await api.get("/Gastos/historico");
        setGrupoGastos(resposta.data);
        console.log(resposta.data);
    }

    useEffect(() => {
        getGastosHistorico();
    }, []);

    <h1>TESTE</h1>

    return (
        <div className='containerHistorico'>
            <div className='divBlocos'>
                {grupoGastos.slice(1, 4).map(grupo => (
                        <div key={`${grupo.mes}-${grupo.ano}`} className='divHistoricos'>
                            <h1>
                               {`${meses[grupo.mes-1]}/${grupo.ano}`}
                            </h1>
                            <div className="tabelaHistorico">
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
                                        {grupo.gastos.map(gasto => (
                                            <tr key={gasto.id}>
                                                <td>{new Date(gasto.data).toLocaleString("pt-BR", {
                                                    day: "2-digit",
                                                    month: "2-digit"
                                                })}</td>
                                                <td>{gasto.descricao}</td>
                                                <td>{gasto.categoriaNome}</td>
                                                <td>{gasto.valor}</td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                ))}
            </div>
        </div>
    );
}

export default Historico