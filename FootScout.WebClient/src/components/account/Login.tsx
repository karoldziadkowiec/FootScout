import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Form, Button, Alert } from 'react-bootstrap';
import axios from 'axios';
import LoginDTO from '../../models/dtos/LoginDTO';
import AccountService from '../../services/api/AccountService';
import '../../App.css';
import '../../styles/account/Login.css';

const Login = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const loginDTO: LoginDTO = { email, password };

    try {
      await AccountService.login(loginDTO);
      navigate('/home');
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.response) {
          if (error.response.status === 401) {
            setError('Invalid email or password.');
          } else if (error.response.status === 500) {
            setError('Internal server error. Please try again later.');
          } else {
            setError('Login failed. Please check your credentials and try again.');
          }
        } else if (error.request) {
          setError('No response from server. Please check your network connection.');
        } else {
          setError('Login request failed. Please try again.');
        }
      } else {
        setError('An unexpected error occurred. Please try again.');
      }
    }
  };

  const moveToRegistrationPage = () => {
    navigate('/registration');
  };

  return (
    <div className="Login">
      <div className="logo-container">
        <img src={require('../../img/logo.png')} alt="logo" className="logo" />
        FootScout
      </div>
      <div className="login-container">
        <Form onSubmit={handleLogin}>
          <h2>Sign in</h2>
          {error && <Alert variant="danger">{error}</Alert>}
          <Form.Group className="mb-3" controlId="formBasicEmail">
            <Form.Label className="white-label">E-mail</Form.Label>
            <Form.Control
              name="email"
              type="email"
              placeholder="Enter e-mail"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              maxLength={50}
              required
            />
          </Form.Group>
          <Form.Group className="mb-3" controlId="formBasicPassword">
            <Form.Label className="white-label">Password</Form.Label>
            <Form.Control
              name="password"
              type="password"
              placeholder="Enter password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              maxLength={30}
              required
            />
          </Form.Group>
          <div className="d-grid">
            <Button variant="success" type="submit">Log in</Button>
            <p></p>
            <Button variant="outline-light" onClick={moveToRegistrationPage}>Register account</Button>
          </div>
        </Form>
      </div>
    </div>
  );
}

export default Login;