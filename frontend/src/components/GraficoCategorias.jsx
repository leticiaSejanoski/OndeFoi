import {
    PieChart,
    Pie,
    Tooltip,
    Legend,
} from "recharts";

import api from "../services/api";
import { useEffect, useState } from "react";

function GraficoCategorias({ atualizar }) {

    const [categorias, setCategorias] = useState([]); //dados da api
    const [gastosPorCategoria, setGastosPorCategoria] = useState([]); //dados modificados + "outros"

    const cores = [
        "#8884d8",
        "#82ca9d",
        "#ffc658",
        "#ff8042",
        "#0088FE"
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

        if (categorias.length > 4) {
            let principais = ordenados.slice(0, 4);
            let resto = ordenados.slice(4);

            let totalResto = 0;
            resto.forEach(categoria => {
                totalResto += categoria.total;
            });

            const outros =
            {
                categoriaNome: "Outros",
                total: totalResto
            };

            const dadosGrafico = [...principais, outros].map((categoria, index) => ({
                ...categoria,
                fill: cores[index % cores.length]
            }));

            setGastosPorCategoria(dadosGrafico);
        } else {
            const dadosGrafico = categorias.map((categoria, index) => ({
                ...categoria,
                porcentagem: (categoria.total / total) * 100,
                fill: cores[index % cores.length]
            }));

            setGastosPorCategoria(dadosGrafico);
        }
    }

    useEffect(() => {
        getTotal();
    }, [atualizar]);

    useEffect(() => {
        prepararDadosGrafico();
    }, [categorias]);

    return (
        <PieChart width={500} height={300}>
            <Pie
                data={gastosPorCategoria}
                dataKey={"total"}
                nameKey={"categoriaNome"}
                cx="50%"
                cy="50%"
                innerRadius={70}
                outerRadius={120}
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
                position={"insideBottomLeft"}
                iconSize={18}
                width={200}
                height={140}
                formatter={(categoriaNome, dadosCategoria) => {
                    return `${categoriaNome} (${Number(dadosCategoria.payload.porcentagem).toLocaleString("pt-BR", {
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    })}%)`;
                }}


            />

        </PieChart>
    );
}

export default GraficoCategorias;