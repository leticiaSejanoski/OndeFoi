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

    const [gastosPorCategoria, setGastosPorCategoria] = useState([]);

    const cores = [
        "#8884d8",
        "#82ca9d",
        "#ffc658",
        "#ff8042",
        "#0088FE"
    ];

    async function getTotal() {
        const dados = await api.get('/Dashboard');
        setGastosPorCategoria(dados.data.totalPorCategoria);
        console.log(dados);
    }

    function calculaPorcentagem() {
        let total = 0;
        gastosPorCategoria.forEach(categoria => {
            total += categoria.total
        });

        gastosPorCategoria.forEach(categoria => {
            const porcentagem = (categoria.total / total) * 100;
            console.log(categoria.categoriaNome, porcentagem);
        });

        const ordenados = gastosPorCategoria.toSorted((categoriaA, categoriaB) => (
            categoriaB.total - categoriaA.total
        ));

        if (gastosPorCategoria.length > 5) {
            let principais = ordenados.slice(0, 5);
            let resto = ordenados.slice(5);

            let totalResto = 0;
            resto.forEach(categoria => {
                totalResto += categoria.total;
            });

            const outros =
            {
                categoriaNome: "Outros",
                total: totalResto
            };

            const dadosGrafico = [...principais, outros];
            setGastosPorCategoria(dadosGrafico);

            console.log("TotalResto: " + totalResto);
        }
    }

    function desenharSetor(props) {
        const { index } = props;
        return (
            <Sector
                {...props}
                fill={cores[index % cores.length]}
            />
        );
    }

    useEffect(() => {
        getTotal();
    }, []);

    useEffect(() => {
        calculaPorcentagem();
    }, [gastosPorCategoria]);

    return (
        <PieChart width={500} height={300}>
            <Pie
                data={gastosPorCategoria}
                shape={desenharSetor}
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