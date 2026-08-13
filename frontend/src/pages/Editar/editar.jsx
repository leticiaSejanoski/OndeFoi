import "./style.css";
import api from "../../services/api";
import { useEffect, useState } from "react";

function Editar() {
    return (
        <div className="containerEditar">
            <div className="editarBloco1">
                <div className="editarGastos">
                    <h1>Gastos</h1>
                    <div className="tabelaGasto">
                        <table>
                            <thead>
                                <tr>
                                    <th>Data</th>
                                    <th>Descrição</th>
                                    <th>Categoria</th>
                                    <th>Valor</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>data</td>
                                    <td>descricao</td>
                                    <td>categoriaNome</td>
                                    <td>R$ valor</td>
                                    <td>icone editar</td>
                                    <td>icone excluir</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div className="editarBloco2">

                <div className="editarCategorias">
                    <h1>Categorias</h1>
                    <div className="tabelaCategorias">
                        <table>
                            <thead>
                                <tr>
                                    <th>nome</th>
                                    <th>icone editar</th>
                                    <th>icone excluir</th>
                                </tr>
                            </thead>
                                <tbody>
                                    <tr>
                                        <td>nome</td>
                                        <td>icone editar</td>
                                        <td>icone excluir</td>
                                    </tr>
                                </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    );
}

export default Editar;