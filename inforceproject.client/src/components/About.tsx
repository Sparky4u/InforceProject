import React, { useState, useEffect } from 'react';
import { fetchAboutInfo, updateAboutInfo } from '../services/aboutService';

const About: React.FC = () => {
    const [aboutText, setAboutText] = useState('');
    const [error, setError] = useState<string | null>(null);
    const [isAdmin, setIsAdmin] = useState(false);
    
    useEffect(() => {
        const checkAdminStatus = () => {
            const token = localStorage.getItem('token');
            if (token) {
                try {
                    const payload = JSON.parse(atob(token.split('.')[1]));
                    setIsAdmin(payload?.role === 'Admin');
                } catch (err) {
                    console.error('Invalid token format', err);
                }
            }
        };

        checkAdminStatus();
    }, []);

    // Завантаження даних про About
    useEffect(() => {
        const loadAboutInfo = async () => {
            try {
                const data = await fetchAboutInfo();
                setAboutText(data);
            } catch {
                setError('Failed to load About info');
            }
        };
        loadAboutInfo();
    }, []);

    // Обробка зміни тексту
    const handleSave = async () => {
        try {
            await updateAboutInfo(aboutText);
        } catch {
            setError('Failed to save changes');
        }
    };

    return (
        <div>
            <h1>About URL Shortener</h1>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <textarea
                value={aboutText}
                onChange={(e) => setAboutText(e.target.value)}
                disabled={!isAdmin}
            />
            <button onClick={handleSave} disabled={!isAdmin}>
                Save Changes
            </button>
        </div>
    );
};

export default About;
