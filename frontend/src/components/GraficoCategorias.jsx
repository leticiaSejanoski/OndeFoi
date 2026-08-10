import {
    PieChart,
    Pie,
    Tooltip,
    Legend
} from "recharts";

import api from "../services/api";
import { useEffect, useState } from "react";

function GraficoCategorias() {

    const [totalCategoria, setTotalCategoria] = useState([]);

    async function getTotal() {
        const dados = await api.get('/Dashboard');
        setTotalCategoria(dados.data.totalPorCategoria);
        console.log(dados);
    }

    useEffect(()=>{
        getTotal();
    }, []);

    return (
        <PieChart width={500} height={300}>
            <Pie
            data={totalCategoria}
             dataKey={"total"}
             nameKey={"categoriaNome"}
             cx="50%"
             cy="50%"
             outerRadius={100}
            />

            <Tooltip/>
            <Legend/>
                
        </PieChart>
    );
}

export default GraficoCategorias;