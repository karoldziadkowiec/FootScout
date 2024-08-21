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
    <Navbar bg="primary" variant="dark" expand="lg" className="sticky-top">
      <Container>
        <img src={require('../../img/logo.png')} alt="logo" className="logo" />
        <Navbar.Brand as={NavLink} to="/admin/home">FootScout</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto blue-links">
            <Nav.Link as={NavLink} to="/admin/home"><i className="bi bi-house-fill"></i> Home</Nav.Link>
            <Nav.Link as={NavLink} to="/admin/users"><i className="bi bi-people-fill"></i> Users</Nav.Link>
            <Nav.Link as={NavLink} to="/admin/users"><i className="bi bi-clock-history"></i> Club History</Nav.Link>
            <Nav.Link as={NavLink} to="/admin/users"><i className="bi bi-chat-text-fill"></i> Chats</Nav.Link>
            <NavDropdown title={<><i className="bi bi-list-nested"></i> Advertisements</>} id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/admin/users"><i className="bi bi-person-bounding-box"></i> Player's</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/admin/users"><i className="bi bi-shield-fill"></i> Club's</NavDropdown.Item>
            </NavDropdown>
            <NavDropdown title={<><i className="bi bi-briefcase-fill"></i> Offers</>} id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/admin/users"><i className="bi bi-person-bounding-box"></i> Player's</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/admin/users"><i className="bi bi-shield-fill"></i> Club's</NavDropdown.Item>
            </NavDropdown>
          </Nav>
          <Nav className="ms-auto blue-links">
            <Nav.Link as={NavLink} to="/admin/support"><i className="bi bi-gear-fill"></i> Reported Problems</Nav.Link>
            <Nav.Link as={NavLink} to="/chats"><i className="bi bi-chat-fill"></i> Chat</Nav.Link>
            <NavDropdown title={<><i className="bi bi-person-circle"></i> My Profile</>} id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/my-profile"><i className="bi bi-person-fill"></i> Profile</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/support"><i className="bi bi-wrench-adjustable"></i> Support</NavDropdown.Item>
              <NavDropdown.Item onClick={AccountService.logout} as={NavLink} to="/"><i className="bi bi-box-arrow-left"></i> Log out</NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default AdminNavbarComponent;