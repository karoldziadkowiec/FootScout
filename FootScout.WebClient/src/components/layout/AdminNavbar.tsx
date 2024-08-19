import React from 'react';
import { NavLink } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import AccountService from '../../services/api/AccountService';
import '../../App.css';
import '../../styles/layout/AdminNavbar.css';

const AdminNavbarComponent = () => {
  return (
    <Navbar bg="primary" variant="dark" expand="lg">
      <Container>
        <img src={require('../../img/logo.png')} alt="logo" className="logo" />
        <Navbar.Brand as={NavLink} to="/admin-home">FootScout</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto blue-links">
            <Nav.Link as={NavLink} to="/admin-home">Home</Nav.Link>
            <Nav.Link as={NavLink} to="/admin-users">Users</Nav.Link>
          </Nav>
          <Nav className="ms-auto blue-links">
            <Nav.Link as={NavLink} to="/chats">Chat</Nav.Link>
            <NavDropdown title="My Profile" id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/my-profile">Profile</NavDropdown.Item>
              <NavDropdown.Item onClick={AccountService.logout} as={NavLink} to="/">Log out</NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default AdminNavbarComponent;