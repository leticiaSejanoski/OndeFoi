import {
    PieChart,
    Pie,
    Tooltip,
    Legend,
    Sector
} from "recharts";

import api from "../services/api";
import { useEffect, useState } from "react";

function GraficoCategorias() {

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

    function calculaPorcentagem() {
        let total = 0;
        categorias.forEach(categoria => {
            total += categoria.total
        });

        categorias.forEach(categoria => {
            const porcentagem = (categoria.total / total) * 100;
            console.log(categoria.categoriaNome, porcentagem);
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
        }else{
            const dadosGrafico = categorias.map((categoria, index) => ({
                ...categoria,
                fill: cores[index % cores.length]
            }));

            setGastosPorCategoria(dadosGrafico);
        }

    }

    useEffect(() => {
        getTotal();
    }, []);

    useEffect(() => {
        calculaPorcentagem();
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

            <Tooltip />
            <Legend
                iconType="circle"
                layout="vertical"
                position={"insideBottomLeft"}
                iconSize={18}
                width={200}
                height={140}

            />

        </PieChart>
    );
}

export default GraficoCategorias;