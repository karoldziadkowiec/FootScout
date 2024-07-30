import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import AccountService from '../../services/api/AccountService';

interface ProtectedRouteProps {
  element: React.ReactElement;
  allowedRoles: string[];
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ element, allowedRoles }) => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [role, setRole] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchAuthStatus = async () => {
      const authStatus = await AccountService.isAuthenticated();
      const userRole = await AccountService.getRole();
      setIsAuthenticated(authStatus);
      setRole(userRole);
      setLoading(false);
    };
    fetchAuthStatus();
  }, []);

  if (loading)
    return <div>Loading...</div>;

  if (!isAuthenticated)
    return <Navigate to="/" />

  if (!allowedRoles.includes(role || ''))
    return <Navigate to="/" />;

  return element;
};

export default ProtectedRoute;