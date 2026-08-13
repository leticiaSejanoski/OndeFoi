import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import Login from "./pages/Login/login";
import Cadastrar from "./pages/Cadastro/cadastro";
import Dashboard from "./pages/Dashboard";
import Editar from "./pages/Editar/editar";

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
                 path="/editar"
                 element={<Editar />}
                />

            </Routes>
</>
    );
}

export default Rotas;