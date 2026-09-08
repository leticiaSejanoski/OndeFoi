import {
    PieChart,
    Pie,
    Tooltip,
    Legend,
    ResponsiveContainer
} from "recharts";

import api from "../services/api";
import { useEffect, useState } from "react";

function GraficoCategorias({ atualizar }) {

    const [categorias, setCategorias] = useState([]); //dados da api
    const [gastosPorCategoria, setGastosPorCategoria] = useState([]); //dados modificados + "outros"

    const cores = [
        "#4BE1FC",
        "#FDBB33",
        "#F05BA1",
        "#70EB7B",
        "#C574FB"
    ];

    async function getTotal() {
        const dados = await api.get('/Dashboard');
        setCategorias(dados.data.dado.totalPorCategoria);
        console.log(dados.data);
    }


    function prepararDadosGrafico() {
        let total = 0;
        categorias.forEach(categoria => {
            total += categoria.total
        });

        const ordenados = categorias.toSorted((categoriaA, categoriaB) => (
            categoriaB.total - categoriaA.total
        ));

        let dadosBase = ordenados;

        if (categorias.length > 5) {
            let principais = ordenados.slice(0, 4);
            let resto = ordenados.slice(4);

            let totalResto = 0;
            resto.forEach(categoria => {
                totalResto += categoria.total;
            });

            dadosBase = [...principais, { categoriaNome: "Outros", total: totalResto }]
        }


        const dadosGrafico = dadosBase.map((categoria, index) => ({
            ...categoria,
            porcentagem: (categoria.total / total) * 100,
            fill: cores[index % cores.length]
        }));

        setGastosPorCategoria(dadosGrafico)
    }

    useEffect(() => {
        getTotal();
    }, [atualizar]);

    useEffect(() => {
        prepararDadosGrafico();
    }, [categorias]);

    return (
        <ResponsiveContainer width="100%" height="80%">
            <PieChart>
                <Pie
                    data={gastosPorCategoria}
                    dataKey={"total"}
                    nameKey={"categoriaNome"}
                    cx="50%"
                    cy="37%"
                    innerRadius={70}
                    outerRadius={110}
                />

                <Tooltip
                    formatter={(valor) => (
                        `R$${Number(valor).toLocaleString("pt-BR", {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        })}`
                    )}
                />

                <Legend
                    iconType="circle"
                    layout="vertical"
                    position="insideBottomLeft"
                    width={260}
                    height={100}
                    iconSize={18}
                    wrapperStyle={{ whiteSpace: 'nowrap' }}
                    labelStyle={{ fontSize: 14 }}
                    formatter={(categoriaNome, dadosCategoria) => {
                        return `${categoriaNome} (${Number(dadosCategoria.payload.porcentagem).toLocaleString("pt-BR", {
                            minimumFractionDigits: 2,
                            maximumFractionDigits: 2
                        })}%)`;
                    }}


                />

            </PieChart>
        </ResponsiveContainer>
    );
}

export default GraficoCategorias;