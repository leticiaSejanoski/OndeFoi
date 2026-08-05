import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/Login/login";
import Cadastrar from "./pages/Cadastro/cadastro";

function Rotas() {
    return (
        <BrowserRouter>

            <Routes>
                <Route
                    path="/"
                    element={<Login />}
                />

                <Route
                    path="/cadastro"
                    element={<Cadastrar />}
                />

             

            </Routes>

        </BrowserRouter>
    );
}

export default Rotas;