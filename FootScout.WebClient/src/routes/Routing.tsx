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
import ClubAdvertisements from '../components/clubAdvertisement/ClubAdvertisements';
import PlayerAdvertisement from '../components/playerAdvertisement/PlayerAdvertisement';
import ClubAdvertisement from '../components/clubAdvertisement/ClubAdvertisement';
import MyPlayerAdvertisements from '../components/user/MyPlayerAdvertisements';
import MyClubAdvertisements from '../components/user/MyClubAdvertisements';
import MyFavoritePlayerAdvertisements from '../components/user/MyFavoritePlayerAdvertisements';
import MyFavoriteClubAdvertisements from '../components/user/MyFavoriteClubAdvertisements';
import Chats from '../components/chat/Chats';
import Chat from '../components/chat/Chat';
import MyOffersAsPlayer from '../components/user/MyOffersAsPlayer';
import MyOffersAsClub from '../components/user/MyOffersAsClub';
import NewPlayerAdvertisement from '../components/playerAdvertisement/NewPlayerAdvertisement';
import NewClubAdvertisement from '../components/clubAdvertisement/NewClubAdvertisement';
import Support from '../components/support/Support';
import AdminDashboard from '../components/admin/AdminDashboard';
import AdminUsers from '../components/admin/AdminUsers';
import AdminSupport from '../components/admin/AdminSupport';

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
        <Route path="/club-advertisements" element={<ProtectedRoute element={<DynamicNavbar><ClubAdvertisements /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        <Route path="/player-advertisement/:id" element={<ProtectedRoute element={<DynamicNavbar><PlayerAdvertisement /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        <Route path="/club-advertisement/:id" element={<ProtectedRoute element={<DynamicNavbar><ClubAdvertisement /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        <Route path="/my-player-advertisements" element={<ProtectedRoute element={<DynamicNavbar><MyPlayerAdvertisements /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/my-club-advertisements" element={<ProtectedRoute element={<DynamicNavbar><MyClubAdvertisements /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/my-favorite-player-advertisements" element={<ProtectedRoute element={<DynamicNavbar><MyFavoritePlayerAdvertisements /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/my-favorite-club-advertisements" element={<ProtectedRoute element={<DynamicNavbar><MyFavoriteClubAdvertisements /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/chats" element={<ProtectedRoute element={<DynamicNavbar><Chats /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        <Route path="/chat/:id" element={<ProtectedRoute element={<DynamicNavbar><Chat /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        <Route path="/my-offers-as-player" element={<ProtectedRoute element={<DynamicNavbar><MyOffersAsPlayer /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/my-offers-as-club" element={<ProtectedRoute element={<DynamicNavbar><MyOffersAsClub /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/new-player-advertisement" element={<ProtectedRoute element={<DynamicNavbar><NewPlayerAdvertisement /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/new-club-advertisement" element={<ProtectedRoute element={<DynamicNavbar><NewClubAdvertisement /></DynamicNavbar>} allowedRoles={[Role.User]} />} />
        <Route path="/support" element={<ProtectedRoute element={<DynamicNavbar><Support /></DynamicNavbar>} allowedRoles={[Role.Admin, Role.User]} />} />
        {/* Admin */}
        <Route path="/admin/dashboard" element={<ProtectedRoute element={<DynamicNavbar><AdminDashboard /></DynamicNavbar>} allowedRoles={[Role.Admin]} />} />
        <Route path="/admin/users" element={<ProtectedRoute element={<DynamicNavbar><AdminUsers /></DynamicNavbar>} allowedRoles={[Role.Admin]} />} />
        <Route path="/admin/support" element={<ProtectedRoute element={<DynamicNavbar><AdminSupport /></DynamicNavbar>} allowedRoles={[Role.Admin]} />} />
      </Routes>
    </Router>
  );
};

export default Routing;