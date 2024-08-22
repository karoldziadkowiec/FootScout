import React, { useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
/*import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';*/
import '../../App.css';
import '../../styles/admin/AdminDashboard.css';

const AdminDashboard = () => {
    const location = useLocation();
    
    useEffect(() => {
        if (location.state && location.state.toastMessage)
            toast.success(location.state.toastMessage);
        
    }, [location]);

    return (
        <div className="AdminDashboard">
            <ToastContainer />
            <h1><i className="bi bi-grid"></i> Dashboard</h1>
        </div>
    );
}

export default AdminDashboard;