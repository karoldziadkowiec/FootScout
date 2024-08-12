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
import PlayerAdvertisements from '../components/playerAdvertisement/PlayerAdvertisements';
import PlayerAdvertisement from '../components/playerAdvertisement/PlayerAdvertisement';
import MyPlayerAdvertisements from '../components/user/MyPlayerAdvertisements';
import MyFavoritePlayerAdvertisements from '../components/user/MyFavoritePlayerAdvertisements';
import MyOffersAsPlayer from '../components/user/MyOffersAsPlayer';
import MyOffersAsClub from '../components/user/MyOffersAsClub';
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
        <Route path="/player-advertisements" element={<ProtectedRoute element={<DynamicNavbar><PlayerAdvertisements /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        <Route path="/player-advertisement/:id" element={<ProtectedRoute element={<DynamicNavbar><PlayerAdvertisement /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        <Route path="/my-player-advertisements" element={<ProtectedRoute element={<DynamicNavbar><MyPlayerAdvertisements /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/my-favorite-player-advertisements" element={<ProtectedRoute element={<DynamicNavbar><MyFavoritePlayerAdvertisements /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/my-offers-as-player" element={<ProtectedRoute element={<DynamicNavbar><MyOffersAsPlayer /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/my-offers-as-club" element={<ProtectedRoute element={<DynamicNavbar><MyOffersAsClub /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/new-player-advertisement" element={<ProtectedRoute element={<DynamicNavbar><NewPlayerAdvertisement /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/admin-home" element={<ProtectedRoute element={<DynamicNavbar><AdminHome /></DynamicNavbar>} allowedRoles={[Role.Admin]} />} />
        <Route path="/admin-users" element={<ProtectedRoute element={<DynamicNavbar><Users /></DynamicNavbar>} allowedRoles={[Role.Admin]} />} />
      </Routes>
    </Router>
  );
};

export default Routing;