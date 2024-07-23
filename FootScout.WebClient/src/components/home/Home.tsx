import React, { useEffect, useState } from 'react';
import AccountService from '../../services/api/AccountService';
import '../../App.css';
import '../../styles/home/Home.css';

const Home = () => {
    const [role, setRole] = useState<string | null>(null);

    useEffect(() => {
        const fetchRole = async () => {
            try {
                const userRole = await AccountService.getRole();
                setRole(userRole);
            } catch (error) {
                console.error('Failed to fetch role:', error);
            }
        };
        fetchRole();
    }, []);

    return (
        <div className="Home">
            <h1>Home page</h1>
            {role && <p>Role: {role}</p>}
        </div>
    );
}

export default Home;