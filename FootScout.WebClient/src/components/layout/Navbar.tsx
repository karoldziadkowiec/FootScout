import React from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { NavLink } from 'react-router-dom';
import AccountService from '../../services/api/AccountService';
import '../../App.css';

const NavbarComponent = () => {
  return (
    <Navbar bg="dark" variant="dark" expand="lg">
      <Container>
        <img src={require('../../img/logo.png')} alt="logo" className="logo" />
        <Navbar.Brand as={NavLink} to="/home">FootScout</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto green-links">
            <Nav.Link as={NavLink} to="/home">Home</Nav.Link>
            <NavDropdown title="Advertisements" id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/my-profile">for Players</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/my-profile">for Clubs</NavDropdown.Item>
            </NavDropdown>
            <NavDropdown title="Recommendations" id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/my-profile">for Players</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/my-profile">for Clubs</NavDropdown.Item>
            </NavDropdown>
            <NavDropdown title="New Advertisement" id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/my-profile">as Player</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/my-profile">as Club</NavDropdown.Item>
            </NavDropdown>
          </Nav>
          <Nav className="ms-auto green-links">
            <Nav.Link as={NavLink} to="/my-profile">Chat</Nav.Link>
            <NavDropdown title="My Offers" id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/my-profile">as Player</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/my-profile">as Club</NavDropdown.Item>
            </NavDropdown>
            <NavDropdown title="My Profile" id="basic-nav-dropdown">
              <NavDropdown.Item as={NavLink} to="/my-profile">Profile</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/club-history">Club History</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/my-profile">Player Ads</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/my-profile">Club Ads</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/my-profile">Player Favorites</NavDropdown.Item>
              <NavDropdown.Item as={NavLink} to="/my-profile">Club Favorites</NavDropdown.Item>
              <NavDropdown.Item onClick={AccountService.logout} as={NavLink} to="/">Log out</NavDropdown.Item>
            </NavDropdown>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default NavbarComponent;