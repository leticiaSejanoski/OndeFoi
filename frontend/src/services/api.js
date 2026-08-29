import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5294/api'
})


api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});

api.interceptors.response.use(

    (resposta) => {
        // console.log("Entrou no interceptor de sucesso");
        return resposta;
    },

    async (erro) => {
        // console.log("Entrou no interceptor de erro");
        if (erro.response?.status === 401) {
            // console.log("Token inválido");

            const refreshToken = localStorage.getItem("refreshToken");

            // console.log(refreshToken);

            const resposta = await api.post("/Usuario/refresh",
                null,
                {
                    params: {
                        refreshToken: refreshToken
                    }
                }
            );

            const novoToken = resposta.data;
            localStorage.setItem("token", novoToken);

            return api(erro.config);
        }

        return Promise.reject(erro);
    }

)

export default api