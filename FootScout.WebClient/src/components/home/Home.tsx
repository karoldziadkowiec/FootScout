import React, { useEffect, useState } from 'react';
import AccountService from '../../services/api/AccountService';
import '../../App.css';
import '../../styles/home/Home.css';

const Home = () => {
    const [role, setRole] = useState<string | null>(null);
    const [email, setEmail] = useState<string | null>(null);
    const [id, setId] = useState<string | null>(null);

    useEffect(() => {
        const fetchRole = async () => {
            try {
                const userRole = await AccountService.getRole();
                setRole(userRole);
                const userEmail= await AccountService.getEmail();
                setEmail(userEmail);
                const userId = await AccountService.getId();
                setId(userId);
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
            {role && <p>E-mail: {email}</p>}
            {role && <p>Id: {id}</p>}
        </div>
    );
}

export default Home;