import './style.css';
import api from '../../services/api';
import { useEffect, useState } from 'react';

function Historico() {

    const [gastos, setGastos] = useState([]);
    // const [gastosMes3, setGastosMes3] = useState([]);
    // const [gastosMes2, setGastosMes2] = useState([]);
    // const [gastosMes1, setGastosMes1] = useState([]);

    // let dataAtual = Date.now();
   


    async function getGastosHistorico() {
        const gastos = await api.get("/Gastos/historico");
        setGastos(gastos);
    }

    // function separaGastos(){
    //     gastos.forEach(gasto => {
    //         if(gasto.data )
    //     });
    // }


    useEffect(() => {
        getGastosHistorico();
    }, []);


    return (
        <div className='containerHistorico'>
            <div className='divBlocos'>
                <div className='divHistoricos'>
                    <h1>3 meses atras</h1>
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
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div className='divHistoricos'>
                    <h1>2 meses atras</h1>
                    <div className='tabelaHistorico'>
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
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div className='divHistoricos'>
                    <h1>1 mes atras</h1>
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
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Historico