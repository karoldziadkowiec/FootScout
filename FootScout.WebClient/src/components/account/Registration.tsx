import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Form, Button, Col, Row, Alert, Container } from 'react-bootstrap';
import AccountService from '../../services/api/AccountService';
import RegisterDTO from '../../models/dtos/RegisterDTO';
import '../../App.css';
import '../../styles/account/Registration.css';

const Registration: React.FC = () => {
    const navigate = useNavigate();
    const [error, setError] = useState('');
    const [registerDTO, setRegisterDTO] = useState<RegisterDTO>({
        email: '',
        password: '',
        confirmPassword: '',
        firstName: '',
        lastName: '',
        phoneNumber: '',
        location: ''
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setRegisterDTO(prevRegisterDTO => ({
            ...prevRegisterDTO,
            [name]: value,
        }));
    };

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            if (isNaN(Number(registerDTO.phoneNumber))) {
                setError('Phone number must be a number.');
                return;
            }

            if (registerDTO.phoneNumber.length !== 9) {
                setError('Phone number must contain exactly 9 digits.');
                return;
            }

            if (registerDTO.password !== registerDTO.confirmPassword) {
                setError('Passwords do not match.');
                return;
            }

            await AccountService.registerUser(registerDTO);
            alert('Registration successful!');
            moveToLoginPage();
        } catch (error) {
            setError('Registration failed. Please try again.');
        }
    };

    const moveToLoginPage = () => {
        navigate('/');
    };

    return (
        <div className="Registration">
            <div className="logo-container">
                <img src={require('../../img/logo.png')} alt="logo" className="logo" />
                FootScout
            </div>
            <div className="registration-container">
                <Container>
                    <Row className="justify-content-md-center">
                        <Col md="6">
                            <h2>Sign Up</h2>
                            {error && <Alert variant="danger">{error}</Alert>}
                            <Form onSubmit={handleRegister}>
                                <Form.Group className="mb-3" controlId="formEmail">
                                    <Form.Label className="white-label">E-mail</Form.Label>
                                    <Form.Control
                                        type="email"
                                        name="email"
                                        placeholder="E-mail"
                                        value={registerDTO.email}
                                        onChange={handleChange}
                                        maxLength={50}
                                        required
                                    />
                                </Form.Group>
                                <Row>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formPassword">
                                            <Form.Label className="white-label">Password</Form.Label>
                                            <Form.Control
                                                type="password"
                                                name="password"
                                                placeholder="Password"
                                                value={registerDTO.password}
                                                onChange={handleChange}
                                                maxLength={30}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formConfirmPassword">
                                            <Form.Label className="white-label">Confirm Password</Form.Label>
                                            <Form.Control
                                                type="password"
                                                name="confirmPassword"
                                                placeholder="Confirm Password"
                                                value={registerDTO.confirmPassword}
                                                onChange={handleChange}
                                                maxLength={30}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formFirstName">
                                            <Form.Label className="white-label">First Name</Form.Label>
                                            <Form.Control
                                                type="text"
                                                name="firstName"
                                                placeholder="First Name"
                                                value={registerDTO.firstName}
                                                onChange={handleChange}
                                                maxLength={20}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formLastName">
                                            <Form.Label className="white-label">Last Name</Form.Label>
                                            <Form.Control
                                                type="text"
                                                name="lastName"
                                                placeholder="Last Name"
                                                value={registerDTO.lastName}
                                                onChange={handleChange}
                                                maxLength={30}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formPhoneNumber">
                                            <Form.Label className="white-label">Phone Number</Form.Label>
                                            <Form.Control
                                                type="tel"
                                                name="phoneNumber"
                                                placeholder="Phone Number"
                                                value={registerDTO.phoneNumber}
                                                onChange={handleChange}
                                                maxLength={9}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formLocation">
                                            <Form.Label className="white-label">Location</Form.Label>
                                            <Form.Control
                                                type="text"
                                                name="location"
                                                placeholder="Location"
                                                value={registerDTO.location}
                                                onChange={handleChange}
                                                maxLength={40}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                </Row>
                                <Button variant="success" type="submit" className="mb-3">Register account</Button>
                                <Row>
                                    <Col>
                                        <Button variant="outline-light" onClick={() => navigate('/')} className="mb-3">Back</Button>
                                    </Col>
                                </Row>
                            </Form>
                        </Col>
                    </Row>
                </Container>
            </div>
        </div>
    );
};

export default Registration;