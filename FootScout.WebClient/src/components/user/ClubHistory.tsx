import React, { useEffect, useState } from 'react';
import AccountService from '../../services/api/AccountService';
import '../../App.css';
import '../../styles/user/ClubHistory.css';

const ClubHistory = () => {
    const [id, setId] = useState<string | null>(null);

    useEffect(() => {
        const fetchRole = async () => {
            try {
                const userId = await AccountService.getId();
                setId(userId);
            } catch (error) {
                console.error('Failed to fetch user id:', error);
            }
        };
        fetchRole();
    }, []);
    
    return (
        <div className="ClubHistory">
            <h1>Club History page</h1>
            {id && <p>Id: {id}</p>}
        </div>
    );
}

export default ClubHistory;