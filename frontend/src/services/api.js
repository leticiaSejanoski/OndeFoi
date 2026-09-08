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

        return resposta;
    },

    async (erro) => {
        const requisicaoOriginal = erro.config;

        if (erro.response?.status === 401 &&
            !requisicaoOriginal.jaTentouRefresh &&
            !requisicaoOriginal.url.includes("/Usuario/refresh") &&
            !requisicaoOriginal.url.includes("/Usuario/login")
        ) {

            requisicaoOriginal.jaTentouRefresh = true;

            const refreshToken = localStorage.getItem("refreshToken");

            if (!refreshToken) {
                localStorage.removeItem("token");
                localStorage.removeItem("refreshToken");

                window.location.href = "/";

                return Promise.reject(erro);
            }

            try {

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

                requisicaoOriginal.headers.Authorization = `Bearer ${novoToken}`;

                return api(requisicaoOriginal);
            } catch (erro) {
                localStorage.removeItem("token");
                localStorage.removeItem("refreshToken");

                window.location.href = "/";

                return Promise.reject(erro);

            }
        }
        return Promise.reject(erro);
    }
)

export default api