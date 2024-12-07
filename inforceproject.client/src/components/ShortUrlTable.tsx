import React, { useEffect, useState } from 'react';
import { fetchUrls, createShortUrl, deleteUrl } from '../services/urlService';
import { useNavigate, Link } from 'react-router-dom';

interface Url {
    id: string;
    originalUrl: string;
    shortUrl: string;
    createdBy: string;
    createdDate: string;
}

const ShortUrlTable: React.FC = () => {
    const [urls, setUrls] = useState<Url[]>([]);
    const [newUrl, setNewUrl] = useState<string>('');
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const navigate = useNavigate();

    useEffect(() => {
        const loadUrls = async () => {
            try {
                setLoading(true);
                const data = await fetchUrls();
                console.log('Fetched URLs:', data); 
                if (Array.isArray(data)) {
                    setUrls(data);
                } else {
                    setError('Invalid data format received.');
                }
            } catch {
                setError('Failed to load URLs');
            } finally {
                setLoading(false);
            }
        };
        loadUrls();
    }, []);

    const handleAdd = async () => {
        if (!newUrl.trim()) {
            setError('URL cannot be empty.');
            return;
        }
        try {
            setLoading(true);
            const url = await createShortUrl(newUrl);
            setUrls((prevUrls) => [...prevUrls, url]);
            setNewUrl('');
            setError(null);
        } catch {
            setError('Failed to add URL. Ensure it is valid and unique.');
        } finally {
            setLoading(false);
        }
    };

    const handleDelete = async (id: string) => {
        try {
            setLoading(true);
            await deleteUrl(id);
            setUrls((prevUrls) => prevUrls.filter((url) => url.id !== id));
            setError(null);
        } catch {
            setError('Failed to delete URL. You might not have permission.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h1>Short URLs</h1>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <div>
                <input
                    type="text"
                    placeholder="Enter URL"
                    value={newUrl}
                    onChange={(e) => setNewUrl(e.target.value)}
                />
                <button onClick={handleAdd} disabled={loading}>
                    {loading ? 'Adding...' : 'Add URL'}
                </button>
            </div>
            {loading && <p>Loading...</p>}
            <table>
                <thead>
                    <tr>
                        <th>Original URL</th>
                        <th>Short URL</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {Array.isArray(urls) ? (
                        urls.map((url) => (
                            <tr key={url.id}>
                                <td>{url.originalUrl}</td>
                                <td>
                                    <Link to={`/url/${url.id}`}>{url.shortUrl}</Link>
                                </td>
                                <td>
                                    <button onClick={() => handleDelete(url.id)} disabled={loading}>
                                        {loading ? 'Deleting...' : 'Delete'}
                                    </button>
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan={3}>No URLs available</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
};

export default ShortUrlTable;
