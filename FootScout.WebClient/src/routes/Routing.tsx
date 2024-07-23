import React from 'react';
import Navbar from '../components/layout/Navbar';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import RenderWithNavbarProps from '../models/interfaces/RenderWithNavbarProps';
import Login from '../components/account/Login';
import Registration from '../components/account/Registration';
import Home from '../components/home/Home';
import MyProfile from '../components/user/MyProfile';

const RenderWithNavbar = ({ children }: RenderWithNavbarProps) => {
  return (
    <>
      <Navbar/>
      <div className="main-content">
        {children}
      </div>
    </>
  );
};

const Routing = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/registration" element={<Registration />} />
        <Route path="/home" element={<RenderWithNavbar><Home /></RenderWithNavbar>} />
        <Route path="/my-profile" element={<RenderWithNavbar><MyProfile /></RenderWithNavbar>} />
      </Routes>
    </Router>
  );
}

export default Routing;