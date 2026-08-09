import { BrowserRouter, Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import Login from "./pages/Login/login";
import Cadastrar from "./pages/Cadastro/cadastro";
import Dashboard from "./pages/Dashboard";

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

            </Routes>
</>
    );
}

export default Rotas;