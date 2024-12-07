import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchUrlDetails } from '../services/urlService';

const ShortUrlInfo: React.FC = () => {
    const { id } = useParams();
    const [urlDetails, setUrlDetails] = useState<any>(null);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const loadUrlDetails = async () => {
            try {
                const data = await fetchUrlDetails(id!);
                setUrlDetails(data);
            } catch {
                setError('Failed to load URL details');
            }
        };
        loadUrlDetails();
    }, [id]);

    if (error) return <p>{error}</p>;

    return (
        <div>
            <h1>URL Info</h1>
            {urlDetails ? (
                <div>
                    <p>Original URL: {urlDetails.originalUrl}</p>
                    <p>Short URL: {urlDetails.shortUrl}</p>
                    <p>Created By: {urlDetails.createdBy}</p>
                    <p>Created Date: {urlDetails.createdDate}</p>
                </div>
            ) : (
                <p>Loading...</p>
            )}
        </div>
    );
};

export default ShortUrlInfo;
