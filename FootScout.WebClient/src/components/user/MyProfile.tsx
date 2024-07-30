import React, { useEffect, useState } from 'react';
import AccountService from '../../services/api/AccountService';
/*import UserService from '../../services/api/UserService';*/
import '../../App.css';
import '../../styles/user/MyProfile.css';

const MyProfile = () => {
    const [role, setRole] = useState<string | null>(null);
    const [id, setId] = useState<string | null>(null);

    useEffect(() => {
        const fetchRole = async () => {
            try {
                const userRole = await AccountService.getRole();
                setRole(userRole);
                const userId = await AccountService.getId();
                setId(userId);
            } catch (error) {
                console.error('Failed to fetch role:', error);
            }
        };
        fetchRole();
    }, []);

    return (
        <div className="MyProfile">
            <h1>My Profile page</h1>
            {role && <p>Role: {role}</p>}
            {role && <p>Id: {id}</p>}
        </div>
    );
}

export default MyProfile;