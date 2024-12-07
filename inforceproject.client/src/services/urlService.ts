import axios from 'axios';

interface Url {
    id: string;
    originalUrl: string;
    shortUrl: string;
    createdBy: string;
    createdDate: string;
}

const API_URL = "/api/url";

export const fetchUrls = async (): Promise<Url[]> => {
    const response = await axios.get<Url[]>(`${API_URL}`);
    console.log('Response data:', response.data); 
    return Array.isArray(response.data) ? response.data : []; 
};


export const fetchUrlDetails = async (id: string): Promise<Url> => {
    const response = await axios.get<Url>(`${API_URL}/${id}`);
    return response.data;
};


export const createShortUrl = async (originalUrl: string): Promise<Url> => {
    const response = await axios.post<Url>(`${API_URL}`, { originalUrl });
    return response.data;
};


export const deleteUrl = async (id: string): Promise<void> => {
    await axios.delete(`${API_URL}/${id}`);
};
