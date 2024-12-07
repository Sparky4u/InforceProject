import axios from 'axios';

const API_URL = '/api/about'; 

export const fetchAboutInfo = async (): Promise<string> => {
    const response = await axios.get<string>(`${API_URL}`);
    return response.data;
};

export const updateAboutInfo = async (newAboutText: string): Promise<void> => {
    await axios.put(`${API_URL}`, newAboutText);
};
