import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import ProtectedRoute from '../services/roles/ProtectedRoute';
import DynamicNavbar from '../components/layout/DynamicNavbar';
import Role from '../models/enums/Role';
import Login from '../components/account/Login';
import Registration from '../components/account/Registration';
import Home from '../components/home/Home';
import MyProfile from '../components/user/MyProfile';
import ClubHistory from '../components/user/ClubHistory';
import NewPlayerAdvertisement from '../components/playerAdvertisement/NewPlayerAdvertisement';
import AdminHome from '../components/admin/AdminHome';
import Users from '../components/admin/Users';

const Routing = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/registration" element={<Registration />} />
        <Route path="/home" element={<ProtectedRoute element={<DynamicNavbar><Home /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/my-profile" element={<ProtectedRoute element={<DynamicNavbar><MyProfile /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        <Route path="/club-history" element={<ProtectedRoute element={<DynamicNavbar><ClubHistory /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/new-player-advertisement" element={<ProtectedRoute element={<DynamicNavbar><NewPlayerAdvertisement /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/admin-home" element={<ProtectedRoute element={<DynamicNavbar><AdminHome /></DynamicNavbar>} allowedRoles={[Role.Admin]} />} />
        <Route path="/admin-users" element={<ProtectedRoute element={<DynamicNavbar><Users /></DynamicNavbar>} allowedRoles={[Role.Admin]} />} />
      </Routes>
    </Router>
  );
};

export default Routing;