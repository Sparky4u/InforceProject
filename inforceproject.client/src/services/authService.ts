import axios from 'axios';

interface LoginResponse {
    token: string;
    username: string;
}

const API_URL = "/api/auth";

export const login = async (username: string, password: string): Promise<LoginResponse> => {
    const response = await axios.post<LoginResponse>(`${API_URL}/login`, { username, password });
    if (response.data.token) {
        localStorage.setItem("user", JSON.stringify(response.data));
    }
    return response.data;
};

export const logout = (): void => {
    localStorage.removeItem("user");
};
