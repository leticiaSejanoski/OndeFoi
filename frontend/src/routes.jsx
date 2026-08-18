import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import Login from "./pages/Login/login";
import Cadastrar from "./pages/Cadastro/cadastro";
import Dashboard from "./pages/Dashboard";
import Editar from "./pages/Editar/editar";
import Historico from "./pages/Histórico/historico";

function Rotas() {
    return (
        <>
            <Header />

            <Routes>
                <Route
                    path="/"
                    element={<Login />}
                />

                <Route
                    path="/cadastro"
                    element={<Cadastrar />}
                />

                <Route
                    path="/dashboard"
                    element={<Dashboard />}
                />

                <Route
                    path="/historico"
                    element={<Historico />}
                />

                <Route
                    path="/editar"
                    element={<Editar />}
                />

            </Routes>
        </>
    );
}

export default Rotas;