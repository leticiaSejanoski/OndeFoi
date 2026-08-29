import './style.css';
import api from '../../services/api';
import { useEffect, useState } from 'react';

function Historico() {

    const [grupoGastos, setGrupoGastos] = useState([]);
    const meses = [
        "Janeiro",
        "Fevereiro",
        "Março",
        "Abril",
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

    async function excluirMesHistorico(mes, ano) {
        try{
            await api.delete("/Gastos/historico/mes", {

                params: {
                    mes,
                    ano
                }
            });
            getGastosHistorico();

        }catch(erro){
            console.log(erro.response.data);
        }

    }

    useEffect(() => {
        getGastosHistorico();
    }, []);

    return (
        <div className='containerHistorico'>
            <div className='divBlocos'>
                {grupoGastos.slice(1).map(grupo => (
                    <div key={`${grupo.mes}-${grupo.ano}`} className='divHistoricos'>
                        <div className='cabeçalhoDivMes'>
                            <h1>
                                {`${meses[grupo.mes - 1]}/${grupo.ano}`}
                            </h1>
                            <p><img title="Excluir gastos" src="./../../../public/excluir.png" alt='Botão "Excluir histórico"' onClick={() => excluirMesHistorico(grupo.mes, grupo.ano)}/></p>
                        </div>
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
                                            <td>{Number(gasto.valor).toFixed(2)}</td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                            <div className="totalMes">
                                <p>Total: R$ {Number(grupo.total).toFixed(2)}</p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Historico